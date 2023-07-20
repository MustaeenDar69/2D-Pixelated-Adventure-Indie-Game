using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBomb : MonoBehaviour
{
    private GameObject playerReference;
    private Animator animator;

    [SerializeField] private float health;

    private float damage = 50f;

    private Vector2 playerPos;
    private float safeDistance;
    public float moveSpeed;
    private bool walk;

    private void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        health = 15f;
        moveSpeed = 2f;
        walk = false;
    }

    private void Update()
    {
        playerPos = playerReference.transform.position;
        safeDistance = Vector2.Distance(transform.position, playerPos);

        Debug.Log(safeDistance);

        if (safeDistance <= 1.4f)
        {
            walk = false;
            animator.SetBool("fuse", true);
        }
        else
        {
            walk = true;
            animator.SetBool("fuse", false);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (walk)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sword")
        {
            health -= PlayerPrefs.GetFloat("normalSwordDmg");
            animator.Play("walking bomb hurt");

            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetFloat("linkHealth", PlayerPrefs.GetFloat("linkHealth") - damage);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        animator.SetBool("fuse", false);
    //    }
    //}

    private void Walk()
    {
        walk = true;
    }

    private void Blast()
    {
        PlayerPrefs.SetFloat("linkHealth", PlayerPrefs.GetFloat("linkHealth") - damage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
