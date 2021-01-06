using eaxis;
using UnityEngine;

public class axislinkage : MonoBehaviour
{
    public axis linkage = axis.x;
    public Transform linkto;
    // Update is called once per frame
    void LateUpdate()
    {
        if (linkage == axis.x) {
            transform.position = new Vector3(linkto.position.x, transform.position.y, transform.position.z);
        }
        else if (linkage == axis.y)
        {
            transform.position = new Vector3(transform.position.x, linkto.position.y, transform.position.z);
        }
        else {
            transform.position = new Vector3(transform.position.x, transform.position.y, linkto.position.z);
        }
    }
}
