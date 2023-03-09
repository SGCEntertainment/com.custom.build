using UnityEngine;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        if (Config.IsClearBuild)
        {
            Object.Instantiate(Resources.Load<GameObject>("game"));
        }

        foreach (var sdk in Config.UsesSDKArray.GetUsesSDK())
        {
            sdk.Setup();
        }
    }
}
