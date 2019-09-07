using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerMovementMathFunctions
{
    #region Public Enums
    public enum Type
    {
        LinerEquation01,
        QuadraticEquation01,
        CubicEquation01,
        QuarticEquation01,
    }
    #endregion

    #region Private Static Fields
    private static Dictionary<Type, Func<float, float>> _typeToFunc = new Dictionary<Type, Func<float, float>> {
        {
            Type.LinerEquation01,
            (x) => -0.18f * x + 18.0f
        },
        {
            Type.QuadraticEquation01,
            (x) => -0.0068f * x * x + 0.68f * x
        },
    };
    #endregion


    #region Public Functions

    public static float GetVelocityByStamina(float stamina, Type equationType)
    {
        if (! _typeToFunc.ContainsKey(equationType)) {
            Debug.Assert(false);
            return 0.0f;
        }

        Func<float, float> func = _typeToFunc[equationType];
        return func(stamina);
    }

    #endregion
}
