using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Off Hand Item
    /// </summary>
    [CreateAssetMenu(fileName = "New OffHand", menuName = "Inventory System/Items/Off Hand")]
    public class OffHandSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.OffHand;
        }
    }
}