using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //public
    public Rigidbody masterRb;
    public Vector3 axisOffsetMaster;
    public Transform axis;
    public float rotateSpeed; 
    public float maxRotateSpeed;
    public float zoomSpeed;
    public Vector2 maxZRotation;
    public bool debug;

    //private
    private float zoom;
    private Vector2 input;
    private Vector2 inputS;
    private float zRot;
    private Vector3 axisOffset;

    //Components
    private Rigidbody thisRb;



    // Use this for initialization
    void Start () {
        thisRb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        MapInputs();
        Zoom();
        Turn();
        WriteDebug();
    }

    private void MapInputs()
    {
        inputS = Input.mouseScrollDelta;
        input.x = Input.GetAxisRaw("Mouse X");
        input.y = Input.GetAxisRaw("Mouse Y");

        input = input * rotateSpeed;
        Mathf.Clamp(input.magnitude, 0, maxRotateSpeed); //tentative
    }

    private void Turn()
    {
        masterRb.transform.Rotate(0, input.x, 0);
        axisOffset = masterRb.transform.forward * axisOffsetMaster.z + masterRb.transform.up * axisOffsetMaster.y + masterRb.transform.right * axisOffsetMaster.x;
        axis.position = masterRb.position + axisOffset;
        axis.forward = -masterRb.transform.forward;

        zRot = zRot + input.y;
        Mathf.Clamp(zRot, maxZRotation.x, maxZRotation.y);
        axis.Rotate(zRot - axis.rotation.x, 0, 0); //resets vertical angle 
        
        thisRb.transform.forward = (-axis.forward);
        thisRb.transform.position = axis.position + axis.forward.normalized * zoom;
    }

    private void Zoom()
    {
        zoom = zoom + (inputS.y * zoomSpeed);
    }

    private void WriteDebug()
    {
        if (debug)
        {
            Debug.Log(axisOffset);
        }
    }
}
