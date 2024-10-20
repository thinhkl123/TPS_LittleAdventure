using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestSpawnUI : MonoBehaviour
{
    private List<UICanvas> loadedUICanvases = new List<UICanvas>();
    public AssetLabelReference UIAssetLabel;

    async void Start()
    {
        var handle = Addressables.LoadAssetsAsync<GameObject>(UIAssetLabel, (GameObject obj) =>
        {
            Debug.Log(obj.name);
            loadedUICanvases.Add(obj.GetComponent<UICanvas>());
        });

        await handle.Task;

        Debug.Log("Final " + loadedUICanvases.Count);
    }
}
