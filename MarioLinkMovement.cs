using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MarioLinkMovement : MonoBehaviour
{
    private LinkHealthSystem linkHealthSystem;
    private LinkAnimations LinkAnimations;
    private Animator animator;
    private Rigidbody2D rb;

    private float moveSpeed;

    private Vector3 moveDir;
    private float moveX;
    private float moveY;

    private void Awake()
    {
        linkHealthSystem = GetComponent<LinkHealthSystem>();
        LinkAnimations = GetComponent<LinkAnimations>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = 3f;
    }

    private void Update()
    {

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("move horizontal", moveX);
        animator.SetFloat("move vertical", moveY);

        if (moveY > 0)
        {
            animator.SetFloat("facing direction", 1);
        }
        else if (moveY < 0)
        {
            animator.SetFloat("facing direction", 2);
        }
        else if (moveX > 0)
        {
            animator.SetFloat("facing direction", 3);
        }
        else if (moveX < 0)
        {
            animator.SetFloat("facing direction", 4);
        }


        if (!LinkAnimations.stateLock || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (moveDir.sqrMagnitude > 0)
            {
                LinkAnimations.CurrentState = LinkAnimations.PlayerStates.Move;
            }
            else
            {
                LinkAnimations.CurrentState = LinkAnimations.PlayerStates.Idle;
            }
        }
        else if (!LinkAnimations.canMove)
        {
            moveDir = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            LinkAnimations.CurrentState = LinkAnimations.PlayerStates.Hurt;
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
}
