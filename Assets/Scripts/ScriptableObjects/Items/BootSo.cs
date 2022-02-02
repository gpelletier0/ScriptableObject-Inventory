using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Boot Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Boot", menuName = "Inventory System/Items/Boot")]
    public class BootSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Boot;
        }
    }
}