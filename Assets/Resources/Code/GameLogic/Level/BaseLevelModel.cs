
using System.Collections.Generic;

namespace Assets.Resources.Code.GameLogic.Level
{
    public interface IBaseLevelModel
    {
        GameDirector Director { get; }
        int CurrentState { get; set; }
        BaseLevelController Controller { get; set; }
        Stack<int> PreviousStatesStack { get; set; } 
        void OnStateCycle();
    }
}
