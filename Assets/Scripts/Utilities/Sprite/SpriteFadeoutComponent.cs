using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スプライトフェードアウト演出用コンポーネント.
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFadeoutComponent : MonoBehaviour
{
    #region  Private Fields
    private SpriteRenderer _renderer = null;
    private float _fadeoutTime = 0.0f;
    private float _remainTime = 0.0f;
    private bool _isStartFadeout = false;
    #endregion


    #region Public Functions
    public void StartFadeout(float fadeoutTime)
    {
        _isStartFadeout = true;
        _remainTime  = fadeoutTime;
        _fadeoutTime = fadeoutTime;
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public bool IsFadeout()
    {
        float alpha = _renderer.color.a;
        return alpha < 0.01f;
    }
    #endregion


    #region MonoBehaviour CallBacks
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (! _isStartFadeout) {
            return;
        }

        _remainTime -= Time.deltaTime;
        _remainTime = Mathf.Max(_remainTime, 0.0f);
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, _remainTime/_fadeoutTime);
    }
    #endregion
}
