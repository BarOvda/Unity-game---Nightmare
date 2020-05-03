using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;



public class LevelOne : MonoBehaviour
{
    bool[] tombFound;


    
    public Text timerText;
    public Text goalText;
    public Text instrectoinsText;
    public Text gameOverText;
    public float countDown = 55f;
    public static int tombsToWin = 2;
    public int tombsCount = 0;
    public GameObject instrectionsPanel;
    private bool levelStarted = false;
    public FirstPersonController fpsc;
    public static String levelInstrections = "Level 1!\n find and hit the ancient tomb that will light your way,\n" +
        " and the statue that facing your future";

    // Start is called before the first frame update
    void Start()
    {
        
        tombFound = new bool[tombsToWin];
        for (int i = 0; i < tombFound.Length; i++)
            tombFound[i] = false;

        fpsc.enabled = false;
        gameOverText.text = "";
        goalText.text = (tombsCount+ "/" + tombsToWin);
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
        if (countDown < 0)
        {
            GameOver();
            return;

        }
         if (countDown < 5)
        {

            timerText.color = Color.red;
        }

         if (tombsCount== tombsToWin)
        {
            LevelComplete();
        }
    }
    public void TombFound(int index)
    {
        if (!tombFound[index])
        {
            tombFound[index] = true;
            tombsCount++;
            goalText.text = (tombsCount + "/" + tombsToWin);
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
        SceneManager.LoadScene("Level2");
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
        SceneManager.LoadScene("Level1");
    }
}
