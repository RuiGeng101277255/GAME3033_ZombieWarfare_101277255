using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool cursorActive = true;
    public bool gamePaused = false;
    public static int currentZombieWaves = 0;
    public static int totalZombieWaves = 2;
    public static int currentWaveZombieCount = 0;
    public static float currentTime = 0.0f;
    public static float totalTimePerWave = 60.0f;
    public static int totalZombiesKilled = 0;
    public static bool hasWon = false;
    public static bool isInGame = true;

    public AudioSource dingSFX;

    private ZombieSpawnManager[] allZombieSpawners;

    private static GameManager instance;

    public static GameManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                return;
            }
        }
    }

    private void Update()
    {
        if (isInGame)
        {
            currentTime -= Time.deltaTime;

            if (currentWaveZombieCount <= 0)
            {
                currentWaveZombieCount = 0;
                SpawnNewWaveOfZombies();
            }
            else
            {
                if (currentTime <= 0.0f)
                {
                    GameOverCondition(false);
                }
            }
        }
    }

    void EnableCursor(bool isEnable)
    {
        cursorActive = isEnable;
        Cursor.visible = isEnable;
        Cursor.lockState = isEnable ? CursorLockMode.None: CursorLockMode.Locked;
    }

    void EnableGamePause(bool pause)
    {
        gamePaused = pause;
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    private void OnEnable()
    {
        AppEvents.MouseCursorEnabled += EnableCursor;
        AppEvents.GamePauseEnabled += EnableGamePause;
    }

    private void OnDisable()
    {
        AppEvents.MouseCursorEnabled -= EnableCursor;
        AppEvents.GamePauseEnabled -= EnableGamePause;
    }

    private void SpawnNewWaveOfZombies()
    {
        if (currentZombieWaves < totalZombieWaves)
        {
            if (dingSFX != null)
            {
                dingSFX.Play();
            }

            allZombieSpawners = FindObjectsOfType<ZombieSpawnManager>();

            int zombieToSpawnAtEachSite = currentZombieWaves * 5 + 10;

            foreach (ZombieSpawnManager spawner in allZombieSpawners)
            {
                spawner.spawnNumberOfZombies(zombieToSpawnAtEachSite);
                currentWaveZombieCount += zombieToSpawnAtEachSite;
            }

            currentTime = totalTimePerWave;
            currentZombieWaves++;
        }
        else
        {
            GameOverCondition(true);
        }
    }

    public void GameOverCondition(bool won)
    {
        SceneChanger sceneChange = FindObjectOfType<SceneChanger>();
        if (sceneChange != null)
        {
            hasWon = won;
            sceneChange.OpenScene("GameOverScene");
        }
        isInGame = false;
    }

    public void ResetManager()
    {
        cursorActive = true;
        gamePaused = false;
        currentZombieWaves = 0;
        currentWaveZombieCount = 0;
        currentTime = 0.0f;
        totalZombiesKilled = 0;
        isInGame = false;
    }

    public void addToTotalZombiesKilled()
    {
        currentWaveZombieCount = Mathf.Clamp(currentWaveZombieCount--, 0, currentWaveZombieCount);
        totalZombiesKilled++;
    }

    public void setIsInGame(bool ingame)
    {
        isInGame = ingame;
    }

    public bool getIsInGame()
    {
        return isInGame;
    }

    public int getCurrentTime()
    {
        return (int)currentTime;
    }

    public int getCurrentZombieWave()
    {
        return currentZombieWaves;
    }

    public int getTotalZombieWaves()
    {
        return totalZombieWaves;
    }

    public int getCurrentWaveZombieCount()
    {
        return currentWaveZombieCount;
    }

    public int getTotalZombiesKilled()
    {
        return totalZombiesKilled;
    }

    public bool getHasPlayerWon()
    {
        return hasWon;
    }
}
