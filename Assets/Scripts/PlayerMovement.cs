using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
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
