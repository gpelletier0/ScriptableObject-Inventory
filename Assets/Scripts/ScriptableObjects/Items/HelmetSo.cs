using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Helmet Item
    /// </summary>
    [CreateAssetMenu(fileName = "New Helmet", menuName = "Inventory System/Items/Helmet")]
    public class HelmetSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.Helmet;
        }
    }
}