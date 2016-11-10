using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupScript : MonoBehaviour {

    public int PointValue=1;
    public bool Infinipickup;

    public SpriteRenderer PickupTextImg;
    public List<Sprite> NumberSprites;

    private ScrollScript TheScrollScript;

    private SpriteRenderer GreenLight;

    // Use this for initialization
    void Start ()
    {
	    TheScrollScript = FindObjectOfType<ScrollScript>();
        if(PointValue <= NumberSprites.Count)
        PickupTextImg.sprite = NumberSprites[PointValue];

        GreenLight = GameObject.FindGameObjectWithTag("greenlight").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        TheScrollScript.AddScore(PointValue);
        GetComponent<AudioSource>().Play();
        GreenLight.enabled = true;

        if (!Infinipickup) Destroy(this.gameObject, 0.1f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GreenLight.enabled = false;
    }
}
