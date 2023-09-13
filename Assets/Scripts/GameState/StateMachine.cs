namespace GameState
{
    public class StateMachine
    {
        public BaseState CurrentState { get; private set; }

        public void ChangeState(BaseState newState)
        {
            CurrentState.OnEnter();
            CurrentState = newState;
            CurrentState.OnExit();
        }
    }
}