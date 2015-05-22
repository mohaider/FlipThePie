using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Code.Camera;
using Assets.Resources.Code.UIElements;
using UnityEngine;


namespace Assets.Resources.Code.GameLogic.IngameObjects.MusicButton
{
    [RequireComponent(typeof(MusicButtonControllerView))]
    public class MusicButtonController: MonoBehaviour
    {
        private List<MusicButton> m_musicButtons;
        private Queue<MusicButton> m_musicDemoSequence =new Queue<MusicButton>();
        private Queue<int> m_correctSequence; //used to check the correct order
        private bool m_currentlyPlayingNote = false;
        private MusicButtonControllerView m_thisControllersView;
        public delegate void SequenceHandler();

        private float m_originalSpeed;
        private float m_assignedSpeed;
        public static event SequenceHandler CorrectSequenceEvent;
        public static event SequenceHandler IncorrectSequenceEvent;
        void Start()
        {
            m_assignedSpeed = m_originalSpeed = MusicButtons[0].m_Speed;
        }
        public void CreateNewSequence(int n)
        {
            m_correctSequence = new Queue<int>();
            while (n >= 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, MusicButtons.Count);
                MusicButton mb = MusicButtons[randomIndex];
                m_musicDemoSequence.Enqueue(mb);
                m_correctSequence.Enqueue(mb.Index);
                n--;
            }
        }

        public void MusicNoteSequenceView()
        {
            EnableAllMusicButtons(false);
        }

        public void SwitchToInGameView()
        {
            EnableAllMusicButtons(true);
        }

        void EnableAllMusicButtons(bool status)
        {
            foreach (MusicButton mb in MusicButtons)
            {
                mb.ButtonInteractivity(status);
            } 
        }

        public void IncrementMusicSpeed()
        {
            AssignedSpeed += 0.2f;
            ChangeSpeedForMusic(AssignedSpeed);
        }

        public void ResetMusicSpeed()
        {
            ChangeSpeedForMusic(m_originalSpeed);
        }

        private void ChangeSpeedForMusic(float newSpeed)
        {
            foreach (MusicButton mb in MusicButtons)
            {
                mb.m_Speed = newSpeed;
            }
        }
        public  IEnumerator StartPlayingSequence()
        {
            while (m_musicDemoSequence.Count > 0)
            {
                if (m_currentlyPlayingNote)
                {
                    yield return null;
                }
                else
                {
                    m_currentlyPlayingNote = true;//don't play multiple animations at once. 
                    MusicButton mb = m_musicDemoSequence.Dequeue();
                    mb.PlaySoundWhenDisabled(PlayNext);
                }
            }
            yield break;

        }

        public void ReplayLastSequence()
        {
            Queue<int> copyOfCorrectSequence = new Queue<int>();
            while ( m_correctSequence.Count >0)
            {
                int temp = m_correctSequence.Dequeue();
                m_musicDemoSequence.Enqueue(MusicButtons[temp]);
                copyOfCorrectSequence.Enqueue(temp);
            }
            m_correctSequence = copyOfCorrectSequence;
            StartCoroutine(StartPlayingSequence());
        }

        public void CheckCorrectNote(int i)
        {
           
            int top = m_correctSequence.Peek();
            if (top == i)
            {
                MusicButton mb = MusicButtons[i];
                mb.PlaySound();
                    m_correctSequence.Dequeue();
                if (m_correctSequence.Count == 0 && CorrectSequenceEvent !=null)
                {
                    EnableAllMusicButtons(false);
                    CorrectSequenceEvent();//trigger correct sequence event

                }
            }
            else if (top != i && IncorrectSequenceEvent!= null)
            {
                MusicButton mb = MusicButtons[i];
                mb.PlaySound();
                IncorrectSequenceEvent();
            }
           
        }

       
        void PlayNext()
        {
            m_currentlyPlayingNote = false;
        }
      
        public List<MusicButton> MusicButtons
        {
            get
            {
                if (m_musicButtons == null)
                {
                    m_musicButtons = new List<MusicButton>();
                  
                    MusicButton[] musicButtons = GetComponentsInChildren<MusicButton>();
                    for (int i = 0; i < musicButtons.Length; i++)
                    {
                        MusicButton mb = musicButtons[i];
                        int b = new int();
                        b = i;
                        mb.ThisButton.onClick.AddListener(() => CheckCorrectNote(b));
                        mb.SetIndex(i);
                        m_musicButtons.Add(mb);
                       
                    }
                   
                }

                return m_musicButtons;
            }
            set { m_musicButtons = value; }
        }

        public MusicButtonControllerView ThisControllersView
        {
            get
            {
                if (m_thisControllersView == null)
                {
                    m_thisControllersView = GetComponent<MusicButtonControllerView>();
                    if (m_thisControllersView == null) //if its still null
                    {
                        m_thisControllersView = gameObject.AddComponent<MusicButtonControllerView>();
                    }
                }
                return m_thisControllersView;
            }
            set { m_thisControllersView = value; }
        }

        public float OriginalSpeed
        {
            get { return m_originalSpeed; }
            set { m_originalSpeed = value; }
        }

        public float AssignedSpeed
        {
            get { return m_assignedSpeed; }
            set { m_assignedSpeed = value; }
        }
    }
  
}
