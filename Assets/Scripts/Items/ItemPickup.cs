using System;
using ScriptableObjects.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// World item sprite display script
    /// </summary>
    [Serializable]
    public class ItemPickup : MonoBehaviour
    {
        [OnValueChanged("OnValueChanged")]
        public ItemSo itemSo;

        public int amount;
        public SpriteRenderer sprite;

        private void Start()
        {
            if (!itemSo) sprite = null;
        }

        /// <summary>
        /// Sets the item's sprite from the scriptable object when it's value changes
        /// </summary>
        private void OnValueChanged()
        {
            if (itemSo)
            {
                sprite.sprite = itemSo.sprite;
                amount = 1;
            }
            else
            {
                sprite.sprite = null;
                amount = 0;
            }
        }
    }
}