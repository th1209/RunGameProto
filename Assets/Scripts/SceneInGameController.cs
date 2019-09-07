using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// インゲームシーン管理クラス.
public class SceneInGameController : SingletonMonoBehaviour<SceneInGameController>
{
    #region Private Enums
    public enum State
    {
        // ゲーム開始前.
        NotStartd,
        // ゲームプレイ中.
        GamePlaying,
        // ポーズ中.
        Pausing,
        // ゲームクリア.
        GameCleard,
        // 無効な状態.
        Invalid,
    }
    #endregion


    #region Private Serialize Fields

    // プレイヤー管理クラス.
    [SerializeField]
    private Player _player = null;

    // UIレイヤ管理クラス.
    [SerializeField]
    private UiLayers _uiLayerController = null;

    // UIコールバック管理クラス.
    [SerializeField]
    private UiCallbacks _uiCallbackController = null;

    // パラメタウィンドウ管理クラス.
    [SerializeField]
    private UiParametersWindow _uiParametersWindowController = null;

    #endregion


    #region Private Fields

    // 状態.
    private State _state = State.NotStartd;

    #endregion


    #region State Change Functions

    public void StartGame()
    {
        _uiLayerController.StartGame();
        _player.Run();
    }

    public void ClearGame()
    {
        _uiLayerController.ClearGame();
        _player.Win();
    }

    public void Pause()
    {
        _uiLayerController.Pause();
        _uiParametersWindowController.Pause();
        _player.Pause();
    }

    public void Resume()
    {
        _uiLayerController.Resume();
        _uiParametersWindowController.Resume();
        _player.Resume();
    }

    #endregion


    #region Public Functions

    public void Retry()
    {
        SceneManager.LoadSceneAsync("SceneInGame");
    }

    public void GoToHomeScene()
    {
        SceneManager.LoadSceneAsync("SceneStart");
    }

    #endregion


    #region Private Functions

    private void Initialize()
    {
        _state = State.NotStartd;

        _uiLayerController.Initialize();
        _uiCallbackController.Initialize();
        _uiParametersWindowController.Initialize();
        _player.Idle();
    }

    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        Debug.Assert(_player != null);
        Debug.Assert(_uiLayerController != null);
        Debug.Assert(_uiCallbackController != null);

        Initialize();
    }

    void Update()
    {
        switch(_state)
        {
            case State.NotStartd:
                break;
            case State.GamePlaying:
                break;
            case State.Pausing:
                break;
            case State.GameCleard:
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }
    #endregion
}

