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
        conditionText.text = (GameManager.Instance().hasWon) ? "YOU SURVIVED!" : "YOU DIED";
        timerText.text = "Last Wave Time Left: " + (int)GameManager.Instance().currentTime + "s";
        waveText.text = "Waves Completed: " + GameManager.Instance().currentZombieWaves + "/" + GameManager.Instance().totalZombieWaves;
        totalZombiesKilledText.text = "Total Zombies Killed: " + GameManager.Instance().totalZombiesKilled;
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
