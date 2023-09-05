using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public void QuitGame()
    {
        // Quit the application (works in standalone builds)
        Application.Quit();

        // If you're in the Unity Editor, this line will stop the Play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
