using System;

namespace HollowKnight.Tools.FSM
{
    public class Transition<TContext>
    {
        public IState<TContext> From { get; }
        public IState<TContext> To { get; }
        public Func<TContext,bool> Condition{get;}
        
        public Transition(IState<TContext> from, IState<TContext> to, Func<TContext,bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}