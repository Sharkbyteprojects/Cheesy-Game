using UnityEngine;

public class playerinstantiate : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 r = transform.position;
        Instantiate(player, r, Quaternion.identity, transform);
    }
}
