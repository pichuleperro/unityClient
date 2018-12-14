using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class MenuManager: MonoBehaviour {

    SocketIOComponent io;

    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject settingMenu;
    public GameObject loadingPlayMode;

    

    private void Awake() {
        GameObject go = GameObject.Find("SocketIO");
        io = go.GetComponent<SocketIOComponent>();
    }
    private void Start()
    {
        io.On("BuscarSala", (resp) => {
            print(resp.data[0]);
        });

      
   

        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        settingMenu.SetActive(false);
        loadingPlayMode.SetActive(false);

    }

  public void Jugar()
    {
        // desactivar las demas ventanas
        // activar el menu de modos de juego
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
        settingMenu.SetActive(false);
        loadingPlayMode.SetActive(false);

    }
    public void Configuracion()
    {
        // configuracion de controles , pantalla, sonidos , etc..
        mainMenu.SetActive(false);
        playMenu.SetActive(false);
        settingMenu.SetActive(true);
        loadingPlayMode.SetActive(false);
    }
    public void Salir()
    {
        // salir de la aplicacion 
        Application.Quit();
    }

    //public void Tienda() { }
    //public void Amigos() { }


        //MODOS DE JUEGOS
    public void BattleRoyaleMode()
    {
        // escoger tipo de juego, battle royale 
        //una vez sea escogido el tipo de juego , unirse a la sala . si no existe , crearla. 
        mainMenu.SetActive(false);
        playMenu.SetActive(false);
        settingMenu.SetActive(false);
        loadingPlayMode.SetActive(true);
        StartCoroutine(OnLoadingBattleRoyaleMode());
    }
    
    public void StealGoldMode() {
        mainMenu.SetActive(false);
        playMenu.SetActive(false);
        settingMenu.SetActive(false);
        loadingPlayMode.SetActive(true);
        StartCoroutine(OnLoadingStealGoldMode());
    }

   // public void Campaña() { } // proximamente

    public void Atras()
    {
        // volver al panel anterior
        // desactivar ventana de  play menu 
        // activar ventana de main menu
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        settingMenu.SetActive(false);
        loadingPlayMode.SetActive(false);
    }
    public void Cancelar()
    {
        // cancelar el emparejamiento y volver a la pantalla principal
        // emitir al servidor que debe de eliminar al usuario del arreglo en la lista de salas

        JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);

        obj.AddField("userName", NetWorkManager.userName);
        obj.AddField("idDataBase", NetWorkManager.idDataBase);
        obj.AddField("idSession", NetWorkManager.idSession);

        io.Emit("Cancelar", obj);

        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        settingMenu.SetActive(false);
        loadingPlayMode.SetActive(false);
       
    }
    IEnumerator OnLoadingStealGoldMode() {
        JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
        obj.AddField("GameMode", "StealGold");

        io.Emit("BuscarSala", obj);
        yield return new WaitForSeconds(2f);
        // cargar escena de juego
    }

    IEnumerator OnLoadingBattleRoyaleMode()
    {

        JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
        obj.AddField("GameMode", "BattleRoyale");

        io.Emit("BuscarSala", obj);


        yield return new WaitForSeconds(2f);
        
        // cargar escena de juego
    }

    string RandomIdRoom() {

        

        string idRoom = "";

        idRoom = NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() + NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() +"-"+
                 NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() + NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() +"-"+
                 NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() + NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() +"-"+
                 NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() + NumAleatorio() + NumAleatorio() + NumAleatorio() + LetraAleatoria() + LetraAleatoria() 
            ;

        return idRoom;
    }
    int NumAleatorio() { 

        int num =  UnityEngine.Random.Range(0, 100);
        return num;

    }
    int ObtenerLetraAleatoria() {

        int obtenerLetraAleatoria = UnityEngine.Random.Range(0, 25);
        return obtenerLetraAleatoria;

    }
     string LetraAleatoria() {
        string letraAleatoria;

        switch (ObtenerLetraAleatoria()) {
            case 0:
                letraAleatoria = "a";
                break;
            case 1:
                letraAleatoria = "b";
                break;
            case 2:
                letraAleatoria = "c";
                break;
            case 3:
                letraAleatoria = "d";
                break;
            case 4:
                letraAleatoria = "e";
                break;
            case 5:
                letraAleatoria = "f";
                break;
            case 6:
                letraAleatoria = "g";
                break;
            case 7:
                letraAleatoria = "h";
                break;
            case 8:
                letraAleatoria = "i";
                break;
            case 9:
                letraAleatoria = "j";
                break;
            case 10:
                letraAleatoria = "k";
                break;
            case 11:
                letraAleatoria = "l";
                break;
            case 12:
                letraAleatoria = "m";
                break;
            case 13:
                letraAleatoria = "n";
                break;
            case 14:
                letraAleatoria = "o";
                break;
            case 15:
                letraAleatoria = "p";
                break;
            case 16:
                letraAleatoria = "q";
                break;
            case 17:
                letraAleatoria = "r";
                break;
            case 18:
                letraAleatoria = "s";
                break;
            case 19:
                letraAleatoria = "t";
                break;
            case 20:
                letraAleatoria = "u";
                break;
            case 21:
                letraAleatoria = "v";
                break;
            case 22:
                letraAleatoria = "w";
                break;
            case 23:
                letraAleatoria = "x";
                break;
            case 24:
                letraAleatoria = "y";
                break;
            case 25:
                letraAleatoria = "z";
                break;

            default:
                letraAleatoria = "";
                break;
        }
        return letraAleatoria;
    }

}










