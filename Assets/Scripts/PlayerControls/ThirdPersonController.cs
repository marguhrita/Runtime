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

    [SerializeField] private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbital;
    private CinemachineInputAxisController camInput;
    private Vector2 scrollDelta;

    private float targetZoom;
    private float currentZoom;

    private InputAction _scroll;

    private bool cursorOn = true;

    void Start()
    {

        orbital = cam.GetComponent<CinemachineOrbitalFollow>();
        camInput = cam.GetComponent<CinemachineInputAxisController>();
        targetZoom = currentZoom = orbital.Radius;
        _scroll = _playerInput.actions["MouseZoom"];

    }



    void Update()
    {

        CursorManager();


        if (_scroll == null)
        {
            //Debug.LogError("MouseZoom action not found!");
            Debug.Log("Available actions:");
            foreach (var action in _playerInput.actions)
            {
                Debug.Log("- " + action.name);
            }
        }

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


    void CursorManager()
    {
        if (Mouse.current.rightButton.isPressed && cursorOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            camInput.enabled = true;
            cursorOn = false;
        }
        else if (! (Mouse.current.rightButton.isPressed || cursorOn)) // de morgans rule out in the wild no way
        {
            Cursor.lockState = CursorLockMode.None;
            camInput.enabled = false;
            cursorOn = true;
        }
}

}


