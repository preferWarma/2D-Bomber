using Enemy;

namespace FSM
{
    public class AttackState : IState
    {
        private readonly EnemyController _manager;   // 状态机
        private readonly Parameter _parameter;   // 参数
        
        public AttackState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;    
        }
        
        public void OnEnter()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}