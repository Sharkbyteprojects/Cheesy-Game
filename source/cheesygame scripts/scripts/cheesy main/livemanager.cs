using UnityEngine;

public class livemanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(axislinkage x in GetComponents<axislinkage>())
        {
            x.linkto = GameObject.FindGameObjectWithTag("lvm").transform;
        }
        Destroy(this);
    }
}
