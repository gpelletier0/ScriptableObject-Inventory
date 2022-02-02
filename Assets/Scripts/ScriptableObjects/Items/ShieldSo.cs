using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Shield Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Shield", menuName = "Inventory System/Items/Shield")]
    public class ShieldSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Shield;
        }
    }
}