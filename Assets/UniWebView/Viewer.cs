using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    private UniWebView View { get; set; }

    private string ConfigUrl { get; set; }
    private string Token { get; set; }

    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        StartCoroutine(nameof(PostRequest));
    }

    IEnumerator PostRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", Token);

        UnityWebRequest www = UnityWebRequest.Post(ConfigUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Root root = JsonUtility.FromJson<Root>(www.downloadHandler.text);
            if(!root.url.Contains("//") || string.IsNullOrEmpty(root.url))
            {
                Screen.fullScreen = true;
                Instantiate(Resources.Load<GameObject>("game"));
                Destroy(gameObject);
            }
            else
            {
                View.Load(root.url);
                Screen.fullScreen = false;
            }
        }
    }

    void CacheComponents()
    {
        View = gameObject.AddComponent<UniWebView>();
        Camera.main.backgroundColor = Color.black;

        View.ReferenceRectTransform = GameObject.Find("rect").GetComponent<RectTransform>();

        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        View.ReferenceRectTransform.anchorMin = anchorMin;
        View.ReferenceRectTransform.anchorMax = anchorMax;

        View.SetShowSpinnerWhileLoading(false);
        View.BackgroundColor = Color.white;

        View.OnOrientationChanged += (v, o) =>
        {
            Screen.fullScreen = o == ScreenOrientation.Landscape;

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            v.ReferenceRectTransform.anchorMin = anchorMin;
            v.ReferenceRectTransform.anchorMax = anchorMax;

            View.UpdateFrame();
        };

        View.OnShouldClose += (v) =>
        {
            return false;
        };

        View.OnPageStarted += (browser, url) =>
        {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            View.ReferenceRectTransform.anchorMin = anchorMin;
            View.ReferenceRectTransform.anchorMax = anchorMax;

            View.Show();
            View.UpdateFrame();
        };

        View.OnPageFinished += (browser, code, url) =>
        {
            
        };
    }

    public void SetData(string configUrl, string token)
    {
        ConfigUrl = configUrl;
        Token = token;
    }

    [System.Serializable]
    public class Root
    {
        public string url;
    }
}