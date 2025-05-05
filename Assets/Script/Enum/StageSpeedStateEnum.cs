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