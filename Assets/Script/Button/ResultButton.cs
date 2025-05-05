using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルトボタンの処理
/// リザルト画面からスタート画面に戻るボタン
/// </summary>
public class ResultButton : MonoBehaviour
{
    public void OnClick()
    {
        GameStateEnum._currentGameState = GameStateEnum.GameState.Start;
    }
}