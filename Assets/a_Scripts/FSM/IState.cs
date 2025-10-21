namespace HollowKnight.Tools.FSM
{
    public interface IState<TContext>
    {
        string Name { get; }
        void OnEnter(TContext context);
        void OnUpdate(TContext context, float deltaTime);
        void OnExit(TContext context);
    }
}