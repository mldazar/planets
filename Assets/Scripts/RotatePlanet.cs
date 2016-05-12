using System.Collections;
using UnityEngine;

public class RotatePlanet : MonoBehaviour

{
    [SerializeField]
    private float rotationSpeed = 100;

    private Camera mainCam = null;
    private float radius = 0;
    private float horInput = 0;
    private bool onWorldMap = true;
    private Coroutine switchView = null;

    // Use this for initialization
    private void Start()
    {
        radius = transform.localScale.y / 2;//GetComponent<CircleCollider2D>().radius;
        mainCam = Camera.main;
        SwitchWorldMap();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!onWorldMap && (horInput = Input.GetAxis("Horizontal")) != 0)
        {
            GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation + horInput * rotationSpeed * Time.deltaTime / radius);
            //transform.Rotate(0, 0, horInput * rotationSpeed * Time.deltaTime / radius, Space.Self);
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

        Quaternion initialRot = mainCam.transform.rotation;
        Vector3 initialPos = mainCam.transform.position;
        float initialSize = mainCam.orthographicSize;

        Quaternion targetRot = Quaternion.identity;
        Vector3 targetPos = new Vector3(0, 0, -10);
        float targetSize = 2;

        if (onWorldMap)
        {
            onWorldMap = false;
            targetPos = new Vector3(0, 3, -10);
        }
        else
        {
            onWorldMap = true;
            targetRot = transform.rotation;
            targetSize = 6;
        }

        while (percentDone < 1)
        {
            mainCam.transform.rotation = Quaternion.Lerp(initialRot, targetRot, percentDone);
            mainCam.transform.position = Vector3.Lerp(initialPos, targetPos, percentDone);
            mainCam.orthographicSize = Mathf.Lerp(initialSize, targetSize, percentDone);
            yield return new WaitForEndOfFrame();
            percentDone += Time.deltaTime;
        }
        mainCam.transform.rotation = targetRot;
        mainCam.transform.position = targetPos;
        mainCam.orthographicSize = targetSize;
    }
}