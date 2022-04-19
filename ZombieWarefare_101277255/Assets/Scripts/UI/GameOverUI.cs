using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text conditionText;
    public TMP_Text timerText;
    public TMP_Text waveText;
    public TMP_Text totalZombiesKilledText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        conditionText.text = (GameManager.Instance().getHasPlayerWon()) ? "YOU SURVIVED!" : "YOU LOST!";
        timerText.text = "Last Wave Time Left: " + (int)GameManager.Instance().getCurrentTime() + "s";
        waveText.text = "Waves Completed: " + GameManager.Instance().getCurrentZombieWave() + "/" + GameManager.Instance().getTotalZombieWaves();
        totalZombiesKilledText.text = "Total Zombies Killed: " + GameManager.Instance().getTotalZombiesKilled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetsManager()
    {
        GameManager.Instance().ResetManager();
    }
}
