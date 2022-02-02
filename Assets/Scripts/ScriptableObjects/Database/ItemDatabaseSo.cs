using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Items;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjects.Database
{
    /// <summary>
    /// A Scriptable Object database of all the item scriptable objects in the specified path
    /// </summary>
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Database")]
    public class ItemDatabaseSo : SerializedScriptableObject
    {
        // Path where item scriptable objects are stored
        public const string ItemsPath = "Assets/ScriptableObjects/Items";

        [ReadOnly]
        public HashSet<ItemSo> itemsHashSet;
        
#if UNITY_EDITOR
        /// <summary>
        /// Retrieves all item scriptable object from the specified path and adds them to the hashset
        /// OnInspectorInit executes when the property's drawers is initialized in the inspector
        /// </summary>
        [OnInspectorInit]
        private void GetAllItems()
        {
            itemsHashSet = new HashSet<ItemSo>();

            var guids = AssetDatabase.FindAssets($"t:{nameof(ItemSo)}", new[] {ItemsPath});
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var itemObject = AssetDatabase.LoadAssetAtPath(path, typeof(ItemSo)) as ItemSo;

                if (itemObject)
                    itemsHashSet.Add(itemObject);
            }
        }
#endif
        
        /// <summary>
        /// Returns the item or null from guid
        /// </summary>
        /// <param name="guid">item scriptable object guid</param>
        /// <returns>item scriptable object or null</returns>
        public ItemSo GetItem(string guid)
        {
            return itemsHashSet.FirstOrDefault(itemSo => itemSo.itemGuid.Equals(guid));
        }

        /// <summary>
        /// Return the item's guid or null
        /// </summary>
        /// <param name="item">item to find</param>
        /// <returns>item scriptable object guid or null</returns>
        public string GetItemGuid(ItemSo item)
        {
            return itemsHashSet.FirstOrDefault(itemSo => itemSo.Equals(item))?.itemGuid;
        }
    }
}