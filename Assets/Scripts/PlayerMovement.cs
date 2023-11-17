using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Transform;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public GameObject character;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootDelay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>(); 
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(horizontal, 0f);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        //animation
        if (Input.GetAxis("Horizontal") != 0f)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                character.transform.localScale = new Vector3(1.70256f, 1.70256f, 1.70256f);
            } else if (Input.GetAxis("Horizontal") < 0f)
            {
                character.transform.localScale = new Vector3(-1.70256f, 1.70256f, 1.70256f);

            }
            animator.SetBool("isRunning", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("Idle", true);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetBool("Idle", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Hit");
            Invoke("Shoot", shootDelay);

        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("Idle", true);
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
