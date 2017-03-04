using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

    public float force;
    public float maxVelocity;
    public Camera lakitu;

    private float inputX;
    private float inputY;
    private Vector3 cameraDirection;
    private Vector3 frictionForce;
    private Vector3 inputForce;
    private Rigidbody rb;
    private Vector3 cameraDirectionForInputY;
    private Vector3 cameraDirectionForInputX;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        StopIfStopped();
        rb.AddForce(moveForce());
	}

    Vector3 moveForce()
    {
        frictionForce = - rb.velocity.normalized * (rb.velocity.magnitude / maxVelocity);

        cameraDirectionForInputY  = lakitu.transform.forward;
        cameraDirectionForInputY.y = 0;
        cameraDirectionForInputY.Normalize();

        cameraDirectionForInputX = lakitu.transform.right;
        cameraDirectionForInputX.y = 0;
        cameraDirectionForInputX.Normalize();

        inputForce = (cameraDirectionForInputY * inputY + cameraDirectionForInputX * inputX);
        if (inputForce.magnitude > 1) inputForce.Normalize();

        return (frictionForce + inputForce) * force; 
    }

    void StopIfStopped()
    {
        if (rb.velocity.magnitude < maxVelocity / 30 && (Mathf.Abs(inputX) + Mathf.Abs(inputY)) < 0.1)
        {
            rb.velocity = rb.velocity * 0;
            inputX = 0;
            inputY = 0;
        }
    }
}
