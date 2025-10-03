using UnityEngine;

public class ECController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform spriteTransform;

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
        // Trigger attack with K
        if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
        {
            animator.Play("EC_Attack");
            isAttacking = true;
            Invoke(nameof(EndAttack), 0.5f); // adjust to your animation length
            return;
        }

        // Block movement while attacking
        if (isAttacking)
            return;

        // Movement input (left/right arrow keys)
        movement.x = Input.GetAxisRaw("EC_Horizontal");

        // Flip sprite direction
        if (movement.x < 0)
            spriteTransform.localScale = new Vector3(1, 1, 1);
        else if (movement.x > 0)
            spriteTransform.localScale = new Vector3(-1, 1, 1);

        // Play move or idle animation
        if (movement.x != 0)
            animator.Play("EC_Move");
        else
            animator.Play("EC_Idle");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }

    void EndAttack()
    {
        isAttacking = false;
    }
}
