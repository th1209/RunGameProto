﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの現在の状態管理クラス.

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
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
        // 2段ジャンプ中.
        DoubleJumping,
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

    public void Run()
    {

    }

    public void Jump()
    {
        if (_jumpCount >= InGameParameters.MaxJumpCount) {
            return;
        }
        if (_state == State.DoubleJumping) {
            return;
        }

        _jumpCount++;

        if (_jumpCount == 1) {
            _state = State.Jumping;
        }
        if (_jumpCount == InGameParameters.MaxJumpCount) {
            _state = State.DoubleJumping;
        }

        // TODO:PlayerMovementクラスのジャンプ処理を呼び出す.
    }

    public int GetJumpCount()
    {
        return _jumpCount;
    }

    public void Damaged()
    {

    }

    public void Win()
    {

    }

    public void Lose()
    {
        
    }

    public void Pause()
    {

    }

    public void Resume()
    {

    }
    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        
    }

    void Update()
    {
        switch(_state)
        {
            case State.Idling:
                // TODO Animationの更新.
                break;
            case State.Running:
                // TODO Animationの更新.
                // TODO 加速する処理.
                break;
            case State.Jumping:
                // TODO Animationの更新.
                // TODO 地面に付いたら状態を更新する処理.
                break;
            case State.DoubleJumping:
                // TODO Animationの更新.
                // TODO 地面に付いたら状態を更新する処理.
                break;
            case State.Damaged:
                // TODO Animationの更新.
                // TODO 残りダメージ中時間を減算する処理.
                break;
            case State.Winning:
                // TODO Animationの更新.
                break;
            case State.Losing:
                // TODO Animationの更新.
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