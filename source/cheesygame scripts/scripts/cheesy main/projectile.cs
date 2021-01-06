using UnityEngine;
using Unity.RemoteConfig;

public class projectile : MonoBehaviour
{
    struct user { }
    struct users { }
    public Vector2 speed;
    public Vector2 src;
    public float maxDist = 40f;
    public string playertag = "Player";
    Vector2 startpos;
    private void Start()
    {
        startpos = new Vector2(transform.position.x, transform.position.y);
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        src = speed;
        ConfigManager.FetchConfigs(new user(), new users());
    }
    void load()
    {
        float flu = ConfigManager.appConfig.GetFloat("projectileSpeed", 1f);
        speed = new Vector2(src.x * flu, src.y * flu);
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
    void Update()
    {
        Vector3 x = transform.position;
        if (Vector2.Distance(new Vector2(x.x, x.y), startpos) > maxDist)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 speedC = new Vector2(speed.x * Time.deltaTime, speed.y * Time.deltaTime);
        transform.position = new Vector3(x.x + speedC.x, x.y + speedC.y, x.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playertag)
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<characterController>().kill();
            Destroy(gameObject);
        }
    }
}
