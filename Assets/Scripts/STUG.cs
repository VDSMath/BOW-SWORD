using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STUG : MonoBehaviour, PlayerBaseAttack {

    [Header("Attack Parameters")]
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private float AttackRayLength = 3f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Attack()
    {
        sword.GetComponent<Animator>().SetTrigger("attack");

        Vector3 camForward = GetComponent<PlayerMovement>().cam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(GetComponent<PlayerMovement>().cam.transform.position, camForward, out hit, AttackRayLength))
        {
            if (hit.collider.gameObject.tag == "Attackable")
            {
                StartCoroutine(hit.collider.gameObject.GetComponent<Attackable>().Attacked());
            }
        }
    }

    public void Skill1()
    {

    }
    public void Skill2()
    {

    }
    public void Special()
    {

    }
}
