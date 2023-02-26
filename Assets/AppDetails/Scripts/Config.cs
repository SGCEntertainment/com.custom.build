[System.Serializable]
public class Config
{
    public string ProductName;
    public string ApplicationIdentifier;

    public string bundleVersion;
    public int bundleVersionCode;

    public SDK[] UsesSDK;
}
