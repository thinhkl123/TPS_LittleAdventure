using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : UICanvas
{
    [SerializeField] private Button resumeBtn;

    private void OnEnable()
    {
        ResetResumeBtn();
    }

    public void ResetResumeBtn()
    {
        resumeBtn.OnDeselect(null); 
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnClickResumeBtn()
    {
        GameManager.Ins.TooglePauseGame();
    }

    public void OnClickMainMenuBtn()
    {
        SoundManager.Ins.Stop("BGMusic");
        PlayerPrefs.SetString("NameScene", "MainMenu");
        UIManager.Ins.OpenUI<LoadingUI>();
        UIManager.Ins.GetUI<LoadingUI>().LoadLevel();
    }

    public void OnClickRestartBtn()
    {
        SoundManager.Ins.Stop("BGMusic");
        PlayerPrefs.SetString("NameScene", "FSM Demo");
        UIManager.Ins.OpenUI<LoadingUI>();
        UIManager.Ins.GetUI<LoadingUI>().LoadLevel();
    }
}
