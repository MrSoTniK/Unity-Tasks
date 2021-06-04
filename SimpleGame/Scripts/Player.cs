using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private static GameObject _player;
   public static GameObject GetPlayer() 
   {
        return _player;
   }

    private void Awake()
    {
        _player = gameObject;
    }    
}