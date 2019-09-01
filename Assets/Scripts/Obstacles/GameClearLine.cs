using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GameClearLine : MonoBehaviour, IObstacle
{
    #region MonoBehaviour CallBacks
    void OnTriggerEnter2D(Collider2D other)
    {
        if(CollisionUtility.CheckCollider2DType(other) == CollisionType.OwnPlayer) {
            SceneInGameController.GetInstance().ClearGame();
        }
    }
    #endregion
}
