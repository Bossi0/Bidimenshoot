using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Wall Running Settings")]
    public float wallRunForce = 20f;
    public float wallRunUpForce = 5f;   // Fuerza hacia arriba
    public float wallRunDownForce = 5f; // Fuerza hacia abajo
    public float maxWallRunTime = 2f;
    public float wallJumpForce = 15f;  // Fuerza para el salto en la pared
    public float wallCheckDistance = 1f;
    public float minimumJumpHeight = 1.5f;

    [Header("Camera Effects")]
    public Camera playerCamera;
    public float tilt = 10f;

    private bool isWallRight = false;
    private bool isWallLeft = false;
    private bool isWallRunning = false;

    private Rigidbody rb;
    private Transform orientation;

    private float wallRunTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform;
    }

    void Update()
    {
        CheckForWall();
        HandleWallRun();

        // Detectar salto mientras está haciendo wall run
        if (isWallRunning && Input.GetKeyDown(KeyCode.Space))
        {
            PerformWallJump();
        }
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, wallCheckDistance, LayerMask.GetMask("wall"));
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, wallCheckDistance, LayerMask.GetMask("wall"));

        if (isWallRight)
        {
            Debug.Log("Pared detectada a la derecha.");
        }
        if (isWallLeft)
        {
            Debug.Log("Pared detectada a la izquierda.");
        }
    }

    private void HandleWallRun()
    {
        if (CanWallRun())
        {
            if (isWallRight || isWallLeft)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    private bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    private void StartWallRun()
    {
        isWallRunning = true;

        rb.useGravity = false;

        Vector3 wallRunDirection = orientation.forward; // Direccion basica hacia adelante

        // Ajustar direccion segun la pared
        if (isWallRight)
        {
            wallRunDirection += orientation.right;
        }
        else if (isWallLeft)
        {
            wallRunDirection -= orientation.right;
        }

        // Normalizar para evitar movimientos mas rapidos en diagonal
        wallRunDirection = wallRunDirection.normalized;

        // Aplicar movimiento diagonal hacia arriba o hacia abajo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            wallRunDirection += Vector3.up; // Movimiento hacia arriba
            Debug.Log("Moviendo hacia ARRIBA: " + wallRunDirection);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            wallRunDirection += Vector3.down; // Movimiento hacia abajo
            Debug.Log("Moviendo hacia ABAJO: " + wallRunDirection);
        }

        // Aplicar fuerza en la dirección calculada
        rb.AddForce(wallRunDirection * wallRunForce, ForceMode.Force);
        Debug.Log("Fuerza aplicada: " + wallRunDirection * wallRunForce);

        // Inclinar cámara
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(0, 0, isWallRight ? tilt : -tilt);
        }

        wallRunTimer += Time.deltaTime;

        if (wallRunTimer >= maxWallRunTime)
        {
            StopWallRun();
        }
    }

    private void StopWallRun()
    {
        isWallRunning = false;

        rb.useGravity = true;

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        wallRunTimer = 0;
    }

    private void PerformWallJump()
    {
        StopWallRun(); // Detenemos el wall run al saltar

        Vector3 wallJumpDirection = Vector3.zero;

        if (isWallRight)
        {
            wallJumpDirection = -orientation.right + Vector3.up; // Salto hacia la izquierda y arriba
        }
        else if (isWallLeft)
        {
            wallJumpDirection = orientation.right + Vector3.up; // Salto hacia la derecha y arriba
        }

        rb.AddForce(wallJumpDirection.normalized * wallJumpForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (orientation != null)
        {
            Gizmos.DrawLine(transform.position, transform.position + orientation.right * wallCheckDistance);
            Gizmos.DrawLine(transform.position, transform.position - orientation.right * wallCheckDistance);
        }
    }

}






