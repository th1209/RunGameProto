using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GameClearLine : MonoBehaviour, IObstacle
{
    #region MonoBehaviour CallBacks

    void Start()
    {
        ExecuteEvents.Execute<IUiParametersReceiver>(
            target: UiParametersWindow.GetInstance().gameObject,
            eventData: null,
            functor: (handler, eventData) => handler.OnReceiveGoalPositionMessage(gameObject.transform.position)
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
            (handler, eventData) => { handler.OnReceiveStopMessage(); }
        );

        SceneInGameController.GetInstance().ClearGame();
    }
    #endregion
}
