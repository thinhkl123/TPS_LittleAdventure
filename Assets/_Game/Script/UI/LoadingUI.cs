using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : UICanvas
{
    [SerializeField] private Slider loadingSlider;

    public void LoadLevel()
    {
        string nameScene = PlayerPrefs.GetString("NameScene");
        StartCoroutine(LoadAsyncchronously(nameScene));
    }

    IEnumerator LoadAsyncchronously(string nameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
