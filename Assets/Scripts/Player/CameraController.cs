// using System;
// using Unity.Cinemachine;
// using UnityEngine;
//
// public class CameraController : MonoBehaviour
// {
//
//
//     [SerializeField] CinemachineFollow target;
//
//     [SerializeField] Vector3 startposition;
//
//     [SerializeField] Vector3 endposition;
//
//     [SerializeField] float blend;
//
//     private float targetdirection;
//     
//     [SerializeField] float zoomSpeed;
//
//     public void Update()
//     {
//         blend += (targetdirection*zoomSpeed);
//         blend=Mathf.Clamp01(blend);
//         target.FollowOffset = Vector3.Lerp(startposition, endposition, blend);
//       
//     }
//
//     public void OnEnable()
//     {
//         InputManager.OnZoom += zoom;
//     }
//
//     public void OnDisable()
//     {
//         InputManager.OnZoom -= zoom;
//     }
//
//     private void zoom(float direction)
//     {
//         targetdirection=direction;
//     }
// }

//
// using Unity.Cinemachine;
// using UnityEngine;
//
// public class CameraController : MonoBehaviour
// {
//     [SerializeField] CinemachineFollow target;
//
//     [Header("Orbit settings")]
//     [SerializeField] float minAngle = 80f;   // widok z góry
//     [SerializeField] float maxAngle = 15f;   // widok zza postaci
//     [SerializeField] float distance = 6f;
//
//     [SerializeField] float zoomSpeed = 1.5f;
//
//     private float blend;           // 0–1
//     private float targetDirection;
//
//     void Update()
//     {
//         blend += targetDirection * zoomSpeed * Time.deltaTime;
//         blend = Mathf.Clamp01(blend);
//
//         float angle = Mathf.Lerp(minAngle, maxAngle, blend) * Mathf.Deg2Rad;
//
//         Vector3 offset = new Vector3(
//             0,
//             Mathf.Cos(angle) * distance,
//             -Mathf.Sin(angle) * distance
//         );
//
//         target.FollowOffset = offset;
//     }
//
//     void OnEnable()
//     {
//         InputManager.OnZoom += Zoom;
//     }
//
//     void OnDisable()
//     {
//         InputManager.OnZoom -= Zoom;
//     }
//
//     void Zoom(float direction)
//     {
//         targetDirection = direction;
//     }
// }


using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineFollow target;

    [Header("Start (Top View)")] [SerializeField]
    float startY = 25f;

    [SerializeField] float startZ = -2f;

    [Header("End (Front View)")] [SerializeField]
    float endY = 3f;

    [SerializeField] float endZ = -7f;

    [Header("Zoom")] [SerializeField] float zoomSpeed = 1.5f;

    [SerializeField] float orbitSpeed = 120f;

    private float blend; // 0–1
    private float targetDirection;

    float startAngle;
    float endAngle;
    float startDistance;
    float endDistance;
    float yaw;
    float orbitInput;

    void Start()
    {
        // Zamiana Y/Z na kąt i dystans
        startDistance = Mathf.Sqrt(startY * startY + startZ * startZ);
        endDistance = Mathf.Sqrt(endY * endY + endZ * endZ);

        startAngle = Mathf.Atan2(startY, -startZ);
        endAngle = Mathf.Atan2(endY, -endZ);
    }

    void Update()
    {
        // ZOOM (jak było)
        blend += targetDirection * zoomSpeed * Time.deltaTime;
        blend = Mathf.Clamp01(blend);

        float angle = Mathf.Lerp(startAngle, endAngle, blend);
        float dist = Mathf.Lerp(startDistance, endDistance, blend);

        // OBRÓT
        yaw += orbitInput * orbitSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        Vector3 offset = rotation * new Vector3(
            0f,
            Mathf.Sin(angle) * dist,
            -Mathf.Cos(angle) * dist
        );

        target.FollowOffset = offset;
    }
    

    void OnEnable()
    {
        InputManager.OnZoom += Zoom;
        InputManager.OnOrbit += Orbit;
    }

    void OnDisable()
    {
        InputManager.OnZoom -= Zoom;
        InputManager.OnOrbit -= Orbit;
    }

    void Zoom(float direction)
    {
        targetDirection = direction;
    }

    void Orbit(float input)
    {
        orbitInput = input;
    }
}