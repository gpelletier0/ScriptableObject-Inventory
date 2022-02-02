using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Default Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Default", menuName = "Inventory System/Items/Default")]
    public class DefaultSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Default;
        }
    }
}