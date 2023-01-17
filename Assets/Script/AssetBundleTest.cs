using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/model");
        GameObject obj = assetBundle.LoadAsset<GameObject>("Cube");
        Instantiate(obj);
    }

    // Update is called once per frame
    void Update()
    {
    }
}