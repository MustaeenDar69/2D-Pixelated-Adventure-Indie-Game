using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemy : MonoBehaviour
{
    private Animator animator;
     
    [SerializeField] private float health;

    private float damage = 10f; 

    private float moveSpeed;
    private bool moveRight;
    private bool moveUp;
    private float rightThreshold;
    private float leftThreshold;
    private float downThreshold;
    private float upThreshold;
    public float rightThresholdAddition;
    public float leftThresholdAddition;
    public float upThresholdAddition;
    public float downThresholdAddition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        moveSpeed = 2f;

        moveRight = true;
        rightThreshold = transform.position.x + rightThresholdAddition;
        leftThreshold = transform.position.x - leftThresholdAddition;
        upThreshold = transform.position.y + upThresholdAddition;
        downThreshold = transform.position.y - downThresholdAddition;
    }

    private void Update()
    {
        if (transform.position.x >= rightThreshold )
        {
            moveRight = false;
           
        }
        else if (transform.position.x <= leftThreshold)
        {
            moveRight = true;
        }
        if (transform.position.y <= downThreshold)
            {
                moveUp = true;
            }
        else if(transform.position.y >= upThreshold)
        {
            moveUp = false;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
        }
        if (moveUp)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sword")
        {
            health -= PlayerPrefs.GetFloat("normalSwordDmg");
            animator.Play("crab hurt");

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

    private void Die()
    {
        Destroy(gameObject);
    }
}
