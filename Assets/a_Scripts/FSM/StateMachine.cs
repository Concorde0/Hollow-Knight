using System;
using System.Collections.Generic;

namespace HollowKnight.Tools.FSM
{
    public class StateMachine<TContext>
    {
        private readonly TContext _context;
        private readonly List<IState<TContext>> _states = new();
        private readonly List<Transition<TContext>> _transitions = new();
        private IState<TContext> _current;

        public event Action<string, string> OnTransition;

        public StateMachine(TContext context) => _context = context;

        public void AddState(IState<TContext> state) => _states.Add(state);
        public void AddTransition(Transition<TContext> transition) => _transitions.Add(transition);

        public void SetInitial(IState<TContext> state)
        {
            _current = state;
            _current.OnEnter(_context);
        }

        public void Update(float deltaTime)
        {
            if (_current == null) return;

            _current.OnUpdate(_context, deltaTime);

            foreach (var t in _transitions)
            {
                if (t.From == _current && t.Condition(_context))
                {
                    var fromName = _current.Name;
                    _current.OnExit(_context);
                    _current = t.To;
                    OnTransition?.Invoke(fromName, _current.Name);
                    _current.OnEnter(_context);
                    break;
                }
            }
        }
    }
}