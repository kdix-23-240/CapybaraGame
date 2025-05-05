/// <summary>
/// ゲームの状態を管理するクラス
/// ゲームの状態を列挙型で定義する
/// ゲームの状態は、スタート、ゲーム中、リザルトの3つ
/// </summary>
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