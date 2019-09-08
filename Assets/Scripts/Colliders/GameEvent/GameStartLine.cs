using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStartLine : GameEventCollider
{
    #region MonoBehaviour CallBacks

    void Start()
    {
        ExecuteEvents.Execute<IUiParametersReceiver>(
            target: UiParametersWindow.GetInstance().gameObject,
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
            UiParametersWindow.GetInstance().gameObject,
            null,
            (handler, eventData) => { handler.OnReceiveLaunchMessage(); }
        );
    }

    #endregion
}
