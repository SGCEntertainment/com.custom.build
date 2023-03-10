using UnityEngine;

public static class SDKUtility
{
    public static T[] GetUsesSDK<T>(SDK[] sdk) where T : SDKComponent
    {
        T[] usesSDK = new T[sdk.Length];

        for (int i = 0; i < usesSDK.Length; i++)
        {
            usesSDK[i] = sdk[i].name switch
            {
                #if USE_WEBVIEW
                    SDKType.Webview => (T)(SDKComponent)Resources.Load<Viewer>(SDKType.Webview)
                #endif
            };

            usesSDK[i].data = sdk[i].data;
        }

        return usesSDK;
    }
}
