using UnityEngine;

public class mobileinputcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    bool r = false, u = false, l = false, lstate = true;
    public new bool enabled = false;
    public GameObject[] child;
    private void Update()
    {
        if (enabled)
        {
            float dir = 0f;
            if (r)
            {
                dir = 1f;
            }
            else if (l)
            {
                dir = -1f;
            }
            characterController c = GameObject.FindObjectOfType<characterController>();
            if (c != null)
            {
                if (lstate)
                {
                    foreach (GameObject e in child)
                    {
                        e.SetActive(!false);
                    }
                }
                lstate = !true;
                c.jumpMobile(u);
                c.mobileSetMia(dir);
            }
            else
            {
                if (!lstate)
                {
                    foreach (GameObject e in child)
                    {
                        e.SetActive(false);
                    }
                }
                lstate = true;
            }
        }
        else
        {
            foreach (GameObject e in child)
            {
                e.SetActive(false);
            }
            lstate = true;
        }
    }
    void doti(int val, bool of)
    {
        switch (val)
        {
            case (0):
                r = of; break;
            case (1):
                l = of;
                break;
            case (3):
                u = of;
                if (of)
                {
                    characterController c = GameObject.FindObjectOfType<characterController>();
                    if (c != null)
                    {
                        c.dMobileJump();
                    }
                }
                break;
        }
    }
    public void sVT(int val)
    {
        doti(val, true);
    }
    public void sOffline(int val)
    {
        doti(val, false);
    }
}
