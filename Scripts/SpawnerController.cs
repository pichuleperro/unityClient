using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {




    /*
     * al momento de iniciar la escena , el spawner debe obtener todos los usuarios que se unan a la sala .
     * 
     * luego de almacenar a los usuarios de la partida , la camara comenzará siguiendo al spawner y al momento de instanciar al personaje la camara lo seguirá.
     * 
     * 
     */

    public float velocidadHelicoptero ;
    public bool helicopteroEncendido;
    Rigidbody2D rb;


    void Start () {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Contador());
	}
	
	
	void Update () {
		
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
        //
        yield return new WaitForSeconds(1f);
        print("el helicoptero acaba de partir");
        helicopteroEncendido = true;

        
    }

}
