using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieWavesUI : MonoBehaviour
{
    public TMP_Text TimerText;
    public TMP_Text ZombieWaveText;
    public TMP_Text ZombiesLeftText;

    // Update is called once per frame
    void Update()
    {
        UpdateTextContents();
    }

    void UpdateTextContents()
    {
        TimerText.text = "Time Remaining: " + GameManager.Instance().getCurrentTime() + "s";
        ZombieWaveText.text = "Wave: " + GameManager.Instance().getCurrentZombieWave() + "/" + GameManager.Instance().getTotalZombieWaves();
        ZombiesLeftText.text = "Zombies Left In Wave: " + GameManager.Instance().getCurrentWaveZombieCount();
    }
}
