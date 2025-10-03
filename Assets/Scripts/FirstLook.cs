using UnityEngine;

public class FirstLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f; // Sensibilidad del mouse
    public Transform playerBody; // Referencia al cuerpo del jugador
    public float verticalClamp = 90f; // Límite del ángulo vertical

    private float verticalRotation = 0f; // Rotación vertical acumulada

    void Start()
    {
        // Oculta el cursor y lo bloquea al centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Obtén la entrada del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Controla la rotación vertical (mirar arriba y abajo)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);

        // Aplica la rotación vertical
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Controla la rotación horizontal (mirar a los lados)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

