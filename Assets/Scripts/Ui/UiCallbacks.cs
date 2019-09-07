using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// インゲームシーンの各UIに割り当てるコールバックを定義したクラス.
public class UiCallbacks : MonoBehaviour
{
    #region Private Serialize Fields
    // シーンコントローラ.
    [SerializeField]
    private SceneInGameController _sceneController = null;

    [SerializeField]
    Player _player = null;
    #endregion


    #region Public Functions

    public void Initialize()
    {
        Debug.Assert(_sceneController != null);
        Debug.Assert(_player != null);
    }

    #endregion


    #region Ui Callbacks

    public void OnTap()
    {
        _player.GetComponent<PlayerStamina>().ConsumeByTap();
    }

    public void OnGameStart()
    {
        _sceneController.StartGame();
    }

    public void OnPause()
    {
        _sceneController.Pause();
    }

    public void OnResume()
    {
        _sceneController.Resume();
    }

    public void OnRetry()
    {
        _sceneController.Retry();
    }

    public void OnHome()
    {
        _sceneController.GoToHomeScene();
    }

    #endregion


    #region MonoBehaviour CallBacks
    #endregion
}
