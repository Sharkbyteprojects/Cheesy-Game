using UnityEngine;
using Unity.RemoteConfig;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class loadseason : MonoBehaviour
{
    public string mainScene = "uiEntry", newName = "seasonbundle";
    public Text loadTxt;
    struct user { }
    struct users { }
    public bool enableoverride = true;
    void setT(string t)
    {
        if (loadTxt != null)
        {
            loadTxt.text = string.Format("Loading: {0}", t);
        }
        Debug.Log(t);
    }
    bool loadcomplete = false;
    bool dd = false;
    // Start is called before the first frame update
    void Awake()
    {
        setT("Initialising");
        //DOLOAD
        DontDestroyOnLoad(gameObject);
        setT("Fetch Remote Config");
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.SetCustomUserID(Random.value.ToString());
        ConfigManager.FetchConfigs(new user(), new users());
    }
    bool ll = false;
    private void Update()
    {
        if (loadcomplete && !ll)
        {
            setT("UI");
            SceneManager.LoadScene(mainScene);
            ll = true;
        }
        if (dd)
        {
            Debug.Log("Should Destroy");
            Destroy(gameObject);
        }
    }
    bool onerun = false;
    IEnumerator x(string fn)
    {
        //try

        setT("DownLoad Bundle");
        UnityEngine.Networking.UnityWebRequest request
        = UnityWebRequestAssetBundle.GetAssetBundle(fn, 0);
        yield return request.SendWebRequest();
        if (!request.isHttpError)
        {
            try
            {
                setT("Load Bundle");
                AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
                GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>("bundled");
                if (prefab != null)
                {
                    GameObject ipref = Instantiate(prefab);
                    ipref.name = newName;
                    DontDestroyOnLoad(ipref);
                    string txteL = myLoadedAssetBundle.LoadAsset<TextAsset>("names").text;
                    Debug.Log(txteL);
                    //System.IO.File.WriteAllText("./text.txt", txteL);
                    string[] tx = txteL.Split(new string[] { ((char)0x0D).ToString()/*WINDOWS file ending*/ + "\n", "\n" }, System.StringSplitOptions.None);
                    simplestore e = ipref.AddComponent<simplestore>();
                    foreach (string l in tx)
                    {
                        if (l != "")
                        {
                            Debug.Log(string.Format("Item: {0}", l));
                            GameObject chi = ipref.transform.Find(l).gameObject;
                            e.items.Add(chi);
                            chi.SetActive(false);
                        }
                    }
                    //ipref.SetActive(false);
                }
                GameObject cheesys = myLoadedAssetBundle.LoadAsset<GameObject>("cheeseoverride");
                if (cheesys != null)
                {
                    setT("Make new Cheese");
                    GameObject nitem = Instantiate(cheesys);
                    nitem.name = "cheesedOverride";
                    //nitem.SetActive(false);
                    Transform child = nitem.transform.Find("cheese");
                    if (child != null)
                    {
                        nitem.AddComponent<simplestore>().items.Add(child.gameObject);
                        setT("Cheese DDoL");
                        child.gameObject.SetActive(false);
                        DontDestroyOnLoad(nitem);
                    }
                }

            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
                loadcomplete = true;
            }
        }
        loadcomplete = true;
        dd = true;
    }
    void loadfile(string fn)
    {
        if (!onerun)
        {
            StartCoroutine(x(fn));
            onerun = true;
        }
    }
    void load()
    {
        setT("SeasonAssetpath");
        string overr = "";
        bool evarl = false;
        if (enableoverride)
        {
            if (File.Exists("seasonoverride.txt"))
            {
                string overrs = File.ReadAllText("seasonoverride.txt");
                overr = Regex.Replace(overrs, @"[^a-zA-Z0-9]", string.Empty);
                evarl = true;
                Debug.Log(overr);
            }
        }
        string seas = ConfigManager.appConfig.GetString("season", PlayerPrefs.GetString("season", ""));
        if (evarl)
        {
            seas = overr;
        }
        if (seas != "")
        {
            PlayerPrefs.SetString("season", seas);
            PlayerPrefs.Save();
            string uripath = string.Format(ConfigManager.appConfig.GetString("seasonpattern", "https://github.com/Sharkbyteprojects/Cheesy-Game/raw/master/seasonbundles/{0}.season"), seas); setT("Download Bundle");
            loadfile(uripath);
        }
        else
        {
            loadcomplete = true;
            dd = true;
        }
    }
    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session; using default values.");
                load();
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
