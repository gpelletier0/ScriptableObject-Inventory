using System;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class DestroyDialog : MonoBehaviour
    {
        public Image image;

        private Slot _slot;

        public event Action<Slot> DestroyItemAction;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the slot item and display values and activates the panel
        /// </summary>
        /// <param name="slot"></param>
        public void DisplayDialog(Slot slot)
        {
            _slot = slot;

            image.sprite = _slot.itemSo.sprite;
            image.color = Color.white;

            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivates and clears slot and image variables
        /// </summary>
        private void ResetDialog()
        {
            gameObject.SetActive(false);

            _slot = null;

            image.color = Color.clear;
            image.sprite = null;
        }

        /// <summary>
        /// Ok button OnClick function
        /// </summary>
        public void OnYesButton()
        {
            DestroyItemAction?.Invoke(_slot);
            ResetDialog();
        }

        /// <summary>
        /// Cancel button OnClick function
        /// </summary>
        public void OnNoButton()
        {
            _slot.SlotGo.gameObject.GetComponent<SlotComponents>().image.color = Color.white;
            ResetDialog();
        }
    }
}