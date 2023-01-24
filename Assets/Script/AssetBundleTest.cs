using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj1 = (GameObject)AssetBundleManager.GetInstance().LoadResource("model", "Cube");
        GameObject obj2 =
            (GameObject)AssetBundleManager.GetInstance().LoadResource("model", "Cube", typeof(GameObject));
        GameObject obj3 = AssetBundleManager.GetInstance().LoadResource<GameObject>("model", "Cube");

        obj1.transform.position = new Vector3(-1, 0, 0);
        obj2.transform.position = new Vector3(0, 0, 0);
        obj3.transform.position = new Vector3(1, 0, 0);
    }
}