using Bomb;
using UnityEngine;

namespace Enemy
{
    public class BaldPirateController : EnemyController
    {
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
    }
}