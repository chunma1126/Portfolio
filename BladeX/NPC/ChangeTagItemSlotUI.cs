using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

namespace Swift_Blade.UI
{
    public class ChangeTagItemSlotUI : ItemSlot
    {
        [SerializeField] private float scaleSize = 1.02f;
        [SerializeField] private float scaleDuration;
        private ChangeEquipmentTagPopup changeEquipmentTagPopup;
        
        private void Start()
        {
            changeEquipmentTagPopup =  PopupManager.Instance.GetPopupUI(PopupType.ChangeEquipmentTag) as ChangeEquipmentTagPopup;
               
        }

        private void OnDestroy()
        {
            DOTween.Kill(gameObject);
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
           
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if(_itemDataSO == null)return;
            
            changeEquipmentTagPopup.SetItemData(_itemDataSO.itemName , _itemDataSO.description,_itemDataSO.itemImage);
            
            transform.DOScale(scaleSize, scaleDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy)
                .SetUpdate(true);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            if(_itemDataSO == null)return;
            
            changeEquipmentTagPopup.SetItemData("" , "",null);
            
            transform.DOScale(1f, scaleDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy)
                .SetUpdate(true);
        }
        
        
    }
}
