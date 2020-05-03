using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class LevelFive : MonoBehaviour
{
    private bool levelStarted = false;
    public Text timerText;
    public Text goalText;
    public Text instrectoinsText;
    public Text gameOverText;
    public float countDown = 180f;

    public GameObject finishTrigger;
     
    public Animator righttDoor;
    public Animator leftDoor;
    public GameObject[] zombies;
    public GameObject instrectionsPanel;
    public Player player;
    public FirstPersonController fpsc;
    private int numberOfZombies;
    private int zombiesKilled = 0;
    private ArrayList zombisLogic;

    public static String levelInstrections = "Level 5!\n It is now or never!,\nKill all the zombies to get out from here!";

    // Start is called before the first frame update
    void Start()
    {
        numberOfZombies = zombies.Length;
        zombisLogic = new ArrayList();
        for(int i = 0; i < zombies.Length; i++)
        {
            zombisLogic.Add(zombies[i].GetComponent<AIZombie>());
        }
        player = fpsc.GetComponent<Player>();
        fpsc.enabled = false;
        gameOverText.text = "";
        goalText.text = (zombiesKilled+"/"+zombies.Length);
        instrectoinsText.text = levelInstrections + "\n to start press I!";
    }

    // Update is called once per frame
    void Update()
    {

        if (zombiesKilled == numberOfZombies)
        {
            leftDoor.SetBool("Finish", true);
            righttDoor.SetBool("Finish", true);
            if (Vector3.Distance(fpsc.transform.position,finishTrigger.transform.position)<0.5f)
                LevelComplete();
            return;
        }
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

        if (countDown < 0 || player.getHealth() <= 0)
        {
            GameOver();
            return;
        }
        if (countDown < 5)
        {

            timerText.color = Color.red;
        }
        foreach(AIZombie zombie in zombisLogic)
        {
            if (zombie.isDead)
            {
                zombisLogic.Remove(zombie);
                zombiesKilled++;
                goalText.text = (zombiesKilled + "/" + zombies.Length);
               
            }
        }
    }

    private void GameOver()
    {

        levelStarted = false;
        instrectionsPanel.active = true;
        gameOverText.text = "Game Over!";
        Invoke("RestartLevel", 5f);
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
        SceneManager.LoadScene("Level5");
    }
    public void LevelComplete()
    {
        levelStarted = false;
        instrectionsPanel.active = true;
        gameOverText.text = "You Escaped!";
        Invoke("NextLevel", 5f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
