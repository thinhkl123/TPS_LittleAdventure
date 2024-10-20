using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        WaitToStart = 0,
        Playing = 1,
        Pause = 2,
        GameOver = 3,
    }

    public GameState State;

    void Start()
    {
        Time.timeScale = 1f;

        HideCursor();

        State = GameState.Playing;

        UIManager.Ins.CloseAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TooglePauseGame();
        }
    }

    public void TooglePauseGame()
    {
        if (State == GameState.GameOver)
        {
            return;
        }

        if (State == GameState.Playing)
        {
            SoundManager.Ins.Stop("BGMusic");
            SoundManager.Ins.Play("PauseMusic");
            Time.timeScale = 0f;
            State = GameState.Pause;
            ShowCursor();
            UIManager.Ins.OpenUI<PauseUI>();
            UIManager.Ins.GetUI<PauseUI>().ResetResumeBtn();
        }
        else if (State == GameState.Pause) 
        {
            SoundManager.Ins.Stop("PauseMusic");
            SoundManager.Ins.Play("BGMusic");
            Time.timeScale = 1f;
            State = GameState.Playing;
            HideCursor();
            UIManager.Ins.CloseUI<PauseUI>();

        }
    }

    private void HideCursor()
    {
        // Đưa con trỏ chuột về giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;

        // Ẩn con trỏ chuột
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        //Unlock Cursor
        Cursor.lockState = CursorLockMode.None;

        //Hien con tro chuot
        Cursor.visible = true;
    }

    public void WinGame()
    {
        SoundManager.Ins.Stop("BGMusic");
        State = GameState.GameOver;
        Invoke(nameof(ShowUIWin), 2f);
    }

    private void ShowUIWin()
    {
        SoundManager.Ins.Play("Win");
        ShowCursor();
        UIManager.Ins.OpenUI<WinUI>();
    }
    
    public void LostGame()
    {
        SoundManager.Ins.Stop("BGMusic");
        State = GameState.GameOver;
        Invoke(nameof(ShowUILost), 2f);
    }

    private void ShowUILost()
    {
        SoundManager.Ins.Play("Lose");
        ShowCursor();
        UIManager.Ins.OpenUI<LostUI>();
    }
}
