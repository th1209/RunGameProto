using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GameStartLine : MonoBehaviour, IObstacle
{
    // Debug
    [SerializeField]
    GameObject root = null;

    #region MonoBehaviour CallBacks

    void Start()
    {
        ExecuteEvents.Execute<IUiParametersReceiver>(
            target: gameObject,
            eventData: null,
            functor: (handler, eventData) => handler.OnReceiveStartPositionMessage(gameObject.transform.position)
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(CollisionUtility.CheckCollider2DType(other) != CollisionType.OwnPlayer) {
            return;
        }

        ExecuteEvents.Execute<IUiParametersReceiver>(
            gameObject,
            null,
            (handler, eventData) => { handler.OnReceiveLaunchMessage(); }
        );
    }

    #endregion
}
