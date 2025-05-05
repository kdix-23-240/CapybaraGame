using UnityEngine;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour
{
    public void OnClick()
    {
        GameStateEnum._currentGameState = GameStateEnum.GameState.Start;
    }
}