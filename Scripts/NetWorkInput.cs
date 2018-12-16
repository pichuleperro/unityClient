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

    public bool bottonLeftPressed = false;
    public bool bottonRightPressed = false;
    public bool bottonSpacePressed = false;


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
                        bottonLeftPressed = true;
                        break;

                    case "LU":
                        // movLeft = 0f;
                        bottonLeftPressed = false;
                        break;
                    case "RD":
                        // movRight = 1f;
                        bottonRightPressed = true;
                        break;
                    case "RU":
                        //  movRight = 0f;
                        bottonRightPressed = false;
                        break;

                    case "SpaceD":
                        movY = 3f;
                        bottonSpacePressed = true;
                        break;

                    case "SpaceU":
                        movY = 0f;
                        bottonSpacePressed = false;
                        break;

                    default:
                        break;

                }

            }
           
        });

    }
	
	
	void Update () {
        SendInput();
        SetInput();
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

    void SetInput() {

        if(bottonLeftPressed && !bottonRightPressed) {
            movX = -1f;
        }

        if (!bottonLeftPressed && bottonRightPressed) {
            movX = 1f;
        }

        if (!bottonLeftPressed && !bottonRightPressed) {
            movX = 0f;
        }

    }
}
