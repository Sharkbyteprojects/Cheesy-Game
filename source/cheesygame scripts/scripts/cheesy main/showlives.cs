using UnityEngine;
[RequireComponent(typeof(displayText))]
public class showlives : MonoBehaviour
{
    displayText txtme;
    characterController chc;
    string txt = "";
    // Start is called before the first frame update
    void Start()
    {
        txtme = GetComponent<displayText>();
        txt = txtme.TextToDisplay;
        chc = GameObject.FindGameObjectWithTag("plid").GetComponent<characterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (txtme.di())
        {
            txtme.TextToDisplay = string.Format(txt, chc.lives, chc.score);
        }
    }
}
