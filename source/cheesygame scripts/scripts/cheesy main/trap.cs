using UnityEngine;

public class trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.transform.parent.gameObject.GetComponent<characterController>().kill();
        }
    }
}
