using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スタートボタンの処理
/// スタート画面からゲーム画面に遷移するボタン
/// </summary>


public class StartButton : MonoBehaviour
{    
    [SerializeField] private Life lifeUI;
    [SerializeField] private StageController stageController;
    public void OnClick()
    {
        GameStateEnum._currentGameState = GameStateEnum.GameState.Game;
        lifeUI.Reset();
        stageController.Reset();
    }
}