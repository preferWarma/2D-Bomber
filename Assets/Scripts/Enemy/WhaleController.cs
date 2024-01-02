using Bomb;
using UnityEngine;

namespace Enemy
{
    public class WhaleController : EnemyController
    {
        private int _swallowed = 0;
        
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

        private void Swallow() // Animation Event
        {
            parameter.currentAttackTarget.GetComponent<BombController>().TurnOff();
            parameter.currentAttackTarget.gameObject.SetActive(false);
            if (_swallowed < 3)
            {
                _swallowed++;
                transform.localScale *= 1.1f;
            }
        }
    }
}