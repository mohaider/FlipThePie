using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.GameLogic.IngameObjects.MusicButton;
using UnityEngine;

namespace Assets.Resources.Code.GameLogic.Level.Level1
{
  
    public class LevelOne : MonoBehaviour
    {
        private LevelView m_levelView;
        private MusicButtonController m_musicButtonController;
        private EnumContainer.LevelDifficulty m_levelDifficulty = EnumContainer.LevelDifficulty.Easy;
        void OnEnable()
        {
            LevelOneController.levelStateChangeEvent += ChangeState;
        }

        void OnDisable()
        {
            LevelOneController.levelStateChangeEvent -= ChangeState;
        }

        void ChangeState(LevelOneStates newState)
        {
            switch (newState)
            {
                case LevelOneStates.PieToTheFace:
                    break;
                    case LevelOneStates.PlaybackNote:
                    break;
                    case LevelOneStates.Playnotes:
                    LevelView.SwitchToMusicNoteView(true,CreateNotes);
                    break;
                    case LevelOneStates.RegularGameState:
                    break;
                    case LevelOneStates.WinScreen:
                    break;
            }
        }

        void CreateNotes()
        {
            switch (LevelDifficulty)
            {
                case EnumContainer.LevelDifficulty.Easy:
                    MMusicButtonController.CreateNewSequence(4);
                    break;
                    case EnumContainer.LevelDifficulty.VeryEasy:
                    MMusicButtonController.CreateNewSequence(3);
                    break;
                    case EnumContainer.LevelDifficulty.Normal:
                    MMusicButtonController.CreateNewSequence(4);
                    break;
                    case EnumContainer.LevelDifficulty.Hard:
                    MMusicButtonController.CreateNewSequence(5);
                    break;
                    case EnumContainer.LevelDifficulty.VeryHard:
                    MMusicButtonController.CreateNewSequence(6);
                    break;
            }
            MMusicButtonController.StartPlayingSequence();

        }
        public LevelView LevelView
        {
            get { return LevelView.Instance; }
            set { m_levelView = value; }
        }

        public MusicButtonController MMusicButtonController
        {
            get
            {
                if (m_musicButtonController == null)
                {
                    m_musicButtonController = Tools.SceneObjectFinder<MusicButtonController>.FindGameObjectReturnT(
                        "MusicButtonController");
                }
                return m_musicButtonController;
            }
            
        }

        public EnumContainer.LevelDifficulty LevelDifficulty
        {
            get { return m_levelDifficulty; }
            set { m_levelDifficulty = value; }
        }
    }
}
