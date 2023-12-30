using Bomb;
using UnityEngine;

namespace Enemy
{
    public class CucumberController : EnemyController
    {
        private static readonly int Attack = Animator.StringToHash("NormalAttack");
        private static readonly int Skill = Animator.StringToHash("SkillAttack");

        public override void NormalAttack()
        {
            base.NormalAttack();
            parameter.animator.SetTrigger(Attack);
        }
        
        public override void SkillAttack()
        {
            base.SkillAttack();
            parameter.animator.SetTrigger(Skill);
        }
        
        public void SetOff() // Animation event
        {
            if (parameter.currentAttackTarget) 
            {
                parameter.currentAttackTarget.GetComponent<BombController>()?.TurnOff();
            }
        }
        
    }
}