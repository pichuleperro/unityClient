using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour {

    SocketIOComponent io;
    public GameObject register;
    public GameObject login;
    public GameObject onLogin;

    public InputField usernameRegister;
    public InputField passwordRegister;
    public InputField passwordRegister2;
    public InputField usernameLogin;
    public InputField passwordLogin;
    public Text debug;
    public Text debugLogin;

   
    string id;
   

    

    private void Awake()
    {
        GameObject go = GameObject.Find("SocketIO");
        io = go.GetComponent<SocketIOComponent>();
        
    }


    void Start () {

        io.On("info", (respuesta) =>
        {
            debug.text = respuesta.data[0].ToString();
        });
        io.On("Login", (respuesta) =>
        {
            
            OnLogin(respuesta, out id);
         //   print(id);
           // transformar el json que me llega desde el servidor a un string de buena manera. recordar 
        });
      


        register.SetActive(false);
        login.SetActive(true);
        onLogin.SetActive(false);




    }

    
    void Update () {

      //  userData.idDataBase = id;
        
   
    } // update del juego

  // conexion del server


    public void Ingresar()
    {
        if (usernameLogin.text.Length == 0 && passwordLogin.text.Length ==0)
        {
            debugLogin.text = "Debes de llenar los campos";
        }
        else if (usernameLogin.text.Length == 0)
        {
            debugLogin.text = "Debes ingresar tu nombre de usuario";
        }
        else if (passwordLogin.text.Length == 0)
        {
            debugLogin.text = " Debes igresar una contraseña";
        }
        else if (passwordLogin.text.Length < 8 && passwordLogin.text.Length > 0)
        {
            debugLogin.text = " La contraseña contiene 8 caracteres como minimo .";
        }
        else
        {
            debugLogin.text= "ingresando datos al servidor..";

            JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);
            obj.AddField("UserName", usernameLogin.text);
            obj.AddField("PassWord", passwordLogin.text);


            io.Emit("Login", obj);
        }

        
        // una vez que haya ingresado al usuario comenzar el juego . recordar obtener el ID que me otorga el socket en el servidor 

    }  // ingresar 

    public void RegistroPanel()
    {
        login.SetActive(false);
        register.SetActive(true);
        
    } //  manejador de paneles ( activar y desactivar paneles ) 


    //public void GoogleSignIn()
    //{
     
    //    if (passwordRegister.text.Length==0)
    //    {
    //        print("debes ingresar una contraseña");
    //    }
    //    else if (passwordRegister.text.Length < 8)
    //    {
    //        print("Tu contraseña debe contener al menos 8 carácteres");
    //    }
    //    else
    //    {
    //        Application.OpenURL("https://accounts.google.com/signin/oauth?client_id=1026283452409-vo3dtu1q2it9nvvrqpn3974h6en3msi0.apps.googleusercontent.com&as=K69Wu8vVY5y676E5H7tf3w&destination=http://localhost:3000&approval_state=!ChRlLV9rM0otRVZYbHFOaUFfRWE3RBIfSXdWdWFqQU92ZWtYRUZvSEdUZHlJLTN0NzhhMVpSWQ%E2%88%99ANKMe1QAAAAAW76YRNmjBIYpwvCyiFrg_T2mUfZYpbzt&oauthgdpr=1&xsrfsig=AHgIfE_NrCluvpev5lvT0n0dCS1UQqQIXw");
    //        Application.OpenURL("http://localhost:3000");
    //        identificado = true;

    //    }

    //   // debug.text = googleEmail;
        
    //}    // una vez que haya clikeado el weon en el google sign in este quedará autenticado

    public void Register()
    {
        if (passwordRegister.text.Length == 0 && passwordRegister2.text.Length == 0 && usernameRegister.text.Length == 0)
        {
            debug.text = "debes llenar los campos ";
        }
        else if (passwordRegister.text.Length == 0 && passwordRegister2.text.Length == 0)
        {
            debug.text = "debes llenar los campos de contraseña";
        }
        else if (passwordRegister2.text.Length == 0)
        {
            debug.text = "debes repetir la contraseña";
        }
        else if (passwordRegister.text.Length < 8 && passwordRegister.text.Length > 0)
        {
            debug.text = "Tu contraseña debe contener al menos 8 carácteres";
        }
        else if(passwordRegister.text.Length == 0 )
        {
            debug.text = "debes ingresar una contraseña";
           
        }
        else if (passwordRegister.text != passwordRegister2.text)
        {
            debug.text = "Las contraseñas deben ser iguales";
        }
        else
        {
            print(passwordRegister.text + " " + usernameRegister.text);
            debug.text = "Registrando";
            JSONObject obj = new JSONObject(JSONObject.Type.OBJECT);         
            obj.AddField("UserName", usernameRegister.text);
            obj.AddField("PassWord", passwordRegister.text);

            // enviar datos al servidor y registrarlos en mongodb 
            io.Emit("registro", obj);
            StartCoroutine(BackToLogin());

        } // registrar al usuario


    }   // comprobar si la contraseña tiene 8 caracteres como minimo antes de enviarla al servidor para registrar  

    void OnLogin(SocketIOEvent respuesta, out string id )
    {
       

        if (respuesta.data[1].ToString() != "\"true\"")
        {
            print(respuesta.data[0]);
            
            
        }
        id = respuesta.data[2].ToString();
        register.SetActive(false);
        login.SetActive(false);
        onLogin.SetActive(true);
        StartCoroutine(LoadingEscene(2));
    }
   
    IEnumerator BackToLogin()
    {
        yield return new WaitForSeconds(2f);
        register.SetActive(false);
        login.SetActive(true);

      
    }

   
    IEnumerator LoadingEscene(float tiempoDeEspera)
    {
        
        print("cargando escena");
        yield return new WaitForSeconds(tiempoDeEspera);

        // cargar escena
        SceneManager.LoadScene("MainMenu");

    }


    
}
