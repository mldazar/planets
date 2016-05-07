using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private float followThreshold = 1;

    private float distToTarget = 0;

    private void LateUpdate()
    {
        if ((distToTarget = Vector2.Distance(transform.position, target.position)) > followThreshold)
        {
            Vector3 lerpedPos = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
            transform.position = new Vector3(lerpedPos.x, lerpedPos.y, transform.position.z);
        }
    }
}