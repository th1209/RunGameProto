using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージ上に配置される障害物やアイテムを表すクラス.
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteFadeoutComponent))]
public class StageCollider : MonoBehaviour, ICollider
{
    #region Protected Constants
    protected const float FadeoutTime = 0.8f;
    #endregion

    #region Protected Fields
    protected SpriteFadeoutComponent _fadeoutComponent = null;
    #endregion

    #region MonoBehaviour CallBacks
    void Start()
    {
        _fadeoutComponent = GetComponent<SpriteFadeoutComponent>();
        Debug.Assert(_fadeoutComponent != null);
    }
    #endregion
}
