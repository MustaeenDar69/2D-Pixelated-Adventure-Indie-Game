using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject playerReference;
    private Animator animator;

    [SerializeField] private float health;

    private float damage = 10f;

    private Vector2 playerPos;
    public float moveSpeed;
    private bool walk;

    private void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        health = 15f;
        moveSpeed = 1.7f;
        walk = false;
    }

    private void Update()
    {
        playerPos = playerReference.transform.position;

        
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
            animator.Play("zombie hurt");

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

    private void Walk()
    {
        walk = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
