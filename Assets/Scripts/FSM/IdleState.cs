using Enemy;
using UnityEngine;

namespace FSM
{
    public class IdleState : IState
    {
        private readonly EnemyController _manager;   // 状态机
        private readonly Parameter _parameter;   // 参数
        private float _timer; // 计时器
        
        public IdleState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;
        }
        
        public void OnEnter()
        {
            _parameter.animator.Play("Idle");
            _timer = 0f;
        }

        public void OnUpdate()
        {
            _timer += Time.deltaTime;

            if (_parameter.getHit)
            {
                _manager.TransitionState(StateType.Hit);
                return;
            }
            
            if (_parameter.attackList.Count > 0)
            {
                _manager.TransitionState(StateType.Chase);
                return;
            }
            
            if (_timer >= _parameter.idleTime)
            {
                _manager.TransitionState(StateType.Patrol);
            }
        }

        public void OnExit()
        {
            _timer = 0;
        }
    }
}