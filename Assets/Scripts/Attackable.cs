using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {

    [SerializeField]
    private Material[] bas = new Material[2];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Attacked()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mr.material = bas[1];
        yield return new WaitForSeconds(1);
        mr.material = bas[0];
    }
}
