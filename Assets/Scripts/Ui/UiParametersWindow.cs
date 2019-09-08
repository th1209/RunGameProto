using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// メッセージ受信用インタフェース.
public interface IUiParametersReceiver : IEventSystemHandler
{
    // 開始位置受信.
    void OnReceiveStartPositionMessage(Vector2 position);

    // ゴール位置受信.
    void OnReceiveGoalPositionMessage(Vector2 position);

    // 起動メッセージ受信.
    void OnReceiveLaunchMessage();

    // 終了メッセージ受信.
    void OnReceiveStopMessage();

    // 更新メッセージ受信.
    void OnReceiveUpdateMessage(Player player);
}


// インゲームシーンにおいて､各種変数値を受け取って表示するクラス.
public class UiParametersWindow : SingletonMonoBehaviour<UiParametersWindow>, IUiParametersReceiver, IPausable
{
    #region Private Enums
    public enum State
    {
        NotLaunched,
        Launched,
        Stopped,
        Invalid,
    }
    #endregion


    #region Private Serialize Fields

    [SerializeField]
    private SceneInGameController _sceneController = null;

    [SerializeField]
    private Text _textElapsedTime = null;

    [SerializeField]
    private Text _textDistance = null;

    [SerializeField]
    private Text _textStamina = null;

    [SerializeField]
    private Text _textSpeed = null;

    #endregion


    #region Private Fields

    Vector2 _startPosition = Vector2.zero;

    Vector2 _goalPosition = Vector2.zero;

    State _state = State.NotLaunched;

    float _elapsedTime = 0.0f;

    bool _isPaused = false;

    #endregion


    #region Public Functions

    public void Initialize()
    {
        Debug.Assert(_sceneController != null);
        Debug.Assert(_textElapsedTime != null);
        Debug.Assert(_textDistance != null);
        Debug.Assert(_textStamina != null);
        Debug.Assert(_textSpeed != null);

        _state = State.NotLaunched;
        _elapsedTime = 0.0f;
        _isPaused = false;

        _textElapsedTime.text = "0.0";
        _textDistance.text = "0.0" + "/" + "0.0";
        _textStamina.text = InGameParameters.PlayerStaminaMin.ToString("0.0");
        _textSpeed.text = InGameParameters.PlayerVelocityMin.ToString("0.0");
    }

    #endregion


    #region Private Functions

    void UpdateElapsedTime(float deltaTime)
    {
        _elapsedTime += deltaTime;
        Debug.Assert(_elapsedTime <= InGameParameters.ElapsedTimeMax);
        _textElapsedTime.text = _elapsedTime.ToString("0.0");
    }

    void UpdateDistance(Player player)
    {
        Vector2 pos = player.gameObject.GetComponent<Transform>().position;
        Debug.Assert(/*0.0f <= pos.x &&*/ pos.x <= InGameParameters.StageDistanceMax);

        float start = Mathf.Max(pos.x - _startPosition.x, 0.0f);
        float goal = _goalPosition.x - _startPosition.x;
        _textDistance.text = start.ToString("0.0") + "/" + goal.ToString("0.0");
    }

    void UpdateStamina(Player player)
    {
        float stamina = player.GetComponent<PlayerStamina>().GetCurrentValue();
        _textStamina.text = stamina.ToString("0.0");
    }

    void UpdateSpeed(Player player)
    {
        Vector2 velocity = player.gameObject.GetComponent<Rigidbody2D>().velocity;
        Debug.Assert(0.0f <= velocity.x);
        _textSpeed.text = velocity.x.ToString("0.0");
    }

    #endregion


    #region IUiParametersReceiver Implementation

    public void OnReceiveStartPositionMessage(Vector2 position)
    {
        _startPosition = position;
    }

    public void OnReceiveGoalPositionMessage(Vector2 position)
    {
        _goalPosition = position;
    }

    public void OnReceiveLaunchMessage()
    {
        _state = State.Launched;
        _elapsedTime = 0.0f;
    }

    public void OnReceiveStopMessage()
    {
        _state = State.Stopped;
        float goal = _goalPosition.x - _startPosition.x;
        _textDistance.text = goal.ToString("0.0") + "/" + goal.ToString("0.0");
    }

    public void OnReceiveUpdateMessage(Player player)
    {
        if (_state == State.NotLaunched) {
            return;
        }

        if (_isPaused) {
            return;
        }

        UpdateDistance(player);
        UpdateStamina(player);
        UpdateSpeed(player);
    }

    #endregion


    #region IPausable Implementation

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    #endregion


    #region MonoBehaviour CallBacks

    void FixedUpdate()
    {
        if (_state != State.Launched) {
            return;
        }

        if (_isPaused) {
            return;
        }

        UpdateElapsedTime(Time.deltaTime);
    }

    #endregion
}
