/// <summary>
/// ステージのスピード状態を管理するクラス
/// ステージのスピード状態を列挙型で定義する
/// /// ステージのスピード状態は、ノーマル、ファースト、ベリーファースト、ファステストの4つ
/// </summary>
public class StageSpeedStateEnum
{
    public enum StageSpeedState
    {
        Normal,
        Fast,
        VeryFast,
        Fastest,
    }

    public static StageSpeedState _currentStageSpeedState = StageSpeedState.Normal;
}