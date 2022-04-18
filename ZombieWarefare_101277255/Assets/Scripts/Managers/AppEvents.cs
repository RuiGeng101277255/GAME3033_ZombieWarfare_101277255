using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents
{
    public delegate void MouseCursorEnable(bool enabled);
    public delegate void GamePauseEnable(bool paused);

    public static event MouseCursorEnable MouseCursorEnabled;
    public static event GamePauseEnable GamePauseEnabled;

    public static void InvokeOnMouseCursorEnable(bool enabled)
    {
        MouseCursorEnabled?.Invoke(enabled);
    }

    public static void InvokeOnGamePauseEnable(bool paused)
    {
        GamePauseEnabled?.Invoke(paused);
    }
}
