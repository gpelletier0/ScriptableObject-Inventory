using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Food Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Food", menuName = "Inventory System/Items/Food")]
    public class FoodSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Food;
        }
    }
}