using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float maxRotation = 1f;
    [SerializeField] private Transform jumpCheckPosition;
    private Rigidbody rigidBody;
    private bool jumpPressed = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            Debug.Log("Jump Pressed");
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVelocity = transform.forward * vertical * maxSpeed;

        Quaternion rotaionQuaternion = Quaternion.Euler(0, horizontal, 0);

        Vector3 newVelocity = transform.forward * vertical * maxSpeed;
        newVelocity.y = rigidBody.velocity.y;
        rigidBody.velocity = newVelocity;

        if (jumpPressed && Physics.OverlapSphere(jumpCheckPosition.position, 0.1f, LayerMask.GetMask("Ground")).Length > 0)
        {
            rigidBody.AddForce(Vector3.up * 70, ForceMode.Impulse);
            jumpPressed = false;
            Debug.Log("Jump");
        }
        Debug.Log(Physics.OverlapSphere(jumpCheckPosition.position, 0.1f, LayerMask.GetMask("Ground")).Length);

        transform.Rotate(Vector3.up, horizontal * maxRotation * Time.deltaTime, 0f);
    }
}
