using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class loadbundleditem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("seasonbundle");
        if (go != null)
        {
            simplestore store = go.GetComponent<simplestore>();
            if (store != null)
            {
                GameObject[] gos = store.items.ToArray();
                int pos = Mathf.RoundToInt(Random.Range(0f, gos.Length));
                if (!(pos < gos.Length))
                {
                    pos = gos.Length - 1;
                }
                GetComponent<SpriteRenderer>().sprite = gos[pos].GetComponent<SpriteRenderer>().sprite;
                gameObject.AddComponent<autorenderpos>();
                Destroy(this);
            }
        }
    }
}
