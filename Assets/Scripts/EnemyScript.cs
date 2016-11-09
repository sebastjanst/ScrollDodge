using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public GameObject EnemyCanal;

    public int PointValue = -1;

    public bool Immortal = false;

    private ScrollScript TheScrollScript;
    private SpriteRenderer HitLight;

    // Use this for initialization
    void Start ()
    {
        TheScrollScript = FindObjectOfType<ScrollScript>();
        HitLight = GameObject.FindGameObjectWithTag("hitlight").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ouch!");
        TheScrollScript.AddScore(PointValue);
        HitLight.enabled = true;

        if (!Immortal)
        Destroy(this.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        HitLight.enabled = false;
    }
}
