using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAssetBundleList());
    }

    IEnumerator LoadAssetBundleList()
    {
        yield return LoadAssetBundleObject("model", "Cube");
        yield return LoadAssetBundleObject("model", "Sphere");
    }

    IEnumerator LoadAssetBundleObject(string file, string asset)
    {
        AssetBundleCreateRequest assetBundleCreateRequest =
            AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + file);
        yield return assetBundleCreateRequest;
        AssetBundleRequest assetBundleRequest = assetBundleCreateRequest.assetBundle.LoadAssetAsync<GameObject>(asset);
        yield return assetBundleRequest;
        Instantiate(assetBundleRequest.asset);
        AssetBundleUnloadOperation assetBundleUnloadOperation = assetBundleCreateRequest.assetBundle.UnloadAsync(false);
        yield return assetBundleUnloadOperation;
    }

    // Update is called once per frame
    void Update()
    {
    }
}