using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [HideInInspector]
    public Camera cam;

    public float mouseSpeed = 1f;

    [Header("Movement Parameters")]
    [SerializeField]
    private float jumpSpeed = 1f;
    [SerializeField]
    private float walkSpeed = 1.0F;

    private bool grounded = true;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;

        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Look();

        Move();
	}

    void Look()
    {
        Quaternion cr = cam.transform.rotation;
        

        Quaternion target = Quaternion.Euler(cr.eulerAngles.x - (Input.GetAxis("Mouse Y") * mouseSpeed), cr.eulerAngles.y + (Input.GetAxis("Mouse X") * mouseSpeed), 0);

        Vector3 tempAngles = target.eulerAngles;

        if (tempAngles.x > 180)
            tempAngles.x -= 360;

        float clampedX = Mathf.Clamp(tempAngles.x, -70, 70);
        tempAngles.x = clampedX;

        target = Quaternion.Euler(tempAngles);        
        
        cam.transform.rotation = target;

    }  
    
    void Move()
    {
        Jump();

        Vector3 camForward = cam.transform.forward;
        camForward = new Vector3(camForward.x, 0, camForward.z);

        Vector3 camSide = cam.transform.right;
        camSide = new Vector3(camSide.x, 0, camSide.z);

        GetComponent<Rigidbody>().velocity = (camForward * Input.GetAxisRaw("Vertical") * walkSpeed + camSide * Input.GetAxisRaw("Horizontal") * walkSpeed) + new Vector3(0, GetComponent<Rigidbody>().velocity.y);
    }

    void Jump()
    {
        Vector3 camUp = cam.transform.up;

        if (grounded && Input.GetButtonDown("Jump"))
            GetComponent<Rigidbody>().AddForce(camUp * jumpSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }   
    }
}
