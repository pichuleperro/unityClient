using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class SpawnerController : MonoBehaviour {




    /*
     * al momento de iniciar la escena , el spawner debe obtener todos los usuarios que se unan a la sala .
     * 
     * luego de almacenar a los usuarios de la partida , la camara comenzará siguiendo al spawner y al momento de instanciar al personaje la camara lo seguirá.
     * 
     * 
     */

    SocketIOComponent io;

    public float velocidadHelicoptero;
    public bool helicopteroEncendido;
    public GameObject go;
    Rigidbody2D rb;

    bool localPlayerInstantiated = false;


    
   

    private void Awake() {

        GameObject go = GameObject.Find("SocketIO");
        io = go.GetComponent<SocketIOComponent>();

    }
    void Start () {

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Contador());

        // EVENTOS NETWORKING

        io.On("Partida", (resp)=> {

            GameObject clone;

            clone = Instantiate(go, transform.position, transform.rotation);

            if(clone.GetComponent<CharacterData>()!= null) {
            clone.GetComponent<CharacterData>().id = NetWorkManager.QuitarComillas(resp.data.GetField("id").ToString());
            }

           


        });


	}
	
	
	void Update () {

       
        // antes de saltar , esperar cierto tiempo 
        if (Input.GetKeyDown(KeyCode.Space) && !localPlayerInstantiated ) {

            localPlayerInstantiated = true;
            io.Emit("Partida", NetWorkManager.InputJumpHelicopter() );
            
        }
       

	}
    private void FixedUpdate() {

        // para comenzar el juego el spawner se desplazara hacia la izquierda. cada determinado tiempo ,el mismo irá de izquierda a derecha repartiendo objetos
        if (helicopteroEncendido) {
            velocidadHelicoptero = -2;
             rb.velocity = new Vector3(velocidadHelicoptero, rb.velocity.y);
        }

    }

   

    IEnumerator Contador() {
        helicopteroEncendido = false;

        print("bienvenidos , la partida estara por comenzar");

        yield return new WaitForSeconds(5f);

        // mostrar el minimapa y luego comenzar a jugar 

        print("la partida comenzara en :");

        for (int i = 5; i > 0 ; i--) {
           
            yield return new WaitForSeconds(1f);
            print(i);
        }
        
        yield return new WaitForSeconds(1f);
        print("el helicoptero acaba de partir");
        helicopteroEncendido = true;

        
    }


 

}

//// emitir al servidor el momento en que caen los usuarios , el servidor debe buscar en la lista de salas el usuario y obtener su idRoom