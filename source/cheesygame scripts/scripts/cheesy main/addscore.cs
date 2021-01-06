using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class addscore : MonoBehaviour
{
    public int toAddScore = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("plid").GetComponent<characterController>().score += toAddScore;
            Destroy(gameObject);
        }
    }
}
