using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public void openscene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void exit()
    {
        Application.Quit();
    }
    public void destroy(Object e)
    {
        Destroy(e);
    }
    public Text score;
    public bool gover = false, lscore = false, edisp = true;
    void Start()
    {
        if (score == null)
        {
            return;
        }
        GameObject xs = GameObject.FindGameObjectWithTag("store");
        int values = 0;
        if (xs != null)
        {
            if (!lscore)
            {
                values = xs.GetComponent<store>().score;
                if (gover)
                {
                    xs.GetComponent<store>().score = 0;
                    if (values > xs.GetComponent<store>().bestScore)
                    {
                        xs.GetComponent<store>().bestScore = values;
                        PlayerPrefs.SetInt("bestScore", xs.GetComponent<store>().bestScore);
                        PlayerPrefs.Save();
                    }
                }
            }
            else
            {
                values = PlayerPrefs.GetInt("bestScore", xs.GetComponent<store>().bestScore);
            }
        }
        if (edisp || values != 0)
        {
            score.text = string.Format(score.text, values);
        }
        else
        {
            score.text = "";
        }
    }
}
