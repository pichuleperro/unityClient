using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetWorkTransform : MonoBehaviour {



    SocketIOComponent io;

    CharacterData characterData;

    bool uT = true;


    private void Awake() {

        GameObject go = GameObject.Find("SocketIO");
        io = go.GetComponent<SocketIOComponent>();
    }
    void Start () {
        characterData = this.gameObject.GetComponent<CharacterData>();

        StartCoroutine(UpdateTransform(1f));

        io.On("UpdatePos", (resp) => {

            print("recibiendo datos");

            if (characterData.id == NetWorkManager.QuitarComillas(resp.data.GetField("id").ToString())) {

                float x = float.Parse(NetWorkManager.QuitarComillas(resp.data.GetField("x").ToString()));
                float y = float.Parse(NetWorkManager.QuitarComillas(resp.data.GetField("y").ToString()));

                Vector3 pos = new Vector3(x, y);

                transform.position = pos;
            }

        });
    }
	
	
	void Update () {

        if (characterData.isLocalPlayer) {
            SendTransform();
        }
        else {
        // el jugador no local no puede enviar datos.
        }
       


    }

    IEnumerator UpdateTransform(float hZ) {

        while (NetWorkManager.gameStarted) {
            uT = true;
            yield return new WaitForSeconds(hZ);
        }
    }

    void SendTransform() {

        if (uT) { io.Emit("UpdatePos", NetWorkManager.UpdatePos(transform.position)); }
        
        uT = false;
    }
}
