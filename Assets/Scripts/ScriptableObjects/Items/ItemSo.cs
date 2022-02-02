using System;
using System.Linq;
using Enums;
using ScriptableObjects.Database;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Item Scriptable Object base abstract class
    /// </summary>
    public abstract class ItemSo : ScriptableObject
    {
        [ReadOnly]
        public string itemGuid;

        public Sprite sprite;
        public ItemType type;
        public int value;

        [TextArea(5, 10)]
        public string description;

        public ItemBuff[] buffs;

        public bool stackable;

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            // Generates a new guid on scriptable object creation
            if (string.IsNullOrEmpty(itemGuid))
                GenerateGuid();
#endif
        }
        
#if UNITY_EDITOR
        /// <summary>
        /// Generates a new guid and assures it's uniqueness from other items
        /// </summary>
        [Button, PropertyOrder(-1)]
        private void GenerateGuid()
        {
            do
            {
                itemGuid = Guid.NewGuid().ToString();
            } while (IsUnique());
        }
        
        /// <summary>
        /// Verifies that the current guid is unique from other items in the asset database at the specified path
        /// </summary>
        /// <returns>true if item's guid is unique</returns>
        private bool IsUnique()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(ItemSo)}", new[] {ItemDatabaseSo.ItemsPath});
            return guids
                .Select(str => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(str), typeof(ItemSo)) as ItemSo)
                .Any(itemObject => itemObject && itemObject.itemGuid.Equals(itemGuid));
        }
#endif
    }
}