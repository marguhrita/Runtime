using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController pc;
    private bool lastMovementState;

    void Update()
    {
        bool moving = pc.isMoving();
        if (lastMovementState != moving)
            if (moving)
            {
                animator.SetTrigger("Moving");
            }
            else
            {
                animator.SetTrigger("StoppedMoving");

            }
        lastMovementState = moving;

        if (pc.isJumping())
        {
            animator.SetTrigger("Jumping");
        }
    }

  
}
