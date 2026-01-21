using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    [SerializeField] CinemachineFollow target;

    [SerializeField] Vector3 startposition;

    [SerializeField] Vector3 endposition;

    [SerializeField] float blend;

    private float targetdirection;
    
    [SerializeField] float zoomSpeed;

    public void Update()
    {
        blend += (targetdirection*zoomSpeed);
        blend=Mathf.Clamp01(blend);
        target.FollowOffset = Vector3.Lerp(startposition, endposition, blend);
      
    }

    public void OnEnable()
    {
        InputManager.OnZoom += zoom;
    }

    public void OnDisable()
    {
        InputManager.OnZoom -= zoom;
    }

    private void zoom(float direction)
    {
        targetdirection=direction;
    }
}