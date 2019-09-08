using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// プレイヤーの現在の状態管理クラス.

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IPausable
{
    #region Public Enums
    public enum State
    {
        // ゲーム開始前アイドル中.
        Idling,
        // 走行中.
        Running,
        // ジャンプ中.
        Jumping,
        // 被ダメージ中.
        Damaged,
        // ゲーム終了停止中(勝利演出).
        Winning,
        // ゲーム終了停止中(敗北演出).
        Losing,
        // ポーズ中(全機能を完全停止する).
        Pausing,
        // 無効な状態.
        Invalid,
    }
    #endregion


    #region Private Fields
    // 移動用コンポーネント.
    private PlayerMovement _movementComponent = null;

    // アニメーション用コンポーネント.
    private PlayerStamina _staminaComponent = null;

    // アニメーション用コンポーネント.
    private PlayerAnimation _animationComponent = null;

    // 状態.
    private State _state = State.Idling;

    // 以前の状態(ポーズからの再開時に使用する).
    private State _prevState = State.Invalid;

    // 無敵中残り時間.
    private float _restInvincibleTime = 0.0f;

    // ダメージ中残り時間.
    private float _restDamageTime = 0.0f;

    // 現在のジャンプ回数.
    private int _jumpCount = 0;
    #endregion


    #region Public Functions
    // 現在の状態.
    public State GetState()
    {
        return _state;
    }

    // 現在無敵状態か.
    public bool IsInvincible()
    {
        return _restInvincibleTime > 0.0f;
    } 

    public void Idle()
    {
        _state = State.Idling;
    }

    public void Run()
    {
        _state = State.Running;
    }

    public void Jump()
    {
        if (! _movementComponent.IsGrounded()) {
            return;
        }

        if (_state != State.Running) {
            return;
        }

        if (_jumpCount >= InGameParameters.MaxJumpCount) {
            return;
        }

        _jumpCount++;

        if (_jumpCount == 1) {
            _state = State.Jumping;
        }

        _movementComponent.Jump();
    }

    public int GetJumpCount()
    {
        return _jumpCount;
    }

    public void ResetJumpCount()
    {
        _jumpCount = 0;
    }

    public void Damaged()
    {

    }

    public void Win()
    {
        _state = State.Winning;
        _movementComponent.Stop();
    }

    public void Lose()
    {
        _state = State.Losing;
        _movementComponent.Stop();
    }

    #endregion


    #region Private Functions

    private void SendEventToParametersUi()
    {
        ExecuteEvents.Execute<IUiParametersReceiver>(
            target: UiParametersWindow.GetInstance().gameObject,
            eventData: null,
            functor: (handler, eventData) => handler.OnReceiveUpdateMessage(this)
        );
    }

    #endregion

    #region IPausable Implementation

    public void Pause()
    {
        _prevState = _state;
        _state = State.Pausing;
        _movementComponent.Pause();
        _staminaComponent.Pause();
    }

    public void Resume()
    {
        Debug.Assert(_prevState != State.Invalid);
        _state = _prevState;
        _movementComponent.Resume();
        _staminaComponent.Resume();
    }

    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        Debug.Assert(_movementComponent != null);
        _staminaComponent = GetComponent<PlayerStamina>();
        Debug.Assert(_staminaComponent != null);
        _animationComponent = GetComponent<PlayerAnimation>();
        Debug.Assert(_animationComponent != null);

        _state = State.Idling;
    }

    void Update()
    {
        if (_state != State.Pausing) {
            SendEventToParametersUi();
        }

        switch(_state)
        {
            case State.Idling:
                break;
            case State.Running:
                break;
            case State.Jumping:
                if (_jumpCount <= 0) {
                    _state = State.Running;
                }
                break;
            case State.Damaged:
                // TODO 残りダメージ中時間を減算する処理.
                break;
            case State.Winning:
                break;
            case State.Losing:
                break;
            case State.Pausing:
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }
    #endregion
}
