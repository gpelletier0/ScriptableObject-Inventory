using System;
using Enums;

namespace ScriptableObjects.Items
{
    /// <summary>
    /// Item buff
    /// </summary>
    [Serializable]
    public class ItemBuff
    {
        public ItemAttribute itemAttribute;
        public int value;
    }
}