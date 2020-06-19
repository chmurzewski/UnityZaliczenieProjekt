using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class missCounter : MonoBehaviour
{
    public Text missText;
    public Text endScore;
    public Text pointText;
    public GameObject gameOverPanel;

    private int missed = 0;

    public void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        missText.text = "MISSED: " + missed.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "hardware")
        {
            missed += 1;
        }

        if (missed > 5)
        {
            endScore.text = pointText.text;
            gameOverPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}