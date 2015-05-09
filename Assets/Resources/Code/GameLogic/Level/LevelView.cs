using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.UIElements;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level
{
    sealed public class LevelView: MonoBehaviour
    {
        private static LevelView _instance;
        private Dictionary<string,GameAnimatorController> _animationValues;
        private bool _currentlyPlayingAnimation = false;

  //todo get game animator controllers for all the game objects that uses animators

        public static LevelView Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = GameObject.FindGameObjectWithTag("StartLevel");
                    _instance = obj.GetComponent<LevelView>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public Dictionary<string, GameAnimatorController> AnimationValues
        {
            get
            {
                if (_animationValues == null)
                {
                    _animationValues = new Dictionary<string, GameAnimatorController>();
                    GameObject[] allAnimationsInSceneGameObjects = GameObject.FindGameObjectsWithTag("View");
                    for (int i = 0; i < allAnimationsInSceneGameObjects.Length; i++)
                    {
                        GameAnimatorController animationController =
                            allAnimationsInSceneGameObjects[i].GetComponent<GameAnimatorController>();
                        if (animationController == null)
                        {
                            Debug.Log("View " + allAnimationsInSceneGameObjects[i].name +
                                      " doesn't have a animation controller attached. Fix it");
                        }
                        else
                        {
                            _animationValues.Add(animationController.Name,animationController);
                        }
                    }
                }
                return _animationValues;
            }
            set { _animationValues = value; }
        }

        public void SwitchToCameraView(bool status)
        {
            //HideOtherViews
            StartCoroutine(WaitThenShowView("CameraView", 1.3f));
        }

        public void SwitchToSettingsView(bool status)
        {
            
        }

        public void SwitchToFlickToRollView(bool status)
        {
            
        }

        public void SwitchToNoRollStateView(bool status)
        {

        }
        public void SwitchToGameDialView(bool status)
        {

        }
        /// <summary>
        /// Hide other views beside the one passed in
        /// </summary>
        /// <param name="exceptThisOne"></param>
        private void HideOtherViews(string exceptThisOne)
        {
            foreach (KeyValuePair<string, GameAnimatorController> entry in AnimationValues)
            {
                if (exceptThisOne == entry.Key)
                    continue;
                else
                {
                    entry.Value.Show(false);
                }
            }
        }
        public void SwitchToModeSelectView(bool status)
        {

            StartCoroutine(WaitThenShowView("GameModeSelectView", 1.3f));
        }
        public void SwitchToPlayerIconSelect(bool status)
        {
            //PlayerIconSelect
            StartCoroutine(WaitThenShowView("PlayerIconSelect", 1.3f));
        }



        public void ShowMessage(string args, bool status)
        {
            
        }

        public void SwitchToGameStartMenuView(  bool p)
        {

            StartCoroutine(WaitThenShowView("StartGameView", 1.3f));
        }

        private IEnumerator WaitThenShowView(string selectedGameView, float waitTime)
        {
            while (_currentlyPlayingAnimation)
            {
                yield return null;
            }
            _currentlyPlayingAnimation = true;//don't play multiple animations at once. 
            HideOtherViews(selectedGameView);
            yield return new WaitForSeconds(waitTime);
            GameAnimatorController StartMenuController;
            if (AnimationValues.TryGetValue(selectedGameView, out StartMenuController))
                StartMenuController.Show(true);
            else
            {
                Debug.Log("Cannot find"+ selectedGameView+  "  in LevelView");
                throw new Exception("Cannot find" + selectedGameView + "  in LevelView");
            }

            _currentlyPlayingAnimation = false;
        }

        void Start()
        {
            
        }
    }
}
