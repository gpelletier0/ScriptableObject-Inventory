using UnityEngine;

namespace UI
{
    /// <summary>
    /// Dynamic interface class for bags and containers
    /// </summary>
    public class DynamicInterface : UserInterface
    {
        public GameObject slotGo;

        /// <summary>
        /// Creates a new GameObject for each inventory slot in the inventory array, adds them to the dictionary of itemsDisplayed
        /// and set's the inventory slot's GameObject to the newly created GameObject
        /// </summary>
        protected override void CreateSlots()
        {
            foreach (var inventorySlot in inventory.inventoryArray)
            {
                var go = Instantiate(slotGo, transform);
                go.SetActive(true);

                itemsDisplayed.Add(go, inventorySlot);
                inventorySlot.SlotGo = go;
            }
        }
    }
}