using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class CharacterMov : MonoBehaviour {


    SocketIOComponent io;
    CharacterData characterData;
    NetWorkInput netWorkInput;


    Animator anim; // variable para gestionar animaciones

    float movX;
    float movLeft;
    float movRight;
    float movY;

    public float velocidad = 2f;
    float jumpSpeed; // potencia de salto
    bool onGround; // para verificar si el objeto esta topando suelo
    Rigidbody2D rb;
   

    private void Awake() {
        GameObject go = GameObject.Find("SocketIO");
        io = go.GetComponent<SocketIOComponent>();
      
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        characterData = this.gameObject.GetComponent<CharacterData>();
        netWorkInput = this.gameObject.GetComponent<NetWorkInput>();

            
    }
    private void Update()
    {

        //movLeft = netWorkInput.movLeft;
        //movRight = netWorkInput.movRight;
        movX = netWorkInput.movX;
        movY = netWorkInput.movY;

        if (characterData.isLocalPlayer) {
            // corregir comportamiento de input no deseado       
        Mirror();
        MovAnim(); 

        } // NetWorkInput

        
        
    }
   
    void FixedUpdate()
    {

      //  if (characterData.isLocalPlayer) { }
            Jump();
            Movement();
                  
    }

    void Mirror() // para que el objeto mire para el otro lado 
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
    void MovAnim() // animaciones de movimiento 
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("enMovimiento", true);
        }
        else
        {
            anim.SetBool("enMovimiento", false);
        }
    }
    void Jump() // salto
    {
        if (onGround)//verificamos si esta en tierra para poder saltar de nuevo
        {
            // recordar modificar este metodo ya que el input lo recibirá desde el servidor
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //jumpSpeed = 7; 
            //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            //}
            //    else
            //    {
            //        jumpSpeed = 0;
            //    }

            rb.AddForce(Vector2.up * movY, ForceMode2D.Impulse);
            CaidaRapida();
        }               
    }   
    void Movement()
    {
        // rectificar momiviento por comportamiento no deseado. para eso llamar nuevamente a rb.velocity pero con sus respectivos vectores.
        // movimiento

        rb.velocity = new Vector3(movX * velocidad, rb.velocity.y);

        // rb.velocity = new Vector3(movLeft * velocidad, rb.velocity.y);
        //rb.velocity = new Vector3(movRight * velocidad, rb.velocity.y);
    }
    void CaidaRapida() // efecto de caida , da mejor sensacion de caida
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

    private void OnCollisionStay2D(Collision2D coll) // para indicar si esta topando suelo 
    {
        if (coll.transform.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D coll) // para indicar si esta saliendo del suelo
    {
        if (coll.transform.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

   

}
