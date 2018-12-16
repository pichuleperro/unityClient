using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkAnimator : MonoBehaviour {


    Animator anim; // variable para gestionar animaciones
    NetWorkInput netWorkInput;

    void Start () {
        netWorkInput = this.gameObject.GetComponent<NetWorkInput>();
        anim = GetComponent<Animator>();
    }
	
	
	void Update () {

        MovAnim();
        Mirror();

    }
    //

    void MovAnim() // animaciones de movimiento 
   {
        if (  netWorkInput.bottonLeftPressed ||  netWorkInput.bottonRightPressed) {
            anim.SetBool("enMovimiento", true);
        }
        else {
            anim.SetBool("enMovimiento", false);
        }
    }

    void Mirror() // para que el objeto mire para el otro lado 
  {
        if (netWorkInput.bottonLeftPressed) {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (netWorkInput.bottonRightPressed) {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
