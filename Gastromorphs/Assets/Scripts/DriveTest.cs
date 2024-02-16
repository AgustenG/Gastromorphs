using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DriveTest : MonoBehaviour
{
    string URI;
    string assetName;

    // Start is called before the first frame update
    void Start()
    {
        URI = "1s0qwUYTt_UsyG1lBw0N3nnerYxByByHl";
        assetName = "burger.fbx";
        StartCoroutine(DownloadAsset());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DownloadAsset()
    {
        GameObject test = null;

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://firebasestorage.googleapis.com/v0/b/gastromorphs.appspot.com/o/burger.fbx?alt=media&token=2c016173-5d44-43fe-9adc-ba6229b4bc97"))

        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Get downloaded asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                test  = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        InstantiateAssetBundle(test);
    }

    void InstantiateAssetBundle(GameObject obj)
    {
        if (obj != null)
        {
            GameObject instance = Instantiate(obj);
            instance.transform.position = Vector3.zero;
        }
        else Debug.Log("isNull");
    }
}
