using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public GameObject EnemyCanal;

    public int PointValue = -1;

    public bool Immortal = false;

    private ScrollScript TheScrollScript;

    // Use this for initialization
    void Start ()
    {
        TheScrollScript = FindObjectOfType<ScrollScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ouch!");
        TheScrollScript.AddScore(PointValue);

        if(!Immortal)
        Destroy(this.gameObject);
    }
}
