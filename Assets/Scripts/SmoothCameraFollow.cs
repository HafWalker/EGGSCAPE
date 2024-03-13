using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // El objeto que la c�mara seguir�
    public float smoothSpeed = 0.125f; // La suavidad del seguimiento. Cuanto mayor sea el valor, m�s suave ser� el seguimiento.
    public Vector3 offset; // El desplazamiento relativo de la c�mara con respecto al objetivo

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}