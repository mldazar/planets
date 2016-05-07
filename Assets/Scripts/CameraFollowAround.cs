using System.Collections;
using UnityEngine;

public class CameraFollowAround : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private float followThreshold = 1;

    [SerializeField]
    private float lerpRate = 5;

    private bool onWorldMap = true;
    private Coroutine switchView = null;

    private void Start()
    {
        SwitchWorldMap();
    }

    private void Update()
    {
        if (!onWorldMap && (Quaternion.Angle(transform.rotation, target.rotation) > followThreshold))// distToTarget = Vector2.Distance(Camera.main.transform.position, target.position)) > followThreshold)
        {
            transform.rotation =
                Quaternion.Lerp(transform.rotation, target.rotation, lerpRate * Time.deltaTime);
            //Vector3 lerpedPos = Vector3.Lerp(transform.position, target.position, lerpRate * Time.deltaTime);
            //transform.position = new Vector3(lerpedPos.x, lerpedPos.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchWorldMap();
        }
    }

    public void SwitchWorldMap()
    {
        if (switchView != null)
        {
            StopCoroutine(switchView);
        }
        switchView = StartCoroutine(SwitchView());
    }

    private IEnumerator SwitchView()
    {
        float percentDone = 0;

        Quaternion initialRot = transform.rotation;
        Vector3 initialPos = transform.position;
        float initialSize = Camera.main.orthographicSize;

        Quaternion targetRot = Quaternion.identity;
        Vector3 targetPos = new Vector3(0, 0, -10);
        float targetSize = 1.5f;

        if (onWorldMap)
        {
            onWorldMap = false;
            targetRot = target.rotation;
        }
        else
        {
            onWorldMap = true;
            targetPos = new Vector3(0, -5, -10);
            targetSize = 6;
        }

        while (percentDone < 1)
        {
            transform.rotation = Quaternion.Lerp(initialRot, targetRot, percentDone);
            transform.position = Vector3.Lerp(initialPos, targetPos, percentDone);
            Camera.main.orthographicSize = Mathf.Lerp(initialSize, targetSize, percentDone);
            yield return new WaitForEndOfFrame();
            percentDone += Time.deltaTime;
        }

        transform.rotation = targetRot;
        transform.position = targetPos;
        Camera.main.orthographicSize = targetSize;
    }
}