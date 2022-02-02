using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Junk Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Junk", menuName = "Inventory System/Items/Junk")]
    public class JunkSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Junk;
        }
    }
}