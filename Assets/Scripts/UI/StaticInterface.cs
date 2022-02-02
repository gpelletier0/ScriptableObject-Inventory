using UnityEngine;

namespace UI
{
    /// <summary>
    /// Static interface class for equipment panels
    /// </summary>
    public class StaticInterface : UserInterface
    {
        public GameObject[] slots;

        /// <summary>
        /// Assigns each GameObject of slots array to the itemsDisplayed dictionary and set's the inventory array's slot GameObject
        /// that GameObject
        /// </summary>
        protected override void CreateSlots()
        {
            for (var i = 0; i < slots.Length; i++)
            {
                itemsDisplayed.Add(slots[i], inventory.inventoryArray[i]);
                inventory.inventoryArray[i].SlotGo = slots[i];
            }
        }
    }
}