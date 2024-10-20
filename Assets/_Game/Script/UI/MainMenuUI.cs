using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header(" Menu UI ")]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button quitBtn;

    public void EnterGame()
    {
        SoundManager.Ins.Stop("MainMenuMusic");

        PlayerPrefs.SetString("NameScene", "FSM Demo");
        UIManager.Ins.OpenUI<LoadingUI>();
        UIManager.Ins.GetUI<LoadingUI>().LoadLevel();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
