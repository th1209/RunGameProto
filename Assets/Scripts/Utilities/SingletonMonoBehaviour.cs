using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    #region Private Static Fields

    private static volatile T _sInstance = null;

    #endregion


    #region Public Static Functions

    public static T GetInstance()
    {
        if (_sInstance == null) {
            _sInstance = FindObjectOfType(typeof(T)) as T;
            Debug.Assert(_sInstance != null);
            Debug.Assert(FindObjectsOfType(typeof(T)).Length <= 1);
        }

        return _sInstance;
    }

    #endregion


    #region MonoBehaviour CallBacks

    void OnDestroy()
    {
        _sInstance = null;
    }

    #endregion
}
