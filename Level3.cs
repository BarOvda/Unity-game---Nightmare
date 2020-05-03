using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System;
public class Level3 : MonoBehaviour
{
    public Text timerText;
    public Text goalText;
    public Text instrectoinsText;
    public Text gameOverText;
    public float countDown = 120f;
    
    public int currentCircle = 0;
    public GameObject instrectionsPanel;
    public GameObject[] circlesArray;
    private bool levelStarted = false;
    public FirstPersonController fpsc;
    public static String levelInstrections = "Level 3!\n They are coming!\n" +
        " Lets see how fast you can run...\n Pass all checkpoint in time!";

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < circlesArray.Length; i++)
            circlesArray[i].SetActive(false);
        circlesArray[0].active = true;
        fpsc.enabled = false;
        gameOverText.text = "";
        goalText.text = (currentCircle + "/" + circlesArray.Length);
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

        if (currentCircle== circlesArray.Length)
        {
            LevelComplete();
        }
    }
    public void circleFound()
    {

        circlesArray[currentCircle].SetActive(false);
        currentCircle++;
        if(currentCircle<circlesArray.Length)
            circlesArray[currentCircle].SetActive(true);
        goalText.text = (currentCircle + "/" + circlesArray.Length);
    }



    private void GameOver()
    {
        fpsc.enabled = false;
        instrectionsPanel.SetActive(true);
        gameOverText.text = "Game Over!";
        Invoke("RestartLevel", 5f);
    }
    
    private void LevelStartReStart()
    {
        if (instrectionsPanel.active)
        {
            instrectionsPanel.SetActive( false);
            instrectoinsText.text = "";
            fpsc.enabled = true;
            levelStarted = true;
        }
        else
        {
            RestartLevel();
        }
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
        SceneManager.LoadScene("Level4");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level3");
    }
}
