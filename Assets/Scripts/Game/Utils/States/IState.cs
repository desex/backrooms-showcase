using System;
using Game.Utils.Tutorial;
using R3;

namespace Game.Utils.States
{
    public interface IState : IDisposable
    {
        Observable<Unit> StateEnded { get; }
        ETutorialState StateType { get; }
        void Enter();
    }
}