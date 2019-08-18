﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    #region Private Fields
    Player _player = null;
    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        _player = GetComponent<Player>();
        Debug.Assert(_player != null);
    }

    void Update()
    {
        
    }
    #endregion
}
