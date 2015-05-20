using UnityEngine;
using System.Collections;

public class vibrationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID
	    if (Input.touchCount > 0)
	    {
	        if (Input.GetTouch(0).phase == TouchPhase.Began)
	        {
	            Handheld.Vibrate();
	        }
	    }
#endif
	}
}
