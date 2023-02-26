using UnityEngine;

[System.Serializable]
public class Config
{
    public string productName;
    public string applicationIdentifier;
    public string bundleVersion;
    public int bundleVersionCode;

    public SDK[] UsesSDK;

    private static Config instance = null;
    public static Config Instance
    {
        get
        {
            if(instance == null)
            {
                instance = JsonUtility.FromJson<Config>(Resources.Load<TextAsset>("config").text);
            }

            return instance;
        }
    }

    public static string ProductName
    {
        get => Instance.productName;
    }

    public static string ApplicationIdentifier
    {
        get => Instance.applicationIdentifier;
    }

    public static string BundleVersion
    {
        get => Instance.bundleVersion;
    }

    public static int BundleVersionCode
    {
        get => Instance.bundleVersionCode;
    }

    public static SDK[] UsesSDKArray
    {
        get => Instance.UsesSDK;
    }
}