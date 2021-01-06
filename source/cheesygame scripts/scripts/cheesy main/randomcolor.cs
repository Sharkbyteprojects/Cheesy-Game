using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class randomcolor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        Color randColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        render.color = randColor;
    }
}
