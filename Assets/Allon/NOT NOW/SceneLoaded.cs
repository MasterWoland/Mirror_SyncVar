using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaded : MonoBehaviour
{
   public static event Action SceneIsLoaded;

   private void Start()
   {
      SceneIsLoaded?.Invoke();
   }
}
