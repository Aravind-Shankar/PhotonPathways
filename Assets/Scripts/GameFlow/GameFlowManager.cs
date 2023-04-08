using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameFlowManager : MonoBehaviour
{
    public KeyCode EDITOR_pauseKey = KeyCode.Escape;
    public GameObject pauseCanvas;

    private bool _isPaused = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
#if UNITY_EDITOR
        //PauseGame();
#endif
    }

    public void PauseGame()
    {
        if (_isPaused)
            return;

        _isPaused = true;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!_isPaused)
            return;

        _isPaused = false;
        pauseCanvas.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
