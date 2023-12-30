using Bomb;
using Enemy;

namespace FSM
{
    public class HitState : IState
    {
        private readonly EnemyController _manager;
        private readonly Parameter _parameter;

        public HitState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;
        }

        public void OnEnter()
        {
            _parameter.animator.Play("Hit");
        }

        public void OnUpdate()
        {
            if (_parameter.health == 0)
            {
                _manager.TransitionState(StateType.Death);
                return;
            }
            
            var info = _parameter.animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.95f)
            {
                _manager.TransitionState(StateType.Chase);
            }
        }

        public void OnExit()
        {
            _parameter.getHit = false;
        }
    }
}