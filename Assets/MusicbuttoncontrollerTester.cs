using UnityEngine;
using System.Collections;
using Assets.Resources.Code.GameLogic.IngameObjects.MusicButton;

public class MusicbuttoncontrollerTester : MonoBehaviour
{

    private MusicButtonController m_thisController;

    public MusicButtonController ThisController
    {
        get
        {
            if (m_thisController == null)
            {
                m_thisController = GetComponent<MusicButtonController>();
            }
            return m_thisController;
        }
        set { m_thisController = value; }
    }

    // Use this for initialization
    private void Start()
    {
        MusicButtonController.CorrectSequenceEvent += CorrectSequenceEvent;
        MusicButtonController.IncorrectSequenceEvent += IncorrectSequenceEvent;
    }

    void CorrectSequenceEvent()
    {
        print("Correct!");
    }

    void IncorrectSequenceEvent()
    {
        print("That is inorrect!");
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ThisController.MusicNoteSequenceView();
            ThisController.CreateNewSequence(10);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(ThisController.StartPlayingSequence());
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ThisController.ReplayLastSequence();
        }
    }
}
