using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// インゲームシーンの各種UIレイヤの管理クラス.
public class UiLayers : MonoBehaviour
{
    #region Private Serialize Fields

    // シーンコントローラ.
    [SerializeField]
    private SceneInGameController _sceneController = null;

    // 状態別に表示するレイヤ.
    [SerializeField]
    private RectTransform _layerGameStart = null;
    [SerializeField]
    private RectTransform _layerPause = null;
    [SerializeField]
    private RectTransform _layerGameClear = null;

    #endregion


    #region Private Fields
    // レイヤ配列(一括処理用).
    private RectTransform[] _layers = new RectTransform[3]{null, null, null};

    #endregion


    #region Public Functions

    public void Initialize()
    {
        Debug.Assert(_sceneController != null);

        Debug.Assert(_layerGameStart != null);
        Debug.Assert(_layerPause != null);
        Debug.Assert(_layerGameClear != null);

        _layers[0] = _layerGameStart;
        _layers[1] = _layerPause;
        _layers[2] = _layerGameClear;

        PrintLayer(0);
    }

    public void StartGame()
    {
        HideLayers();
    }

    public void ClearGame()
    {
        PrintLayer(2);
    }

    public void Pause()
    {
        PrintLayer(1);
    }

    public void Resume()
    {
        HideLayers();
    }

    #endregion


    #region Private Functions

    private void PrintLayer(int index)
    {
        Debug.Assert(0 <= index && index <= 2);
        HideLayers();
        _layers[index].gameObject.SetActive(true);
    }

    private void HideLayers()
    {
        foreach (var layer in _layers)
        {
            layer.gameObject.SetActive(false);
        }
    }

    #endregion
}
