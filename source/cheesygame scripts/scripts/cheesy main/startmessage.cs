using UnityEngine.UI;
using UnityEngine;
using Unity.RemoteConfig;

[RequireComponent(typeof(Text))]
public class startmessage : MonoBehaviour
{
    Text dt;
    string txt = "";
    struct user { }
    struct users { }
    // Retrieve and apply the current key-value pairs from the service on Awake:
    void Awake()
    {
        dt = GetComponent<Text>();
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.SetCustomUserID(Random.value.ToString());
        txt = dt.text;
        dt.text = "...";
        ConfigManager.FetchConfigs(new user(), new users());
    }
    void load()
    {
        dt.text = string.Format(txt, ConfigManager.appConfig.GetString("infos"));
    }
    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                load();
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New settings loaded this session; update values accordingly.");
                load();
                break;
        }
    }
}
