using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float speed;


    Rigidbody rb;
    InputAction moveAction;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Debug.Log(moveValue);
        if (moveValue != Vector2.zero)
        {
            rb.linearVelocity = new Vector3(moveValue.x, 0, moveValue.y);
        }


    }
}
