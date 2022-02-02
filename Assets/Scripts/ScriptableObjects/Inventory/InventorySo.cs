using System;
using Inventory;
using ScriptableObjects.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    /// <summary>
    /// Inventory Scriptable Object
    /// </summary>
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventorySo : SerializedScriptableObject
    {
        [OnValueChanged("OnAddRemoveSlots")]
        public int nbSlots = 16;

        [OnValueChanged("OnDataChanged", true)]
        public Slot[] inventoryArray;

        private void Awake()
        {
            inventoryArray ??= new Slot[nbSlots];
        }

        /// <summary>
        /// Clears all objects in the inventory array
        /// </summary>
        [Button, PropertyOrder(-1)]
        private void Clear()
        {
            foreach (var inventorySlot in inventoryArray)
                inventorySlot.UpdateSlot(null, 0);
        }

        /// <summary>
        /// Recreates the inventory array when the number of slots value changes
        /// </summary>
        private void OnAddRemoveSlots()
        {
            inventoryArray = new Slot[nbSlots];
        }

        /// <summary>
        /// Set's a default slot amount when the scriptable object value changes in the array
        /// </summary>
        private void OnDataChanged()
        {
            foreach (var inventorySlot in inventoryArray)
                if (!inventorySlot.itemSo)
                    inventorySlot.amount = 0;
                else if (inventorySlot.amount < 1)
                    inventorySlot.amount = 1;
        }

        /// <summary>
        /// Adds an item to the inventory array
        /// </summary>
        /// <param name="item">item scriptable object to add</param>
        /// <param name="amount">amount of items</param>
        /// <returns>true if successful</returns>
        public bool AddItem(ItemSo item, int amount)
        {
            if (!item) return false;

            var inventorySlot = Array.Find(inventoryArray,
                slot => slot.itemSo != null && slot.itemSo.itemGuid.Equals(item.itemGuid));

            if (inventorySlot == null || !inventorySlot.itemSo.stackable)
                return AddToEmptySlot(item, amount);

            AddToExistingSlot(inventorySlot, amount);
            return true;
        }

        /// <summary>
        /// Adds an item to a null value item scriptable object in the array
        /// </summary>
        /// <param name="item">item to add</param>
        /// <param name="amount">amount of the item</param>
        /// <returns></returns>
        private bool AddToEmptySlot(ItemSo item, int amount)
        {
            var inventorySlot = Array.Find(inventoryArray, slot => slot.itemSo == null);
            if (inventorySlot != null)
            {
                inventorySlot.UpdateSlot(item, amount);
                return true;
            }

            Debug.LogWarning("Inventory Full");
            return false;
        }

        /// <summary>
        /// Increments/Decrements the amount of an existing item in the inventory array
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="amount"></param>
        private static void AddToExistingSlot(Slot slot, int amount)
        {
            slot.AddAmount(amount);
        }

        /// <summary>
        /// Moves items to the other's position in the inventory array
        /// </summary>
        /// <param name="slot1">item from</param>
        /// <param name="slot2">item to</param>
        public static void MoveItem(Slot slot1, Slot slot2)
        {
            var (itemSo, amount) = slot2.Copy();
            slot2.UpdateSlot(slot1.itemSo, slot1.amount);
            slot1.UpdateSlot(itemSo, amount);
        }

        /// <summary>
        /// Set's an item's scriptable object value to null and it's amount to 0 in the inventory array
        /// </summary>
        /// <param name="slot">slot to nullify and 0 out</param>
        public static void RemoveItem(Slot slot)
        {
            slot.UpdateSlot(null, 0);
        }
    }
}