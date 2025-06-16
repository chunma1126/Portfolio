
using Swift_Blade.Combat.Projectile;
using Unity.VisualScripting;
using UnityEngine;

namespace Swift_Blade.Enemy
{
    public class BombAnimatorController : BaseEnemyAnimationController
    {
        [SerializeField] private Bomb bomb;
        [SerializeField] private Transform rightHand;

        [SerializeField] private GameObject handBomb;
        
        public void Throw()
        {
            handBomb.SetActive(false);
            Bomb newBomb = Instantiate(bomb , rightHand.position, Quaternion.identity);
            newBomb.SetDirection(transform.forward);
        }

        public override void SetAnimationEnd()
        {
            base.SetAnimationEnd();
            handBomb.SetActive(true);
        }
        
    }
}
