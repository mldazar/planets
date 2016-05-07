using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 150;

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
            Vector2 initialVelocity = transform.right * moveSpeed * horInput * Time.deltaTime;
            rigidBody.velocity = new Vector2(initialVelocity.x, rigidBody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}