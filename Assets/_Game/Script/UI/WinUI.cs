using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : UICanvas
{
    public void OnClickRestartBtn()
    {
        SoundManager.Ins.Stop("BGMusic");
        PlayerPrefs.SetString("NameScene", "FSM Demo");
        UIManager.Ins.OpenUI<LoadingUI>();
        UIManager.Ins.GetUI<LoadingUI>().LoadLevel();
    }

    public void OnClickMainMenuBtn()
    {
        SoundManager.Ins.Stop("BGMusic");
        PlayerPrefs.SetString("NameScene", "MainMenu");
        UIManager.Ins.OpenUI<LoadingUI>();
        UIManager.Ins.GetUI<LoadingUI>().LoadLevel();
    }
}
