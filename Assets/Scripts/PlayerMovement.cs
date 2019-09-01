using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IPausable
{   
    #region Private Fields
    Player _player = null;
    #endregion


    #region Public Functions
    public void Jump()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.y += InGameParameters.PlayerJumpAcceleration;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    #endregion


    #region Private Functions
    private void Run()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = Mathf.Clamp(
            velocity.x + InGameParameters.PlayerAcceleration * Time.deltaTime,
            InGameParameters.PlayerVelocityMin,
            InGameParameters.PlayerVelocityMax);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private bool IsGrounded()
    {
        return transform.position.y <= InGameParameters.PlayerGroundedThreshold;
    }
    #endregion


    #region IPausable Implementation

    public void Pause()
    {
        GetComponent<Rigidbody2D>().Pause(gameObject);
    }

    public void Resume()
    {
        GetComponent<Rigidbody2D>().Resume(gameObject);
    }

    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        _player = GetComponent<Player>();
        Debug.Assert(_player != null);
    }

    void Update()
    {
        // TODO:
        // 後ほど床との衝突判定に処理を引っ越す.
        // (ジャンプ直後時にisGround判定されて､ジャンプ回数がリセットされる不具合を回避するため)
        if (IsGrounded()) {
            _player.ResetJumpCount();
        }
    }

    void FixedUpdate()
    {
        if (_player.GetState() != Player.State.Running &&
            _player.GetState() != Player.State.Jumping &&
            _player.GetState() != Player.State.DoubleJumping
        ) {
            return;
        }

        Run();
    }
    #endregion
}
