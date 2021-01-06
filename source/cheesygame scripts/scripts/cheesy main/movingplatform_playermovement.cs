using UnityEngine;

public class movingplatform_playermovement : MonoBehaviour
{
    Transform otr;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            otr = collision.transform.parent.parent;
            collision.transform.parent.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent.parent = otr;
        }
    }
}
