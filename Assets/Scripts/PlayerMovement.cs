using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{    void FixedUpdate()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = Mathf.Clamp(
            velocity.x + InGameParameters.PlayerAcceleration * Time.deltaTime,
            InGameParameters.PlayerVelocityMin,
            InGameParameters.PlayerVelocityMax);
        GetComponent<Rigidbody2D>().velocity = velocity;
        Debug.Log(GetComponent<Rigidbody2D>().isKinematic);
    }
}
