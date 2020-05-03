using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;



public class LevelFour : MonoBehaviour
{
    private bool levelStarted = false;
    public Text timerText;
    public Text goalText;
    public Text instrectoinsText;
    public Text gameOverText;
    public float countDown = 60f;

    public GameObject instrectionsPanel;
    public Player player;
    public FirstPersonController fpsc;
    public static String levelInstrections = "Level 4!\n They are awake!\ntry to reach the flag without getting catch by the zombies!";
        

    // Start is called before the first frame update
    void Start()
    {

        player = fpsc.GetComponent<Player>();
        fpsc.enabled = false;
        gameOverText.text = "";
        goalText.text = ("0/1");
        instrectoinsText.text = levelInstrections + "\n to start press I!\nYou can move to the next level by press N";
    }

    // Update is called once per frame
    void Update()
    {


        if (!levelStarted)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                LevelStartReStart();
                levelStarted = true;
            }
            return;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            LevelStartReStart();
        }
        
        countDown -= Time.deltaTime;
        timerText.text = ((int)(countDown)).ToString();

        if (countDown < 0||player.getHealth()<200)
        {
            GameOver();
            return;
        }
        if (countDown < 5)
        {

            timerText.color = Color.red;
        }
       
    }

    private void GameOver()
    {
        fpsc.enabled = false;
        instrectionsPanel.active = true;
        gameOverText.text = "Game Over!";
        Invoke("RestartLevel", 5f);
    }
    public void LevelComplete()
    {
        levelStarted = false;
        instrectionsPanel.active = true;
        gameOverText.text = "Level Complete!";
        Invoke("NextLevel", 5f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level5");
    }
    private void LevelStartReStart()
    {
        if (instrectionsPanel.active)
        {
            instrectionsPanel.active = false;
            instrectoinsText.text = "";
            fpsc.enabled = true;
        }
        else
        {
            RestartLevel();
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level4");
    }
    
}
