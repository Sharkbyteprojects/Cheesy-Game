using UnityEngine;
using UnityEngine.UI;
public class displayText : MonoBehaviour
{
    [TextArea]
    public string TextToDisplay = "";
    Text Showit;
    GameObject objects;
    public RectTransform canvas;
    public GameObject toSpawn;
    public Color txtcolor = Color.black;
    void letShow()
    {
        if (objects == null)
        {
            objects = Instantiate(toSpawn, canvas.transform);
            Showit = objects.GetComponent<Text>();
            objects.name = TextToDisplay;
            Showit.color = txtcolor;
        }
        Vector3 cp = Camera.main.WorldToScreenPoint(transform.position);
        objects.transform.position = new Vector3(cp.x, cp.y, 0f);
        Showit.text = TextToDisplay;
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
