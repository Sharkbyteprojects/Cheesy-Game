using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class seasoncheeseloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject ef = GameObject.Find("cheesedOverride");
        if (ef != null)
        {
            simplestore c = ef.gameObject.GetComponent<simplestore>();
            if (c != null)
            {
                SpriteRenderer e = c.items.ToArray()[0].gameObject.GetComponent<SpriteRenderer>();
                if (e != null)
                {
                    SpriteRenderer me = GetComponent<SpriteRenderer>();
                    me.sprite = e.sprite;
                    me.color = new Color(0.9f, 0.9f, 0.9f);
                }
            }
        }
        Destroy(this);
    }
}
