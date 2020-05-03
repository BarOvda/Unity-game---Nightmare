using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class LevelTwo1 : MonoBehaviour
{
    public Text timerText;
    public Text gravesText;
    public Text instrectoinsText;
    public Text gameOverText;
    public float countDown = 40f;
    public static int gravesToWint = 10;
    public int gravesCount = 0;
    public GameObject instrectionsPanel;
    private bool levelStarted = false;
    public FirstPersonController fpsc;
    public static String levelInstrections = "Level 2!\n you need to destroy at least " + gravesToWint + " grave stones to win!";
       
    // Start is called before the first frame update
    void Start()
    {
        fpsc.enabled = false;
        gameOverText.text = "";
        gravesText.text = (gravesCount + "/" + gravesToWint);
        instrectoinsText.text = levelInstrections+ "\n to start press I!\nYou can move to the next level by press N";
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
        timerText.text =((int)(countDown)).ToString();
        if (countDown < 0)
        {
            GameOver();
            return;
            
        } if (countDown < 5)
        {
            
            timerText.color = Color.red;
        }

        if (gravesCount == gravesToWint)
        {
            LevelComplete();
        }
        
    }

    private void GameOver()
    {
        fpsc.enabled = false;
        instrectionsPanel.active = true;
        gameOverText.text="Game Over!";
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
        SceneManager.LoadScene("Level3");
    }
    public void GraveHit()
    {
        gravesCount++;
        gravesText.text = (gravesCount + "/" + gravesToWint);
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
        SceneManager.LoadScene(2);
    }
}
