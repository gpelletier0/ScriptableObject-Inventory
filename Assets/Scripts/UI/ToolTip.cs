using System.Text;
using ScriptableObjects.Items;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// ToolTip script for visual display
    /// </summary>
    public class ToolTip : MonoBehaviour
    {
        public TextMeshProUGUI tmp;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            transform.position = Input.mousePosition;
        }

        /// <summary>
        /// Generates text from the item values and displays the tooltip
        /// </summary>
        /// <param name="item">item to display</param>
        public void GenerateToolTip(ItemSo item)
        {
            if (!item) return;

            var sb = new StringBuilder();

            sb.AppendLine($"<b>{item.name}</b>");
            sb.AppendLine(item.type + "\n");
            sb.AppendLine(item.description + "\n");

            if (item.value > 0)
                sb.AppendLine($"Value: {item.value.ToString()}\n");

            foreach (var buff in item.buffs)
                sb.AppendLine($"{buff.itemAttribute} {buff.value}");

            tmp.text = sb.ToString();

            gameObject.SetActive(true);
        }

        /// <summary>
        /// Clears and hides the tooltip
        /// </summary>
        public void HideToolTip()
        {
            gameObject.SetActive(false);
            tmp.text = string.Empty;
        }
    }
}