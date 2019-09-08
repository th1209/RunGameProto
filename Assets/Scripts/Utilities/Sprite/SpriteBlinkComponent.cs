using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スプライト点滅用コンポーネント.
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBlinkComponent : MonoBehaviour
{
    #region Private Constants
    private const float SpriteBlinkCoefficient = 10.0f;
    #endregion


    #region  Private Fields
    private SpriteRenderer _renderer = null;
    private bool _isBlinking = false;
    #endregion


    #region Public Functions
    public void StartBlink()
    {
        _isBlinking = true;
    }

    public void StopBlink()
    {
        _isBlinking = false;
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (! _isBlinking) {
            return;
        }

        float alpha = Mathf.Abs(Mathf.Sin(Time.time * SpriteBlinkCoefficient));
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
    #endregion
}
