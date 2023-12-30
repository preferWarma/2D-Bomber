using Enemy;
using UnityEngine;

namespace FSM
{
    public class DeadState : IState
    {
        private readonly EnemyController _manager;
        private readonly Parameter _parameter;
        private float _timer;   // 死亡销毁的计时器
        
        public DeadState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;
        }
        
        public void OnEnter()
        {
            _parameter.animator.Play("Dead");
            _timer = 3f;
        }

        public void OnUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                Object.Destroy(_manager.gameObject);
            }
        }

        public void OnExit()
        {
            
        }
    }
}