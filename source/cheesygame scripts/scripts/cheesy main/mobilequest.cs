using UnityEngine.UI;
using UnityEngine;

public class mobilequest : MonoBehaviour
{
    public GameObject MobilePrefab;
    public string savedState = "mobile?";
    bool enable = false;
    mobileinputcontroller linked;
    public Toggle mB;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mobileinput = GameObject.Find("MobileInput");
        if (mobileinput == null)
        {
            mobileinput = Instantiate(MobilePrefab);
        }
        int x = PlayerPrefs.GetInt(savedState, 0);
        enable = x == 1;
        linked = mobileinput.GetComponent<mobileinputcontroller>();
        linked.enabled = enable;
        mB.isOn = enable;
    }
    public void toggling()
    {
        enable = mB.isOn;
        linked.enabled = enable;
        int toggled = 0;
        if (enable)
        {
            toggled = 1;
        }
        PlayerPrefs.SetInt(savedState, toggled);
        PlayerPrefs.Save();
    }
}
