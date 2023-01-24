using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AssetBundleManager : SingleInstanceBase<AssetBundleManager>
{
    private AssetBundle mainAssetBundle = null;

    private AssetBundleManifest manifest = null;

    private Dictionary<string, AssetBundle> assetBundleDictionary = new Dictionary<string, AssetBundle>();

    public string pathUrl = Application.streamingAssetsPath + "/";

    public string mainAssetBundleName = "StandaloneWindows";

    public Object LoadResource(string assetBundleName, string resourceName)
    {
        LoadAssetBundle(assetBundleName);

        Object obj = assetBundleDictionary[assetBundleName].LoadAsset(resourceName);
        return obj is GameObject ? Instantiate(obj) : obj;
    }

    public Object LoadResource(string assetBundleName, string resourceName, Type type)
    {
        LoadAssetBundle(assetBundleName);

        Object obj = assetBundleDictionary[assetBundleName].LoadAsset(resourceName, type);
        return obj is GameObject ? Instantiate(obj) : obj;
    }
    
    public T LoadResource<T>(string assetBundleName, string resourceName) where T:Object
    {
        LoadAssetBundle(assetBundleName);

        T obj = assetBundleDictionary[assetBundleName].LoadAsset<T>(resourceName);
        return obj is GameObject ? Instantiate(obj) : obj;
    }

    private void LoadAssetBundle(string assetBundleName)
    {
        if (mainAssetBundle == null)
        {
            mainAssetBundle = AssetBundle.LoadFromFile(pathUrl + mainAssetBundleName);
            manifest = mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        AssetBundle assetBundle = null;
        string[] strs = manifest.GetAllDependencies(assetBundleName);
        for (int i = 0; i < strs.Length; i++)
        {
            if (!assetBundleDictionary.ContainsKey(strs[i]))
            {
                assetBundle = AssetBundle.LoadFromFile(pathUrl + strs[i]);
                assetBundleDictionary.Add(strs[i], assetBundle);
            }
        }

        if (!assetBundleDictionary.ContainsKey(assetBundleName))
        {
            assetBundle = AssetBundle.LoadFromFile(pathUrl + assetBundleName);
            assetBundleDictionary.Add(assetBundleName, assetBundle);
        }
    }

    public void UnLoad(string assetBundleName)
    {
        if (assetBundleDictionary.ContainsKey(assetBundleName))
        {
            assetBundleDictionary[assetBundleName].Unload(false);
            assetBundleDictionary.Remove(assetBundleName);
        }
    }

    public void ClearAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        assetBundleDictionary.Clear();
        mainAssetBundle = null;
        manifest = null;
    }
}