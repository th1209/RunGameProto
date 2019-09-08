using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーと衝突時に､ゲーム自体やステージに変化をもたらす衝突判定器.
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GameEventCollider : MonoBehaviour, ICollider
{

}
