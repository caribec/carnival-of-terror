using UnityEngine;

public class MCController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform spriteTransform; // Drag MC_Sprite here in the Inspector

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = spriteTransform.GetComponent<Animator>();
    }

    void Update()
    {
        // Get left/right input (A/D or arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");

        // Flip the sprite based on direction
        if (movement.x < 0)
            spriteTransform.localScale = new Vector3(-1, 1, 1); // face left
        else if (movement.x > 0)
            spriteTransform.localScale = new Vector3(1, 1, 1);  // face right

        // Trigger attack
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            animator.Play("MC_Attack");
            isAttacking = true;
            Invoke(nameof(EndAttack), 0.5f); // Adjust time to match your animation
            return;
        }

        // Prevent animation override while attacking
        if (isAttacking)
            return;

        // Play move or idle animation
        if (movement.x != 0)
            animator.Play("MC_Move");
        else
            animator.Play("MC_Idle");
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }

    void EndAttack()
    {
        isAttacking = false;
    }
}