using Enums;
using UnityEngine;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Main Hand Item
    /// </summary>
    [CreateAssetMenu(fileName = "New MainHand", menuName = "Inventory System/Items/Main Hand")]
    public class MainHandSo : ItemSo
    {
        protected override void Awake()
        {
            base.Awake();
            type = ItemType.MainHand;
        }
    }
}