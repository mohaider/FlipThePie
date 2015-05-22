using UnityEngine;
using System.Collections;

public class audiotesterscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.P))
	    {
	        GetComponent<AudioSource>().Play();
	    }
	}
}
