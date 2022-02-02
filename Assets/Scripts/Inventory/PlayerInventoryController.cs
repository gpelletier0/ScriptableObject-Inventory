using Audio;
using Items;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Controls the interactions between the world and the player inventory/equipment.
    /// </summary>
    public class PlayerInventoryController : MonoBehaviour
    {
        public GameObject inventoryPanel;
        public GameObject equipmentPanel;
        public InventorySo inventorySo;
        public AudioManager audioManager;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B)) inventoryPanel.SetActive(!inventoryPanel.activeSelf);

            if (Input.GetKeyDown(KeyCode.I)) equipmentPanel.SetActive(!equipmentPanel.activeSelf);
        }

        private void OnTriggerEnter(Collider other)
        {
            var itemPickup = other.GetComponent<ItemPickup>();
            if (!itemPickup || !itemPickup.itemSo) return;

            if (inventorySo.AddItem(itemPickup.itemSo, itemPickup.amount))
            {
                if(audioManager)
                    audioManager.PlayMoveSuccess();
                
                Destroy(other.transform.gameObject);
            }
        }
    }
}