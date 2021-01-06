using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class velocityOk : MonoBehaviour
{
    Rigidbody2D e;
    void Start()
    {
        e = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            e.velocity = new Vector2(e.velocity.x, 0f);
        }
    }
}
