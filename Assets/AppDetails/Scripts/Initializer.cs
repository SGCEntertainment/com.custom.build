using System;
using UnityEngine;

using Object = UnityEngine.Object;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        SDK[] usesSDK = Config.UsesSDKArray;

        if (Array.Exists(usesSDK, sdk => sdk.name == "webview"))
        {
            string data = Array.Find(usesSDK, sdk => sdk.name == "webview").data;
            Viewer viewer = Object.Instantiate(Resources.Load<Viewer>("webview"));
            viewer.SetData(data.Split(';')[0], data.Split(';')[1]);
        }
        else
        {
            Object.Instantiate(Resources.Load<GameObject>("game"));
        }
    }
}
