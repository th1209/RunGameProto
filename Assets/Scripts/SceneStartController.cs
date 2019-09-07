using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

struct MovementFunctionOption
{
    public PlayerMovementMathFunctions.Type Type { get; }
    public string Label { get; }
    public MovementFunctionOption(PlayerMovementMathFunctions.Type type, string label)
    {
        Type  = type;
        Label = label;
    }
}

public class SceneStartController : MonoBehaviour
{
    #region Private Serialize Fields

    [SerializeField]
    private Dropdown _dropdownMovementFunction = null;
    [SerializeField]
    private Dropdown _dropdownStaminaHealAmount = null;
    [SerializeField]
    private Dropdown _dropdownStaminaConsumeAmount = null;

    #endregion


    #region Private Serialize Fields

    private MovementFunctionOption[] _movementFunctionOptions;
    private float[] _staminaHealAmountOptions;
    private float[] _staminaConsumeAmountOptions;

    #endregion


    #region Public Functions
    
    public void GoToInGameScene()
    {
        SceneManager.LoadSceneAsync("SceneInGame");
    }

    #endregion


    #region MonoBehaviour CallBacks

    void Start()
    {
        Debug.Assert(_dropdownMovementFunction != null);
        Debug.Assert(_dropdownStaminaHealAmount != null);
        Debug.Assert(_dropdownStaminaConsumeAmount != null);

        _movementFunctionOptions = new MovementFunctionOption[]{
            new MovementFunctionOption(
                PlayerMovementMathFunctions.Type.LinerEquation01,
                "1次方程式01"
            ),
            new MovementFunctionOption(
                PlayerMovementMathFunctions.Type.QuarticEquation01,
                "2次方程式01"
            ),
        };
        _staminaHealAmountOptions = Enumerable.Range((int)InGameParameters.StaminaHealAmountMin, (int)(InGameParameters.StaminaHealAmountMax - InGameParameters.StaminaHealAmountMin + 1))
            .Select((x) => (float)x )
            .ToArray();
        _staminaConsumeAmountOptions = Enumerable.Range((int)InGameParameters.StaminaConsumeAmountMin, (int)(InGameParameters.StaminaConsumeAmountMax - InGameParameters.StaminaConsumeAmountMin + 1))
            .Select((x) => (float)x )
            .ToArray();

        List<string> labels = _movementFunctionOptions.Select(x => x.Label).ToList();
        int index = System.Array.FindIndex(_movementFunctionOptions, x => x.Type == GameConfigRepository.LoadMovementEquationType());
        _dropdownMovementFunction.ClearOptions();
        _dropdownMovementFunction.AddOptions(labels);
        _dropdownMovementFunction.value = (index != -1) ? index : 0;


        labels = _staminaHealAmountOptions.Select(x => x.ToString("")).ToList();
        index = System.Array.FindIndex(_staminaHealAmountOptions, x => x == GameConfigRepository.LoadStaminaHealAmount());
        _dropdownStaminaHealAmount.ClearOptions();
        _dropdownStaminaHealAmount.AddOptions(labels);
        _dropdownStaminaHealAmount.value = (index != -1) ? index : 0;


        labels = _staminaConsumeAmountOptions.Select(x => x.ToString("")).ToList();
        index = System.Array.FindIndex(_staminaConsumeAmountOptions, x => x == GameConfigRepository.LoadStaminaConsumeAmount());
        _dropdownStaminaConsumeAmount.ClearOptions();
        _dropdownStaminaConsumeAmount.AddOptions(labels);
        _dropdownStaminaConsumeAmount.value = (index != -1) ? index : 0;
    }

    #endregion


    #region Ui Callbacks

    public void OnMovementFunctionOptionChanged(int index)
    {
        GameConfigRepository.SaveMovementEquationType(_movementFunctionOptions[index].Type);
    }

    public void OnStaminaHealAmountOptionChanged(int index)
    {
        GameConfigRepository.SaveStaminaHealAmount(_staminaHealAmountOptions[index]);
    }

    public void OnStaminaConsumeAmountOptionChanged(int index)
    {
        GameConfigRepository.SaveStaminaConsumeAmount(_staminaConsumeAmountOptions[index]);
    }

    #endregion
}
