using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetWorkInput : MonoBehaviour {

    SocketIOComponent io;

    CharacterData characterData;


    public float movX;
    public float movLeft;
    public float movRight;
    public float movY;


    private void Awake() {
        GameObject go = GameObject.Find("SocketIO");
        io = go.GetComponent<SocketIOComponent>();
    }

    void Start () {
        characterData = this.gameObject.GetComponent<CharacterData>();

        io.On("Input", (resp) => {

            if (characterData.id == NetWorkManager.QuitarComillas(resp.data.GetField("id").ToString())) {

                switch (NetWorkManager.QuitarComillas(resp.data.GetField("input").ToString())) {

                    case "LD":
                        // movLeft = -1f;
                        movX = -1f;
                        break;

                    case "LU":
                        // movLeft = 0f;
                        movX = 0f;
                        break;
                    case "RD":
                        // movRight = 1f;
                        movX = 1f;
                        break;
                    case "RU":
                      //  movRight = 0f;
                        movX = 0f;
                        break;

                    case "SpaceD":
                        movY = 3f;
                        break;

                    case "SpaceU":
                        movY = 0f;
                        break;

                    default:
                        break;

                }

            }
           
        });

    }
	
	
	void Update () {
        SendInput();
	}

    void SendInput() {

        if (characterData.isLocalPlayer) {

            #region Left right
            
            if (Input.GetKeyDown(KeyCode.A)) {

                if (NetWorkManager.gameStarted) {
                    io.Emit("Input", NetWorkManager.Input("LD"));
                }

            }

            if (Input.GetKeyDown(KeyCode.D)) { 

                if (NetWorkManager.gameStarted) {
                    io.Emit("Input", NetWorkManager.Input("RD"));
                }

            }

            if (Input.GetKeyUp(KeyCode.A)) { 

                if (NetWorkManager.gameStarted) {
                    io.Emit("Input", NetWorkManager.Input("LU"));
                }

            }

            if (Input.GetKeyUp(KeyCode.D)) { 

                if (NetWorkManager.gameStarted) {
                    io.Emit("Input", NetWorkManager.Input("RU"));
                }

            }

            #endregion

            #region jump

            if (Input.GetKeyDown(KeyCode.Space)) { 

                if (NetWorkManager.gameStarted) {
                    io.Emit("Input", NetWorkManager.Input("SpaceD"));
                }

            }

            if (Input.GetKeyUp(KeyCode.Space)) {

                if (NetWorkManager.gameStarted) {
                    io.Emit("Input", NetWorkManager.Input("SpaceU"));
                }

            }

            #endregion
        }
    }
}
