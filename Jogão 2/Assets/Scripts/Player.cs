using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     private Rigidbody2D rig;
     private Animator anim;
    
    
    public float velocity;
    public float forceJump;
    private bool jumping;
    private bool doubleJump;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        Move();
    }    
    void Move()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += move * Time.deltaTime * velocity;
            if (Input.GetAxis("Horizontal") > 0f)
            {
                anim.SetBool("Running", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            if (Input.GetAxis("Horizontal") < 0f)
            {
                anim.SetBool("Running", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (Input.GetAxis("Horizontal") == 0f)
            {
                anim.SetBool("Running", false);
            }
        }
    void Jump()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            if (!jumping)
            {
                rig.velocity = Vector2.up * forceJump;
                doubleJump = true;
                anim.SetBool("Jumping", true);
            }
            else
            {
                if (doubleJump) 
                {
                     rig.velocity = Vector2.up * forceJump;
                     doubleJump = false; 
                }
             } 
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
            anim.SetBool("Jumping", false);
        }
        if (collision.gameObject.tag == "Danger")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Victory")
        {
            GameController.instance.ShowVictory();
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = true;
        }
    }
}

