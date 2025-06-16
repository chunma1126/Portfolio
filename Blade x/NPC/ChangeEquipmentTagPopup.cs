using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Swift_Blade.UI
{
    public class ChangeEquipmentTagPopup : PopupUI
    {
        [SerializeField] private Transform itemSlotRoot;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;
        [SerializeField] private Image itemImage;
                
        private List<ChangeTagItemSlotUI> itemSlotList = new List<ChangeTagItemSlotUI>(40);
        
        private void Start()
        {
            for (int i = 0; i < itemSlotRoot.childCount; i++)
            {
                itemSlotList.Add(itemSlotRoot.GetChild(i).GetComponent<ChangeTagItemSlotUI>());   
            }
        }
        
        public override void Popup()
        {
            int i = 0;
            foreach (ItemDataSO item in InventoryManager.Inventory.itemInventory)
            {
                if (item.itemType == ItemType.EQUIPMENT)
                {
                    itemSlotList[i].SetItemData(item);
                    itemSlotList[i].SetItemUI(item.itemImage);
                    i++;
                }
            }
            
            base.Popup();
        }
        
        public void SetItemData(string itemName , string itemDescription , Sprite itemSprite)
        {
            itemNameText.SetText(itemName);
            itemDescriptionText.SetText(itemDescription);
            itemImage.sprite = itemSprite;
        }
        
    }
}
