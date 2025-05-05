using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public void OnClick()
    {
        GameStateEnum._currentGameState = GameStateEnum.GameState.Game;
    }
}