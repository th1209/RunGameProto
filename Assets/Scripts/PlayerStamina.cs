using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour, IPausable
{
    #region Private Fields
    private float _healAmountPerSec = 10.0f;

    private float _consumeAmountPerTap = 10.0f;

    private float _currentAmount = InGameParameters.PlayerStaminaMax;

    private bool _isPause = false;
    #endregion


    #region Public Functions
    public float GetCurrentValue()
    {
        return _currentAmount;
    }

    public void ConsumeByTap()
    {
        _currentAmount = Mathf.Clamp(
            _currentAmount - _consumeAmountPerTap,
            InGameParameters.PlayerStaminaMin,
            InGameParameters.PlayerStaminaMax
        );
    }
    #endregion


    #region IPausable Implementation

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }

    #endregion


    #region Private Functions
    void Start()
    {
        // TODO: 保存された設定値を見て､設定値を復元する処理を追加する
        _currentAmount = InGameParameters.PlayerStaminaMax;
        _isPause = false;
    }

    void Update()
    {
        if (_isPause) {
            return;
        }

        _currentAmount = Mathf.Clamp(
            _currentAmount + _healAmountPerSec * Time.deltaTime,
            InGameParameters.PlayerStaminaMin,
            InGameParameters.PlayerStaminaMax
        );
    }
    #endregion
}
