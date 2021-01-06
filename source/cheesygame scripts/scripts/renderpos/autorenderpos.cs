using UnityEngine;
using eaxis;

[RequireComponent(typeof(SpriteRenderer))]
public class autorenderpos : MonoBehaviour
{
    public float multiply = 100;
    public bool autoUpdate = false;
    SpriteRenderer thisrender;
    public axis2d axistolookfor = axis2d.x;
    float setting = 0f;
    void compute()
    {
        float caxis = 0f;
        switch (axistolookfor)
        {
            case (axis2d.x):
                caxis = transform.position.x; break;
            case (axis2d.y):
                caxis = transform.position.y; break;
        }
        int x = Mathf.RoundToInt(caxis * multiply - setting) * -1;
        thisrender.sortingOrder = x;
    }
    // Start is called before the first frame update
    void Start()
    {
        thisrender = GetComponent<SpriteRenderer>();
        setting = thisrender.sortingOrder;
        compute();
        if (!autoUpdate)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (autoUpdate)
        {
            compute();
        }
    }
}
