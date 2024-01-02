using Enemy;
using UnityEngine;

namespace FSM
{
    public class PatrolState : IState
    {
        private readonly EnemyController _manager;   // 状态机
        private readonly Parameter _parameter;   // 参数
        private Transform _currentPatrol;    // 当前巡逻点
        
        
        public PatrolState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;
        }
        
        public void OnEnter()
        {
            ChoosePatrolPoint();
            _parameter.animator.Play("Run");
        }

        public void OnUpdate()
        {
            _manager.FlipDirection(_currentPatrol);   // 巡逻时翻转方向
         
            if (_parameter.hasBomb) return;
            
            if (_parameter.getHit)
            {
                _manager.TransitionState(StateType.Hit);
                return;
            }
            
            // 移动到巡逻点
            _manager.transform.position = Vector2.MoveTowards(_manager.transform.position,
                _currentPatrol.position, _parameter.moveSpeed * Time.deltaTime);
            
            // 发现攻击目标则切换到追击状态
            if (_parameter.attackList.Count > 0)
            {
                _manager.TransitionState(StateType.Chase);
                return;
            }
            
            // 到达时进入Idle状态
            if (Vector2.Distance(_manager.transform.position, _currentPatrol.position) < 0.1f)
            {
                _manager.TransitionState(StateType.Idle);
            }
            
        }

        public void OnExit()
        {
        }
        
        private void ChoosePatrolPoint()
        {
            // 选择距离最远的巡逻点
            var maxDistance = 0f;
            foreach (var patrolPoint in _parameter.patrolPoints)
            {
                var distance = Vector2.Distance(_manager.transform.position, patrolPoint.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    _currentPatrol = patrolPoint;
                }
            }
        }
    }
}