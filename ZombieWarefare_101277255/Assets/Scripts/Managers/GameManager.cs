using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool cursorActive = true;
    public bool gamePaused = false;
    public int currentZombieWaves = 0;
    public int totalZombieWaves = 3;
    public int currentWaveZombieCount = 0;
    public float currentTime = 0.0f;
    public float totalTimePerWave = 10.0f;

    public int totalZombiesKilled = 0;

    public bool hasWon = false;

    private bool isInGame = true;

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

    private void GameOverCondition(bool won)
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
        isInGame = true;
    }
}
