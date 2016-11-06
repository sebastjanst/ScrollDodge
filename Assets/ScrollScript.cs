using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollScript : MonoBehaviour {

    public Text ScoreText;
    public GameObject Canal;

    public Vector2 MoveSpeed;

    private int score = 0;
    private int position = 0;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && position < 5) // upways
        {
            transform.Translate(Vector3.up);
            //rb.MovePosition(rb.position + Vector2.up*50 * Time.fixedDeltaTime);
            position++;
            //Camera.main.orthographicSize += 1;
            score++;
            ScoreText.text = "Score: " + score.ToString();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && position > -5) // downways
        {
            transform.Translate(Vector3.down);
            //rb.MovePosition(rb.position + Vector2.down*50 * Time.fixedDeltaTime);
            position--;
            //Camera.main.orthographicSize -= 1;
            score++;
            ScoreText.text = "Score: " + score.ToString();
        }

        Canal.transform.Rotate(Vector3.forward * 1);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("ok");
        Destroy(other.gameObject);
    }

}
