using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

    public string id;
    public bool isLocalPlayer;

   
    void Start () {
        if (id == NetWorkManager.idSession) {
            isLocalPlayer = true;
        }
        else {
            isLocalPlayer = false;
        }
    }
	
	
	void Update () {
		
	}
}
