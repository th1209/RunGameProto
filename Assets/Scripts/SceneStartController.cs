using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStartController : MonoBehaviour
{
    #region Public Functions
    
    public void GoToInGameScene()
    {
        SceneManager.LoadSceneAsync("SceneInGame");
    }

    #endregion
}
