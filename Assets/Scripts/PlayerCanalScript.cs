using UnityEngine;
using System.Collections;

public class PlayerCanalScript : MonoBehaviour {

    public float SpinSpeed = -1;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Rotate(new Vector3(0,0,SpinSpeed));
    }
}