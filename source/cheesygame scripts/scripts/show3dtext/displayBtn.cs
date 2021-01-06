using UnityEngine;
using UnityEngine.UI;
public class displayBtn : MonoBehaviour
{
    GameObject objects;
    public RectTransform canvas;
    public GameObject toSpawn;
    void letShow()
    {
        if (objects == null)
        {
            objects = Instantiate(toSpawn, canvas.transform);
        }
        Vector3 cp = Camera.main.WorldToScreenPoint(transform.position);
        objects.transform.position = new Vector3(cp.x, cp.y, 0f);
    }
    bool display;
    public bool di()
    {
        return display;
    }
    private void LateUpdate()
    {
        Vector3 val = Camera.main.WorldToViewportPoint(transform.position);
        if (val.x < 1f && val.y < 1f && val.x > 0f && val.y > 0f)
        {
            display = true;
            letShow();
        }
        else
        {
            display = false;
            Destroy(objects);
        }
    }
}
