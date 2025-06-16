using System;
using Swift_Blade.UI;

namespace Swift_Blade
{
    public class EquipmentNpc : NPC
    {
        public override void Interact()
        {
            TalkWithNPC(() =>
            {
                PopupManager.Instance.PopUp(PopupType.ChangeEquipmentTag);
            });
            
        }
        
        protected override void TalkWithNPC(Action dialogueEndEvent = null)
        {
            DialogueManager.Instance.StartDialogue(dialogueData).Subscribe(() =>
            {
                dialogueEndEvent?.Invoke();
                OnDialogueEndEvent?.Invoke();
            });
            
        }
        
    }
}
