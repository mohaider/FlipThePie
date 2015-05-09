using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level.GameStart
{
    class StartScreenLevelModel : MonoBehaviour, IBaseLevelModel
    {
        private int _currentState;
        private int _previousState;
        private BaseLevelController _controller;
        private Stack<int> _previousStates;
        public GameDirector Director
        {
            get { return GameDirector.Instance; }
        }

        public int CurrentState
        {
            get
            {
                return _currentState;
            }
            set { _currentState = value; }
        }

        public BaseLevelController Controller
        {
            get
            {
                if (_controller == null)
                {
                    _controller = GetComponent<StartScreenLevelController>();
                }
                return _controller;
            }
            set { _controller = value; }
        }

        public Stack<int> PreviousStatesStack
        {
            get
            {
                if (_previousStates == null)
                {
                    _previousStates = new Stack<int>();
                }
                return _previousStates;
            }
            set { _previousStates = value; }
        }

        public void OnStateCycle()
        {
            throw new NotImplementedException();
        }

        void OnEnable()
        {
            Controller.OnStateChange += OnStateChange;
        }

        void OnDisable()
        {
            Controller.OnStateChange -= OnStateChange;
        }

        private void OnStateChange(int newState)
        {

            if (NewStateIsValid(newState))
            {
                switch (newState)
                {
                    case (StartLevelState.CameraViewState):
                        {
                            LevelView.Instance.SwitchToCameraView(true);
                            break;
                        }
                    case (StartLevelState.SettingsViewState):
                        {
                            _previousState = CurrentState;
                            PreviousStatesStack.Push(CurrentState);
                            CurrentState = newState;
                            LevelView.Instance.SwitchToSettingsView(true);
                            break;
                        }
                    case (StartLevelState.GameModeSelect):
                        {
                            _previousState = CurrentState;
                            PreviousStatesStack.Push(CurrentState);
                            CurrentState = newState;
                            LevelView.Instance.SwitchToModeSelectView(true);
                            break;
                        }
                    case (StartLevelState.GameStart):
                        {
                            _previousState = CurrentState;
                            PreviousStatesStack.Push(CurrentState);
                            CurrentState = newState;
                            LevelView.Instance.SwitchToGameStartMenuView(true);
                            break;
                        }
                    case (StartLevelState.PlayerIconSelect):
                        {
                            _previousState = CurrentState;
                            PreviousStatesStack.Push(CurrentState);
                            CurrentState = newState;
                            LevelView.Instance.SwitchToPlayerIconSelect(true);
                            break;
                        }

                    case (StartLevelState.GoBackOneState):
                        {
                            GoBackOneState();
                            break;
                        }
                }
            }
        }

        private void GoBackOneState()
        {
            if (PreviousStatesStack.Count > 1)
            {
                int prevState = PreviousStatesStack.Pop();
                _previousState = PreviousStatesStack.Peek();
                OnStateChange(prevState);
            }
        }

        /// <summary>
        /// Checks if the new state is valid and can override the previous state
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
        private bool NewStateIsValid(int newState)
        {
            _previousState = CurrentState;
            _currentState = newState;
            switch (newState)
            {
                case (StartLevelState.CameraViewState):
                    {

                        break;
                    }
                case (StartLevelState.SettingsViewState):
                    {
                        break;
                    }
                case (StartLevelState.GameModeSelect):
                    {
                        break;
                    }
                case (StartLevelState.GameStart):
                    {
                        break;
                    }
                case (StartLevelState.PlayerIconSelect):
                    {
                        break;
                    }
            }
            return true;
        }
    }
}
