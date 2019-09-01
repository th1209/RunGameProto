using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コライダの種別.
public enum CollisionType
{
    OwnPlayer,
    CpuPlayer,
    OtherPlayer,
    NonePlayer,
}

public static class CollisionUtility
{
    public static CollisionType CheckCollider2DType(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player == null) {
            return CollisionType.NonePlayer;
        }

        // TODO: PlayerのI/Fが決まったら､残りのタイプ判定を実装する.
        return CollisionType.OwnPlayer;
    }
}