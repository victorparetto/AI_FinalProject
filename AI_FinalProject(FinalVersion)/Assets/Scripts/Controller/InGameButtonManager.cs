using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtonManager : MonoBehaviour {

    public void LoadScene(int change)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(change);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
