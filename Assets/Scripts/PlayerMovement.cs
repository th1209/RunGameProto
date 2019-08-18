using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{   
    #region Public Functions
    public void Jump()
    {
        // TODO ジャンプ処理を実装する.
    }
    #endregion


    #region Private Functions
    private void Run()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = Mathf.Clamp(
            velocity.x + InGameParameters.PlayerAcceleration * Time.deltaTime,
            InGameParameters.PlayerVelocityMin,
            InGameParameters.PlayerVelocityMax);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
    #endregion


    #region MonoBehaviour CallBacks
    void FixedUpdate()
    {
        Run();
    }
    #endregion
}
