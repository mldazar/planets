using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private Transform GravityCenter = null;

    [SerializeField]
    private float gravityAmount = 9.81f;

    [SerializeField]
    private float jumpForce = 5;

    [SerializeField]
    private float margin = 0.05f;

    [SerializeField]
    private float raysDistance = 0.5f;

    [SerializeField]
    private LayerMask groundMask = 0;

    [SerializeField]
    private LayerMask playerMask = 0;

    private Vector2 gravity = Vector2.zero;
    private float horInput = 0;
    private Vector2 destination = Vector2.zero;

    // Use this for initialization
    private void Start()
    {
        gravity = new Vector2(0, -gravityAmount);
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, -transform.up, raysDistance, groundMask);
        Debug.DrawRay(transform.position, -transform.up, Color.yellow);
        Debug.DrawRay(groundInfo.point, groundInfo.normal, Color.red);
        //Debug.DrawLine(transform.position, GravityCenter.position, Color.green);
        if (groundInfo.transform == null || Vector2.Distance(groundInfo.point, transform.position) > margin)
        {
            destination = (Vector2)transform.position + gravity;
        }
        //else if (Vector2.Distance(groundInfo.point, transform.position) < 0)
        //{
        //    movingDir = -gravity;
        //}
        //else
        //{
        //    destination = transform.position;
        //}

        gravity = GravityCenter.position - transform.position;
        if (groundInfo.transform != null)
            transform.up = groundInfo.normal;
        else
            transform.up = -gravity;
        if ((horInput = Input.GetAxis("Horizontal")) != 0)
        {
            transform.Translate(transform.right * moveSpeed * horInput * Time.deltaTime, Space.World);
            Debug.DrawRay(transform.position, transform.right * moveSpeed * horInput, Color.green);
            //destination += (Vector2)transform.right * moveSpeed * horInput;

            //if (Mathf.Abs(Vector2.Dot(groundInfo.normal, transform.right)) > 0.5f)
            //transform.up = -gravity;// groundInfo.normal;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        //if (Vector2.Distance(transform.position, destination) > margin)
        //    transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime);// (Vector3)movingDir * Time.deltaTime;
    }
}