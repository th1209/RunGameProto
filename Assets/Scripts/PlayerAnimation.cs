using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimation : MonoBehaviour
{
    #region Private Fields
    Player _player = null;
    Animator _animator = null;
    #endregion

    #region Private constants
    private const string AnimatorKeyNameSpeed = "speed";
    private const string AnimatorKeyNameFlying = "flying";
    private const string AnimatorKeyNameDamaged = "damaged";
    private const string AnimatorKeyNameWinning = "winning";
    private const string AnimatorKeyNameLosing = "losing";
    #endregion

    #region Public Functions

    void Damaged()
    {
        if (_player.GetState() == Player.State.Winning ||
            _player.GetState() == Player.State.Losing) {
            return;
        }

        GetComponent<Animator>().SetTrigger(AnimatorKeyNameDamaged);
    }

    #endregion

    #region MonoBehaviour CallBacks
    void Start()
    {
        _player = GetComponent<Player>();
        Debug.Assert(_player != null);
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);
    }

    void Update()
    {
        if (_player.GetState() == Player.State.Running ||
            _player.GetState() == Player.State.Jumping
        ) {
            GetComponent<Animator>().SetFloat(AnimatorKeyNameSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.x);
        } else if (_player.GetState() == Player.State.Idling ||
            _player.GetState() == Player.State.Winning ||
            _player.GetState() == Player.State.Losing
        ) {
            GetComponent<Animator>().SetFloat(AnimatorKeyNameSpeed, 0.0f);
        }

        if (_player.GetState() == Player.State.Jumping) {
            GetComponent<Animator>().SetBool(AnimatorKeyNameFlying, true);
        } else {
            GetComponent<Animator>().SetBool(AnimatorKeyNameFlying, false);
        }

        if (_player.GetState() == Player.State.Winning) {
            GetComponent<Animator>().SetBool(AnimatorKeyNameWinning, true);
        } else if (_player.GetState() == Player.State.Losing) {
            GetComponent<Animator>().SetBool(AnimatorKeyNameLosing, true);
        }
    }
    #endregion
}
