using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class typeCredits : MonoBehaviour
{
    public Text x;
    public float wt = 0.1f, blinkRate = 0.5f;
    string totype = "", typed = "";
    AudioSource sr;
    // Start is called before the first frame update
    void Start()
    {
        totype = x.text;
        sr = GetComponent<AudioSource>();
        x.text = "";
        StartCoroutine(brs());
        StartCoroutine(cb());
    }
    public void back()
    {
        SceneManager.LoadScene("uiEntry");
    }
    char eol = (char)0x5f;
    IEnumerator cb()
    {
        string[] split = totype.Split(new string[] { "#" }, System.StringSplitOptions.None);
        for (int c = 0; split.Length > c; c++)
        {
            yield return new WaitForSeconds(wt);
            typed += split[c];
            sr.Play();
        }
    }
    private void Update()
    {
        x.text = typed + (eol.ToString());
    }
    IEnumerator brs()
    {
        bool toggles = false;
        while (true)
        {
            yield return new WaitForSeconds(blinkRate);
            toggles = !toggles;
            if (toggles)
            {
                eol = (char)0x5f;/*0x5f = _*/
            }
            else
            {
                eol = (char)0x20;/*0x20 = Space*/
            }
        }
    }
}
