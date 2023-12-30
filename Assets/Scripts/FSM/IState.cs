using Enemy;

namespace FSM
{
    public interface IState
    {
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
    }
}