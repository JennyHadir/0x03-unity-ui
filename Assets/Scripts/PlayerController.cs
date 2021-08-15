using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private int score;
    public int health;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 5;
    }

    // Update is called once per frame
    void Update()

    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += (move * speed * Time.deltaTime);
        if (health == 0)
        {
            SetLose();
            StartCoroutine(LoadScene(3));
            
        }
        if (Input.GetButton("Cancel"))
            SceneManager.LoadScene("menu");
    }

    // Increment the score value when the player touches an object tagged pickup
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Trap")
        {
            health--;
            SetHealthText();
        }
        if (other.gameObject.tag == "Goal")
        {
            SetWin();
            StartCoroutine(LoadScene(3));
        }
    }
    // Set the score text in the game
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Set the health text in the game
    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    // Set Win condition
    void SetWin()
    {
        
        winLoseText.color = Color.black;
        winLoseBG.color = Color.green;
        winLoseText.text = "You Win!";
        winLoseBG.gameObject.SetActive(true);
    }

    // Set lose condition
    void SetLose()
    {
        winLoseText.color = Color.white;
        winLoseBG.color = Color.red;
        winLoseText.text = "Game Over!";
        winLoseBG.gameObject.SetActive(true);
    }

    // Load next scene
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
