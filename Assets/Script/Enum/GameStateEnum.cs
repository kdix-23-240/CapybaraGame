public class GameStateEnum
{
    public enum GameState
    {
        Start,
        Game,
        Result,
    }

    public static GameState _currentGameState = GameState.Start;
}