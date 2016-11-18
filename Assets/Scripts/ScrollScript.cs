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
    public bool tenpet;
    private bool ScrollBottom = false;
    private bool ScrollTop = false;

    public bool ScrollingPoints; //give points for scrolling

    public bool Survival; //survive 10 spins
    public bool Drain10;

    public int ScrollingPointsAmmount = 100;

    public Vector2 MoveSpeed;

    private int score = 0;
    private int position = 0;

    private bool gameStopped;
    private bool gameNext;
    private bool gameRestart;
    public GameObject WinScreen;
    public GameObject LossScreen;

    private SpriteRenderer GreenLight;
    private SpriteRenderer HitLight;

    //private Rigidbody2D rb;

    //limiting framerate only works with vsync off
    void Awake()
    {
        Application.targetFrameRate = 40;
    }

    // Use this for initialization
    void Start ()
    {
        //rb = GetComponent<Rigidbody2D>();
        gameStopped = false;
        gameNext = false;
        gameRestart = false;

        GreenLight = GameObject.FindGameObjectWithTag("greenlight").GetComponent<SpriteRenderer>();
        HitLight = GameObject.FindGameObjectWithTag("hitlight").GetComponent<SpriteRenderer>();

        if (Drain10) score = 10;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameStopped && (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKey(KeyCode.UpArrow)) && position < 5) // upways
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
        if (!gameStopped && (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKey(KeyCode.DownArrow)) && position > -5) // downways
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
                GetComponent<AudioSource>().Play();
            }
        }

        if (FullScrolls)
        {


            if (position >= 5 && ScrollTop == false ) { ScrollTop = true; GreenLight.enabled = true; GetComponent<AudioSource>().Play(); }
            if (position <= -5 && ScrollBottom == false) { ScrollBottom = true; HitLight.enabled = true; GetComponent<AudioSource>().Play(); }

            if (ScrollTop && ScrollBottom)
            {
                ScrollTop = false;
                ScrollBottom = false;

                GreenLight.enabled = false;
                HitLight.enabled = false;

                AddScore(1);
            }
        }

        //scroll to start next level/restart
        if (gameNext && (Input.GetAxis("Mouse ScrollWheel") != 0f || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))//(Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (gameRestart)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    public void AddScore(int scoreToAdd)
    {

        if (!gameStopped)
        {
            score += scoreToAdd;
            if (score < 0) score = 0;//don't let score go below 0
            if (score > 10 && !ScrollingPoints && !Exact10) score = 10;// or above 10


            if (tenpet)
            {
                ScoreText.text = "";
                for(int i = 0; i < 10 - score; i++)
                ScoreText.text += "pet ";
            }
            else ScoreText.text = "Score: " + score.ToString();

            if (Exact10)
            {
                if (score > ScoreGoal) StartCoroutine(levelEnd("loss"));
                if (score == ScoreGoal) StartCoroutine(levelEnd("win"));
            }
            else if (Drain10 && score <= 0)
            {
                StartCoroutine(levelEnd("win"));
            }
            else if (score >= ScoreGoal && !Drain10)
            {

                StartCoroutine(levelEnd("win"));

            }
        }
    }

    IEnumerator levelEnd(string gameOverType)
    {
        //pause everything
        gameStopped = true;

        if (gameOverType == "win")
        {
            WinScreen.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            gameNext = true;
        }

        if(gameOverType == "loss")
        {
            LossScreen.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            gameRestart = true;
        }

        yield return null;
    }

}
