using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiTouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Private Enum

    private enum TouchType
    {
        Touch,
        FlickRight,
        FlickLeft,
        FlickUp,
        FlickDown,
    }

    private const float FlickThreshold = 100.0f;

    #endregion

    #region Private Serialize Fields
    [SerializeField]
    Player _player = null;
    #endregion

    #region Private Fields
    Vector2 _touchDownPos;
    Vector2 _touchUpPos;
    #endregion


    #region MonoBehaviour Callbacks

    void Start()
    {
        Debug.Assert(_player != null);
    }

    void Update()
    {
    #if UNITY_EDITOR
        if (Input.GetKeyDown("space")) {
            _player.Jump();
        }
        // パネルタッチと二重でスタミナが消費されてしまうので､一旦コメントアウト.
        // if (Input.GetMouseButtonDown(0)) {
        //     _player.GetComponent<PlayerStamina>().ConsumeByTap();
        // }
    #endif
    }

    #endregion

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _touchDownPos = pointerEventData.position;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _touchUpPos = pointerEventData.position;

        TouchType touchType = DecideTouchType(_touchDownPos, _touchUpPos);

        if (touchType == TouchType.Touch) {
            _player.GetComponent<PlayerStamina>().ConsumeByTap();
        }
        if (touchType == TouchType.FlickUp) {
            _player.Jump();
        }
    }

    private TouchType DecideTouchType(Vector2 posStart, Vector2 posEnd)
    {
        Vector2 diff = posEnd - posStart;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
            if (diff.x >= FlickThreshold) {
                return TouchType.FlickRight;
            }
            if (diff.x <= -FlickThreshold) {
                return TouchType.FlickLeft;
            }
        } else {
            if (diff.y >= FlickThreshold) {
                return TouchType.FlickUp;
            }
            if (diff.y <= -FlickThreshold) {
                return TouchType.FlickDown;
            }
        }
        return TouchType.Touch;
    }
}
