using Enemy;
using UnityEngine;

namespace FSM
{
    public class ChaseState : IState
    {
        private readonly EnemyController _manager;
        private readonly Parameter _parameter;
        
        public ChaseState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;
        }
        
        public void OnEnter()
        {
            _parameter.animator.Play("Run");
        }

        public void OnUpdate()
        {
            if (_parameter.getHit)
            {
                _manager.TransitionState(StateType.Hit);
                return;
            }
            
            // 在视野范围内则追击
            if (_parameter.currentAttackTarget)
            {
                _manager.FlipDirection(_parameter.currentAttackTarget);
                _manager.transform.position = Vector2.MoveTowards(_manager.transform.position, 
                        _parameter.currentAttackTarget.position, _parameter.chaseSpeed * Time.deltaTime);
            }
            // 否则返回Idle状态
            else
            {
                _manager.TransitionState(StateType.Idle);
                return;
            }
            
            // 在攻击范围内则切换到攻击状态
            if (Physics2D.OverlapCircle(_parameter.attackPoint.position, _parameter.skillAttackRadius, _parameter.targetLayer)
                && _parameter.currentAttackTarget && _parameter.currentAttackTarget.CompareTag("Bomb"))
            {
                _manager.TransitionState(StateType.SkillAttack);
                return;
            }
            if (Physics2D.OverlapCircle(_parameter.attackPoint.position, _parameter.normalAttackRadius, _parameter.targetLayer)
                && _parameter.currentAttackTarget && _parameter.currentAttackTarget.CompareTag("Player"))
            {
                _manager.TransitionState(StateType.NormalAttack);
            }
        }

        public void OnExit()
        {
            
        }
    }
}