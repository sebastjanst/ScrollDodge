using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollScript : MonoBehaviour {

    public Text ScoreText;
    public GameObject Canal;

    private int score = 0;
    private int position = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && position < 4) // upways
        {
            transform.Translate(Vector3.up);
            position++;
            Camera.main.orthographicSize += 1;
            score++;
            ScoreText.text = "Score: " + score.ToString();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && position > -4) // downways
        {
            transform.Translate(Vector3.down);
            position--;
            Camera.main.orthographicSize -= 1;
            score++;
            ScoreText.text = "Score: " + score.ToString();
        }

        Canal.transform.Rotate(Vector3.forward * 1);
    }
}
