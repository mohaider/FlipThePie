using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic.IngameObjects.MusicButton;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level.Level1

{  public enum LevelOneStates
    {
        PieToTheFace=0, WinScreen, PlaybackNote, Playnotes, RegularGameState
    }
    
    [RequireComponent(typeof(LevelOne)),RequireComponent(typeof(LevelView))]
    public class LevelOneController:BaseLevelController
    {
        public delegate void OnStateChange(LevelOneStates newstate);

        public static event OnStateChange levelStateChangeEvent;
        private LevelOne _level1;
        private int m_maxDifficulty;
        private MusicButtonController m_buttonController;
      
        
        void Awake()
        {
           // GameDirector.Instance.AddLevel(this);
        }

        public void IncreaseDifficultyByOne()
        {
            int currentDifficulty = (int) Level1.LevelDifficulty;
            currentDifficulty++;
            if (currentDifficulty > m_maxDifficulty)
            {
                currentDifficulty = m_maxDifficulty;
            }
            Level1.LevelDifficulty = (EnumContainer.LevelDifficulty)currentDifficulty;
            MButtonController.IncrementMusicSpeed();
        }
        
        public void ChangeState(LevelOneStates newstate)
        {
            if (levelStateChangeEvent != null)
            {
                levelStateChangeEvent(newstate);
            }
        }

        void Start()
        {
            m_maxDifficulty = Enum.GetValues(typeof(EnumContainer.LevelDifficulty)).Cast<int>().Max();
           
        }
        public LevelOne Level1
        {
            get { return GetComponent<LevelOne>(); }
        }

        public MusicButtonController MButtonController
        {
            get
            {
                if (m_buttonController == null)
                {
                    m_buttonController =Tools.SceneObjectFinder<MusicButtonController>.FindGameObjectReturnT("MusicButtonController");
                }
                return m_buttonController;
            }
            set { m_buttonController = value; }
        }
    }
}
