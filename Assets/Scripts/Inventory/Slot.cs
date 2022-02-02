using System;
using System.Linq;
using Enums;
using ScriptableObjects.Items;
using UI;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// An inventory slot object
    /// </summary>
    [Serializable]
    public class Slot
    {
        public delegate void SlotUpdated(Slot slot);

        public ItemSo itemSo;
        public int amount;
        public ItemType[] allowedTypes = Array.Empty<ItemType>();

        public SlotUpdated onSlotUpdated;

        public GameObject SlotGo { get; set; }
        public UserInterface Parent { get; set; }

        /// <summary>
        /// Creates copy of the scriptable object and it's amount value
        /// </summary>
        /// <returns>ShallowCopy copy object</returns>
        public (ItemSo, int) Copy()
        {
            return (itemSo, amount);
        }

        /// <summary>
        /// Updates the slot item and value and invokes the onSlotUpdated event
        /// </summary>
        /// <param name="item">item scriptable object</param>
        /// <param name="value">amount value</param>
        public void UpdateSlot(ItemSo item, int value)
        {
            itemSo = item;
            amount = value;

            onSlotUpdated?.Invoke(this);
        }

        /// <summary>
        /// Adds or subtract from the slot amount
        /// </summary>
        /// <param name="value">value to add or subtract from amount</param>
        public void AddAmount(int value)
        {
            UpdateSlot(itemSo, amount += value);
        }

        /// <summary>
        /// Checks if the item is allowed in the slot.
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true/false</returns>
        public bool CanPlaceItem(ItemSo item)
        {
            return !item || allowedTypes.Length <= 0 || allowedTypes.Contains(item.type);
        }
    }
}