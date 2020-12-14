public interface IState
{
    void OnEnter();
    void PhysicsTick();
    void Tick();
    void OnExit();
}
