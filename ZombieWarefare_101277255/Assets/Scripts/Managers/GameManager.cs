using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool cursorActive = true;

    void EnableCursor(bool isEnable)
    {
        cursorActive = isEnable;
        Cursor.visible = isEnable;
        Cursor.lockState = isEnable ? CursorLockMode.None: CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        AppEvents.MouseCursorEnabled += EnableCursor;
    }

    private void OnDisable()
    {
        AppEvents.MouseCursorEnabled -= EnableCursor;
    }
}
