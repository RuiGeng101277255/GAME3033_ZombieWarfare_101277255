using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool cursorActive = true;
    public bool gamePaused = false;

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

    void EnableCursor(bool isEnable)
    {
        cursorActive = isEnable;
        Cursor.visible = isEnable;
        Cursor.lockState = isEnable ? CursorLockMode.None: CursorLockMode.Locked;
    }

    void EnableGamePause(bool pause)
    {
        gamePaused = pause;
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
}
