using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerStamina))]
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

    public bool IsGrounded()
    {
        return transform.position.y <= InGameParameters.PlayerGroundedThreshold;
    }

    public void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    #endregion


    #region Private Functions
    private void Run()
    {
        float stamina = GetComponent<PlayerStamina>().GetCurrentValue();
        float velocityX = PlayerMovementMathFunctions.GetVelocityByStamina(stamina, GameConfigRepository.LoadMovementEquationType());
        if (_player.GetState() == Player.State.Damaged) {
            velocityX += InGameParameters.PlayerMinusVelocityWhenDamaged;
        }
        velocityX = Mathf.Clamp(
            velocityX,
            InGameParameters.PlayerVelocityMin,
            InGameParameters.PlayerVelocityMax);
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = velocityX;
        GetComponent<Rigidbody2D>().velocity = velocity;
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
        if (IsGrounded()) {
            _player.ResetJumpCount();
        }
    }

    void FixedUpdate()
    {
        if (_player.GetState() != Player.State.Running &&
            _player.GetState() != Player.State.Jumping &&
            _player.GetState() != Player.State.Damaged
        ) {
            return;
        }

        Run();
    }
    #endregion
}
