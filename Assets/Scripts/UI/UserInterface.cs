using System.Collections.Generic;
using Audio;
using Inventory;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// User interface base abstract class
    /// </summary>
    public abstract class UserInterface : MonoBehaviour
    {
        public GameObject mouseSlotGo;
        public GameObject toolTipGo;
        public InventorySo inventory;
        public AudioManager audioManager;
        public DestroyDialog destroyDialog;

        protected Dictionary<GameObject, Slot> itemsDisplayed;

        private MouseHoverSlot _mouseHoverSlot;
        private RectTransform _mouseSlotRect;
        private ToolTip _toolTip;

        private void Start()
        {
            _mouseSlotRect = mouseSlotGo.GetComponent<RectTransform>();
            _mouseHoverSlot = mouseSlotGo.GetComponent<MouseHoverSlot>();
            _toolTip = toolTipGo.GetComponent<ToolTip>();

            if (destroyDialog)
                destroyDialog.DestroyItemAction += OnDestroyItemAction;

            itemsDisplayed = new Dictionary<GameObject, Slot>();

            foreach (var inventorySlot in inventory.inventoryArray)
            {
                inventorySlot.Parent = this;
                inventorySlot.onSlotUpdated += OnSlotUpdate;
            }

            CreateSlots();
            UpdateSlot();
        }

        private static void OnDestroyItemAction(Slot slot)
        {
            InventorySo.RemoveItem(slot);
        }

        /// <summary>
        /// Listener for slot update events
        /// </summary>
        /// <param name="slot"></param>
        private static void OnSlotUpdate(Slot slot)
        {
            if (!Application.isPlaying) return;
            
            if (slot.itemSo)
                SetGameObjectComponents(slot.SlotGo, slot);
            else
                ClearGameObjectComponents(slot.SlotGo);
        }

        /// <summary>
        /// Abstract function for slot creation
        /// </summary>
        protected abstract void CreateSlots();

        /// <summary>
        /// Updates the slot's components to the item's values or clears them
        /// </summary>
        private void UpdateSlot()
        {
            foreach (var kp in itemsDisplayed)
                if (kp.Value.itemSo)
                    SetGameObjectComponents(kp.Key, kp.Value);
                else
                    ClearGameObjectComponents(kp.Key);
        }

        /// <summary>
        /// Returns a formatted string from the int amount passed
        /// </summary>
        /// <param name="amount">item amount</param>
        /// <returns>a formatted string from the amount passed</returns>
        private static string SetItemAmount(int amount)
        {
            return amount > 1 ? amount.ToString("n0") : "";
        }

        /// <summary>
        /// Set's the components of the passed GameObject from the slot values
        /// </summary>
        /// <param name="go">GameObject to set</param>
        /// <param name="slot">Slot values</param>
        private static void SetGameObjectComponents(GameObject go, Slot slot)
        {
            var components = go.GetComponent<SlotComponents>();

            components.image.sprite = slot.itemSo.sprite;
            components.image.color = Color.white;

            components.tmp.text = SetItemAmount(slot.amount);
        }

        /// <summary>
        /// Clears the values of the passed GameObject
        /// </summary>
        /// <param name="go">GameObject to clear</param>
        private static void ClearGameObjectComponents(GameObject go)
        {
            var components = go.GetComponent<SlotComponents>();

            components.image.sprite = null;
            components.image.color = Color.clear;
            components.tmp.text = "";
        }

        /// <summary>
        /// Listener for PointerEnter event
        /// </summary>
        /// <param name="go">GameObject moused over</param>
        public void OnPointerEnter(GameObject go)
        {
            if (!itemsDisplayed.ContainsKey(go)) return;

            _mouseHoverSlot.slot = itemsDisplayed[go];

            if (!mouseSlotGo.activeSelf)
                _toolTip.GenerateToolTip(itemsDisplayed[go].itemSo);
        }

        /// <summary>
        /// Listener for PointerExit event
        /// </summary>
        public void OnPointerExit()
        {
            _mouseHoverSlot.slot = null;
            _toolTip.HideToolTip();
        }

        /// <summary>
        /// Listener for BeginDrag event
        /// </summary>
        /// <param name="go">GameObject being dragged</param>
        public void OnBeginDrag(GameObject go)
        {
            if (!itemsDisplayed[go].itemSo) return;

            go.GetComponent<SlotComponents>().image.color = Color.gray;
            SetGameObjectComponents(mouseSlotGo, itemsDisplayed[go]);

            audioManager.PlayMoveStart();
            toolTipGo.SetActive(false);
            mouseSlotGo.SetActive(true);
        }

        /// <summary>
        /// Listener for EndDrag event
        /// </summary>
        /// <param name="go">GameObject being dropped</param>
        public void OnEndDrag(GameObject go)
        {
            if (!itemsDisplayed[go].itemSo) return;

            if (_mouseHoverSlot.uiPanelHover)
            {
                if (_mouseHoverSlot.slot != null)
                {
                    if (_mouseHoverSlot.slot.Parent.itemsDisplayed[_mouseHoverSlot.slot.SlotGo].CanPlaceItem(itemsDisplayed[go].itemSo) &&
                        itemsDisplayed[go].CanPlaceItem(_mouseHoverSlot.slot.Parent.itemsDisplayed[_mouseHoverSlot.slot.SlotGo].itemSo))
                    {
                        InventorySo.MoveItem(itemsDisplayed[go],
                            _mouseHoverSlot.slot.Parent.itemsDisplayed[_mouseHoverSlot.slot.SlotGo]);
                        audioManager.PlayMoveSuccess();
                    }
                    else
                        MoveFailure(go);
                }
                else
                    MoveFailure(go);
            }
            else
                destroyDialog.DisplayDialog(itemsDisplayed[go]);

            mouseSlotGo.SetActive(false);
            ClearGameObjectComponents(mouseSlotGo);
        }

        /// <summary>
        /// Resets the slot's game component's image color to white and plays failure sound
        /// </summary>
        /// <param name="go"></param>
        private void MoveFailure(GameObject go)
        {
            go.GetComponent<SlotComponents>().image.color = Color.white;
            audioManager.PlayMoveFailure();
        }

        /// <summary>
        /// Moves the mouse object rect to input mouse position while slot is being dragged
        /// </summary>
        public void OnDrag()
        {
            if (mouseSlotGo.activeSelf)
                _mouseSlotRect.position = Input.mousePosition;
        }

        /// <summary>
        /// Listener for PointerEnter event for the inventory panels
        /// </summary>
        /// <param name="go">Inventory panel's GameObject</param>
        public void OnUiPanelPointerEnter(GameObject go)
        {
            _mouseHoverSlot.uiPanelHover = go;
        }

        /// <summary>
        /// Listener for PointerExit event for the inventory panels
        /// </summary>
        public void OnUiPanelPointerExit()
        {
            _mouseHoverSlot.uiPanelHover = null;
        }
    }
}