using UnityEngine;

public class WeaponFollowCamera : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cameraTrf;                         // Camara en 1° persona
    public Transform weaponTrf;                         // Arma dentro del PLAYER

    void LateUpdate()
    {

        if (cameraTrf != null && weaponTrf != null)
        {
            weaponTrf.rotation = cameraTrf.rotation;
        }

    }
}
