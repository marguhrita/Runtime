using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    [SerializeField] PlayerInput _playerInput;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbital;
    private Vector2 scrollDelta;

    private float targetZoom;
    private float currentZoom;

    private InputAction _scroll;

    void Start(){

        Cursor.lockState = CursorLockMode.Locked;
        orbital = cam.GetComponent<CinemachineOrbitalFollow>();
        targetZoom = currentZoom = orbital.Radius;
        _scroll = _playerInput.actions["MouseZoom"];

    }



    void Update(){
        
        scrollDelta = _scroll.ReadValue<Vector2>();


        if (scrollDelta.y != 0)
        {
            if (orbital != null)
            {
                targetZoom = Mathf.Clamp(orbital.Radius - scrollDelta.y * zoomSpeed, minDistance, maxDistance);
            }
        }

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbital.Radius = currentZoom;
    }
}
