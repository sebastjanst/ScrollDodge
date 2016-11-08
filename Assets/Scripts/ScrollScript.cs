using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScrollScript : MonoBehaviour {

    public Text ScoreText;
    public bool restless = false;

    //gamemode setup
    public int ScoreGoal = 10;
    public bool Exact10; //must get an exact value of points

    public bool FullScrolls; //1 point for reaching each end once
    private bool ScrollBottom = false;
    private bool ScrollTop = false;

    public bool ScrollingPoints; //give points for scrolling

    public bool Survival; //survive 10 spins

    public int ScrollingPointsAmmount = 100;

    public Vector2 MoveSpeed;

    private int score = 0;
    private int position = 0;

    private bool gameStopped;

    //private Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        //rb = GetComponent<Rigidbody2D>();
        gameStopped = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameStopped && Input.GetAxis("Mouse ScrollWheel") > 0f && position < 5) // upways
        {
            transform.Translate(Vector3.up);
            //rb.MovePosition(rb.position + Vector2.up*50 * Time.fixedDeltaTime);
            position++;
            if(restless)
            Camera.main.orthographicSize += 1;

            if (ScrollingPoints)
            {
                AddScore(ScrollingPointsAmmount);
                ScoreText.text = "Score: " + score.ToString();
            }
        }
        if (!gameStopped && Input.GetAxis("Mouse ScrollWheel") < 0f && position > -5) // downways
        {
            transform.Translate(Vector3.down);
            //rb.MovePosition(rb.position + Vector2.down*50 * Time.fixedDeltaTime);
            position--;

            if(restless)
            Camera.main.orthographicSize -= 1;

            if (ScrollingPoints)
            {
                AddScore(ScrollingPointsAmmount);
                ScoreText.text = "Score: " + score.ToString();
            }
        }

        if (FullScrolls)
        {
            if (position == 5) ScrollTop = true;
            if (position == -5) ScrollBottom = true;

            if (ScrollTop && ScrollBottom)
            {
                ScrollTop = false;
                ScrollBottom = false;

                AddScore(1);
            }


        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Score: " + score.ToString();

        if(Exact10)
        {
            if (score > ScoreGoal) StartCoroutine(levelEnd("loss"));
            if (score == ScoreGoal) StartCoroutine(levelEnd("win"));
        }
        else if(score >= ScoreGoal)
        {

            StartCoroutine(levelEnd("win"));

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("ok");
    }

    IEnumerator levelEnd(string gameOverType)
    {
        //pause everything
        gameStopped = true;

        if (gameOverType == "win")
        {
            Debug.Log("You Win!");

            yield return new WaitForSeconds(1f);

            //load next level
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        if(gameOverType == "loss")
        {
            Debug.Log("Too many points!");

            yield return new WaitForSeconds(1f);

            //reload level
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

}
