using UnityEngine;
public class savepoint : MonoBehaviour
{
    CircleCollider2D coll;
    public Transform passedSavePoints;
    public Sprite sp;
    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.AddComponent<CircleCollider2D>();
        SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
        spr.sprite = sp;
        spr.sortingOrder = 200;
        gameObject.AddComponent<autorenderpos>();
        coll.radius = 0.6f;
        coll.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            transform.parent = passedSavePoints;
            collision.transform.parent.gameObject.GetComponent<characterController>().setLSP(transform.position);
        }
    }
}
