using UnityEngine;
using System.Collections;

public class DragRotateSlowDown : MonoBehaviour
{

    public float rotationSpeed = 10.0F;
    private float lerpSpeed = 1.0F;

    private Vector3 theSpeed;
    private Vector3 avgSpeed;
    private bool isDragging = false;
    private Vector3 targetSpeedX;
    public LayerMask SliceLayerMask;
    void OnMouseDown()
    {
        if(Input.GetMouseButton(0))
            isDragging = isTouchingObject();
    }
    private bool isTouchingObject()
    {

#if UNITY_EDITOR

        Vector3 touchPos1 = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPos1.z = 10;
        RaycastHit hit;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        //_ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit, 20f, SliceLayerMask))
        {

           
            return true;

        }

#endif
        return false;
    }
    void Update()
    {
        Vector3 n = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F);
       
        OnMouseDown();
        if (Input.GetMouseButton(0) && isDragging)
        {
            theSpeed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F);
            print(theSpeed);
            avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
        }
        else
        {
            if (isDragging)
            {
                theSpeed = avgSpeed;
                isDragging = false;
            }
            float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
        }

        transform.Rotate(Camera.main.transform.forward * theSpeed.x * rotationSpeed, Space.World);
        transform.Rotate(Camera.main.transform.forward * theSpeed.y * rotationSpeed, Space.World);
    }
}