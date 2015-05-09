using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameratester : MonoBehaviour {

    public RawImage rawimage;
    void Start()
    {
        rawimage = GetComponent<RawImage>();
        WebCamTexture webcamTexture = new WebCamTexture();
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
}
