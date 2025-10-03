using UnityEngine;
using Photon.Pun;



public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Jump Settings")]
    public float jump = 8f;


    private Rigidbody rb;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (photonView.IsMine && other.CompareTag("Suelo")) isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine && other.CompareTag("Suelo")) isGrounded = false;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Movimiento
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            Vector3 velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);
            
            rb.velocity = velocity;
            

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                isGrounded = false; 
            }
        }
    }
}



