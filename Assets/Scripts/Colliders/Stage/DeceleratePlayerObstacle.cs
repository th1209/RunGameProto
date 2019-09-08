using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceleratePlayerObstacle : StageObstacle
{
    #region MonoBehaviour CallBacks
    void Start()
    {
        _fadeoutComponent = GetComponent<SpriteFadeoutComponent>();
        Debug.Assert(_fadeoutComponent != null);
    }

    void Update()
    {
        if (_fadeoutComponent.IsFadeout()) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionType colType = CollisionUtility.CheckCollider2DType(other);
        if (colType == CollisionType.NonePlayer) {
            return;
        }

        Player player = CollisionUtility.GetPlayerFromCollider2D(other);
        player.Damaged();
        
        if (colType == CollisionType.OwnPlayer) {
            _fadeoutComponent.StartFadeout(FadeoutTime);
        }
    }
    #endregion
}
