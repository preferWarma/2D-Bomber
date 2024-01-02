using Player;
using UnityEngine;

namespace Enemy
{
    public class BigGuyController : EnemyController
    {
        public Transform pickPoint;
        public int throwPower = 5;
        
        private static readonly int Attack = Animator.StringToHash("NormalAttack");
        private static readonly int Skill = Animator.StringToHash("SkillAttack");

        public override void NormalAttack()
        {
            parameter.animator.SetTrigger(Attack);
        }
        
        public override void SkillAttack()
        {
            parameter.animator.SetTrigger(Skill);
        }

        public void PickUpBomb()    // Animation Event
        {
            if (parameter.currentAttackTarget.CompareTag("Bomb") && !parameter.hasBomb)
            {
                parameter.currentAttackTarget.transform.position = pickPoint.position;
                parameter.currentAttackTarget.transform.SetParent(pickPoint);
                parameter.currentAttackTarget.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                parameter.currentAttackTarget.localScale *= 0.8f;
                parameter.hasBomb = true;
            }
        }

        public void ThrowAway()     // Animation Event
        {
            if (!parameter.hasBomb) return;
            
            FlipDirection(parameter.currentAttackTarget);
            var targetRigidBody = parameter.currentAttackTarget.GetComponent<Rigidbody2D>();
            targetRigidBody.bodyType = RigidbodyType2D.Dynamic;
            parameter.currentAttackTarget.localScale *= 1.25f;
            parameter.currentAttackTarget.transform.SetParent(transform.parent.parent);
            // 朝玩家方向扔
            if (PlayerController.Instance.transform.position.x - transform.position.x < 0)
            {
                targetRigidBody.AddForce(new Vector2(-1, 1) * throwPower, ForceMode2D.Impulse);
            }
            else
            {
                targetRigidBody.AddForce(new Vector2(1, 1) * throwPower, ForceMode2D.Impulse);
            }

            parameter.hasBomb = false;
        }
    }
}