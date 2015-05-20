using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugLogger : MonoBehaviour
{

    private static DebugLogger m_instance;
    private UnityEngine.UI.Text m_text;
    public static DebugLogger Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("DebugText");
                m_instance = obj.GetComponent<DebugLogger>();
            }
            return m_instance;
        }
        set { m_instance = value; }
    }

    public Text DText
    {
        get
        {
            if (m_text == null)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("DebugText");
                m_text = obj.GetComponent<Text>();
            }
            return m_text;
        }
        set { m_text = value; }
    }


    public void ShowText(string text)
    {
        DText.text = text;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void DrawRayFromCameraToPoint(Vector3 end)
    {
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(Camera.main.transform.position),end,Color.red,10f);
        Debug.Log(end);
    }
}
