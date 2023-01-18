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

        AssetBundleCreateRequest assetBundleMain =
            AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + "StandaloneWindows");
        yield return assetBundleMain;

        AssetBundleRequest assetBundleManifest =
            assetBundleMain.assetBundle.LoadAssetAsync<AssetBundleManifest>("AssetBundleManifest");
        yield return assetBundleManifest;
        string[] strs = ((AssetBundleManifest)assetBundleManifest.asset).GetAllDependencies(file);
        AssetBundleCreateRequest[] assetBundleDependencies = new AssetBundleCreateRequest[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            assetBundleDependencies[i] =
                AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + strs[i]);
            yield return assetBundleDependencies;
        }

        AssetBundleRequest assetBundleRequest = assetBundleCreateRequest.assetBundle.LoadAssetAsync<GameObject>(asset);
        yield return assetBundleRequest;
        Instantiate(assetBundleRequest.asset);

        AssetBundleUnloadOperation assetBundleUnloadOperation = assetBundleCreateRequest.assetBundle.UnloadAsync(false);
        yield return assetBundleUnloadOperation;
        for (int i = 0; i < strs.Length; i++)
        {
            AssetBundleUnloadOperation assetBundleUnloadDependency =
                assetBundleDependencies[i].assetBundle.UnloadAsync(false);
            yield return assetBundleUnloadDependency;
        }
        AssetBundleUnloadOperation assetBundleUnloadMain = assetBundleMain.assetBundle.UnloadAsync(false);
        yield return assetBundleUnloadMain;
    }

    // Update is called once per frame
    void Update()
    {
    }
}