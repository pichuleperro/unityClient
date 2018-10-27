using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMov : MonoBehaviour {

    Animator anim;
    public GameObject cam;
    public float velocidad = 2f;
    float jumpSpeed;
    bool onGround;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {    
        Mirror();
        MovAnim(); 
    }
   
    void FixedUpdate()
    {
        Jump();
        Movement();         
    }

    void Mirror()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void MovAnim()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            anim.SetBool("enMovimiento", true);
        }
        else
        {
            anim.SetBool("enMovimiento", false);
        }
    }
    void Jump()
    {
        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
            jumpSpeed = 7;
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
                else
                {
                    jumpSpeed = 0;
                }
                CaidaRapida();
        }               
    }   
    void Movement()
    {
        // movimiento
       rb.velocity = new Vector3(Input.GetAxis("Horizontal") * velocidad, rb.velocity.y);
    }
    void CaidaRapida()
    {
        if(rb.velocity.y<0){
            rb.gravityScale = 3.5f;
        }
        else if( rb.velocity.y > 0 && onGround){
            rb.gravityScale = 2;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

}
