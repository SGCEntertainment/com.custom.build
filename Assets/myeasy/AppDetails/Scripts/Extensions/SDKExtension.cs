using UnityEngine;

public static class SDKExtension
{
    public static SDKComponent Setup(this SDKComponent component)
    {
        return Object.Instantiate(component);
    }

    public static SDKComponent[] GetUsesSDK(this SDK[] SDK)
    {
        return SDKUtility.GetUsesSDK<SDKComponent>(SDK);
    }
}
