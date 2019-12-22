using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkSceneManager : NetworkBehaviour
{
   [Scene] public string ServerScene;
   [Scene] public string ClientScene;
   
   public override void OnStartServer()
   {
      base.OnStartServer();

      Debug.Log("We load the server scene");

      SceneManager.LoadScene(ServerScene, LoadSceneMode.Single);
   }

   public override void OnStartClient()
   {
      base.OnStartClient();

      Debug.Log("We load the CLIENT scene");
      
      SceneManager.LoadScene(ClientScene, LoadSceneMode.Single);
   }
}
