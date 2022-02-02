using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Chest Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Chest", menuName = "Inventory System/Items/Chest")]
    public class ChestSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Chest;
        }
    }
}