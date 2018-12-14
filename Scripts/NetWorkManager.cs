using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIO;

public class NetWorkManager : MonoBehaviour {

    SocketIOComponent io;

    #region UserData
    static public string userName;
    static public string idDataBase;
    static public string idSession;
    static public string idRoom;
    //static public bool isLocalPlayer;
    #endregion

    static public string[] playersId; // usuarios en el spawner
    static private bool created = false;
    static public bool gameStarted = false; // cuando termine la partida colocar esta variable en false

    void Awake() {

         GameObject go = GameObject.Find("SocketIO");
     
        io = go.GetComponent<SocketIOComponent>();

        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true; 
        }
    }

    void Start () {
        io.On("UserData", (resp) => {


            userName = resp.data.GetField("player")["nombre"].ToString();
            idDataBase = resp.data.GetField("player")["id"].ToString();
            idSession = resp.data.GetField("player")["idSession"].ToString();

            userName = QuitarComillas(userName);
            idDataBase = QuitarComillas(idDataBase);
            idSession = QuitarComillas(idSession);

             //print("username: " + userName);
             //print("idDataBase: " + idDataBase);
             //print("idSession: " + idSession);


        });

        io.On("ComenzarPartida", (resp) => {

            SceneManager.LoadScene("Game");
            gameStarted = true;
            idRoom = resp.data.GetField("idRoom").ToString();
            idRoom = QuitarComillas(idRoom);

            JSONObject p = resp.data.GetField("players");
            for (int i = 0; i < p.Count; i++) {

                ///// AGREGAR A LOS PLAYERS AL SPAWNER !!

                playersId[i] = QuitarComillas(p[i]["id"].ToString());
               // print(p[i]["id"]);

            }

           

            print("idRoom: " + idRoom);
        });

            
	}
	
	void Update () {
		
	}

    public static string QuitarComillas(string palabra) {
       palabra =  palabra.Replace("\"", "");
        return palabra;
    }

    public static JSONObject InputJumpHelicopter() {
        JSONObject o = new JSONObject(JSONObject.Type.OBJECT);
       
        o.AddField("idRoom", idRoom);

        return o;
    }

    public static JSONObject Input(string v) {

        JSONObject o = new JSONObject(JSONObject.Type.OBJECT);
        o.AddField("input", v);
       
        o.AddField("idRoom", idRoom);

        
        return o;
    }

    public static JSONObject UpdatePos(Vector3 pos) {

        JSONObject o = new JSONObject(JSONObject.Type.OBJECT);

        o.AddField("x", pos.x);
        o.AddField("y",pos.y);

        o.AddField("idRoom", idRoom);


        return o;

    }
}



