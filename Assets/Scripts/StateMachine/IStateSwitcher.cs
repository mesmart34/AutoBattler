namespace StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : BaseState;
    }
}