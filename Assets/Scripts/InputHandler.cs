using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region Private Serialize Fields
    [SerializeField]
    Player _player = null;
    #endregion

    #region MonoBehaviour CallBacks
    void Start()
    {
        Debug.Assert(_player != null);
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) {
            _player.Jump();
        }
    }
    #endregion
}
