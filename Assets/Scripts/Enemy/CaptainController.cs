using UnityEngine;

namespace Enemy
{
    public class CaptainController : EnemyController
    {
        private SpriteRenderer _sprite;
        
        private static readonly int Attack = Animator.StringToHash("NormalAttack");
        private static readonly int Skill = Animator.StringToHash("SkillAttack");

        public override void Init()
        {
            base.Init();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public override void NormalAttack()
        {
            parameter.animator.SetTrigger(Attack);
        }
        
        public override void SkillAttack()
        {
            parameter.animator.SetTrigger(Skill);
        }

        public override void SkillOnUpdate()
        {
            if (!parameter.currentAttackTarget) return;
            // 根据炸弹的反方向跑
            if (parameter.currentAttackTarget.position.x > transform.position.x)
            {
                _sprite.flipX = true;
                transform.position = Vector2.MoveTowards(transform.position,
                    (Vector2)transform.position + Vector2.left, parameter.chaseSpeed * Time.deltaTime);
            }
            else
            {
                _sprite.flipX = true;
                transform.position = Vector2.MoveTowards(transform.position, 
                    (Vector2)transform.position + Vector2.right, parameter.chaseSpeed * Time.deltaTime);
            }
        }

        public override void SkillOnExit()
        {
            _sprite.flipX = false;
        }
    }
}