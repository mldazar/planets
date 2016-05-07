using System.Collections;
using UnityEngine;

public class PlayerMovementAround : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private Transform GravityCenter = null;

    [SerializeField]
    private LayerMask groundMask = 1;

    [SerializeField]
    private float jumpForce = 5;

    private Rigidbody2D rigidBody = null;
    private float horInput = 0;

    // Use this for initialization
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((horInput = Input.GetAxis("Horizontal")) != 0)
        {
            rigidBody.velocity = ((Vector2)transform.right + rigidBody.velocity).normalized *
                moveSpeed * horInput * Time.deltaTime;

            //RaycastHit2D rayHit =
            //    Physics2D.Raycast(transform.position, -transform.up, 50, groundMask);
            //if (rayHit.normal.y != 0)
            //{
            //    //Debug.DrawRay(transform.position, rayHit.normal, Color.red);
            //    //Debug.DrawRay(transform.position, transform.up, Color.magenta);
            //    //transform.rotation = Quaternion.Lerp(transform.rotation,
            //    //    Quaternion.LookRotation(transform.forward, rayHit.normal), 0.2f);
            //    //float angle = Mathf.Atan2(1, Vector3.Dot(-transform.up, -rayHit.normal));
            //    //transform.Rotate(-transform.forward, angle);
            //    //transform.up = rayHit.normal;
            //}
            Physics2D.gravity = GravityCenter.position - transform.position;
            transform.up = -Physics2D.gravity;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(GravityCenter.position, 0.2f);
    }
}