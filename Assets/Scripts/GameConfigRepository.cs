using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム設定値に関する永続化層.
public static class GameConfigRepository
{
    #region Private Static Fields
    private static string _prefsKeyMovementEquationType = "PrefsKeyMovementEquationType";
    private static string _prefsKeyStaminaHealAmount = "PrefsKeyStaminaHealAmount";
    private static string _prefsKeyStaminaCousumeAmount = "PrefsKeyStaminaCousumeAmount";
    #endregion

    #region Save Functions
    public static void SaveMovementEquationType(PlayerMovementMathFunctions.Type value)
    {
        PlayerPrefs.SetInt(_prefsKeyMovementEquationType, (int)value);
    }

    public static void SaveStaminaHealAmount(float value)
    {
        PlayerPrefs.SetFloat(_prefsKeyStaminaHealAmount, value);
    }

    public static void SaveStaminaConsumeAmount(float value)
    {
        PlayerPrefs.SetFloat(_prefsKeyStaminaCousumeAmount, value);
    }
    #endregion

    #region Load Functions
    public static PlayerMovementMathFunctions.Type LoadMovementEquationType()
    {
        if (! PlayerPrefs.HasKey(_prefsKeyMovementEquationType)) {
            return PlayerMovementMathFunctions.Type.LinerEquation01;
        }
        return Converter.ToEnum<PlayerMovementMathFunctions.Type>(PlayerPrefs.GetInt(_prefsKeyMovementEquationType));
    }

    public static float LoadStaminaHealAmount()
    {
        return PlayerPrefs.GetFloat(_prefsKeyStaminaHealAmount , 10.0f);
    }

    public static float LoadStaminaConsumeAmount()
    {
        return PlayerPrefs.GetFloat(_prefsKeyStaminaCousumeAmount , 10.0f);
    }
    #endregion
}
