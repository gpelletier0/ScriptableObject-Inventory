using Inventory;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Mouse hover slot components
    /// </summary>
    internal class MouseHoverSlot : MonoBehaviour
    {
        public GameObject uiPanelHover;
        public Slot slot;

        private void Start()
        {
            gameObject.SetActive(false);
        }
    }
}