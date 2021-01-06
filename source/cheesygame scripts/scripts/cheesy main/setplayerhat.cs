using UnityEngine;

public class setplayerhat : MonoBehaviour
{
    public Transform hat;
    public float ymov = 0.48f;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newpos = transform.position;
        newpos.y += ymov;
        hat.position = newpos;
    }
}
