using UnityEngine;

public class UILookAtPlayer : SaiBehaviour
{

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
