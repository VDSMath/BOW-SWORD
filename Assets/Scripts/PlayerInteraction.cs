using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [Header("Grab Parameters")]
    [SerializeField]
    private float GrabRayLength = 2f;
    [SerializeField]
    private GameObject holdPosition;

    private GameObject heldObject;
    private bool holding;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Grab"))
        {
            Cast(0);
        }

        if (holding)
            Hold();
    }

    void Hold()
    {
        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPosition.transform.position, .98f);
        heldObject.transform.LookAt(GetComponent<PlayerMovement>().cam.transform);

        if (Input.GetButtonDown("Grab"))
        {
            Drop();
        }
    }

    void Cast(int mode)//0 = grab, 1 = attack
    {
        Vector3 camForward = GetComponent<PlayerMovement>().cam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(GetComponent<PlayerMovement>().cam.transform.position, camForward, out hit, GrabRayLength))
        {
            if (mode == 0)
            {
                if (hit.collider.gameObject.tag == "Grabable")
                {
                    StartCoroutine(Grab(hit.collider.gameObject));
                }
            }
        }
    }

    IEnumerator Grab(GameObject g)
    {
        yield return new WaitForEndOfFrame();
        g.GetComponent<Collider>().enabled = false;
        g.GetComponent<Rigidbody>().isKinematic = true;
        heldObject = g;
        holding = true;
    }

    void Drop()
    {
        Debug.Log("Drop");
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        //heldObject = null;
        holding = false;
    }
}
