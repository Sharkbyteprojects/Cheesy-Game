using UnityEngine.SceneManagement;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject storeprefab;
    public string target = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject x = GameObject.FindGameObjectWithTag("store");
            if (x == null)
            {
                x = Instantiate(storeprefab);
                DontDestroyOnLoad(x);
            }
            store storage = x.GetComponent<store>();
            storage.teleportedFrom = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(target);
        }
    }
}
