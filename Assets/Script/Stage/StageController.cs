using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    [SerializeField] List<GameObject> stages;
    [SerializeField] GameObject initialStage;
    private float speed;
    [SerializeField] float speedNomal = 5.0f;
    [SerializeField] float speedFast = 10.0f;
    [SerializeField] float speedVeryFast = 15.0f;
    [SerializeField] float speedFastest = 20.0f;
    private const int stageWidth = 20;
    private const int initialStageCount = 5;
    private List<GameObject> stageListInGame = new List<GameObject>();
    private int createdStateCount = 0;


    void Start()
    {
        speed = speedNomal;
        stageListInGame.Add(initialStage);
        
        for(int i = 0; i < initialStageCount; i++)
        {
            int index = Random.Range(0, stages.Count);
            GameObject stage = Instantiate(stages[index], new Vector3((i + 1) * stageWidth, -5, 0), Quaternion.identity);
            stageListInGame.Add(stage);
            createdStateCount++;
        }
    }

    void Update()
    {
        ChangeSpeedState();
        AdjustStageSpeed();
        for(int i = 0; i < stageListInGame.Count; i++)
        {
            stageListInGame[i].transform.position += Vector3.left * speed * Time.deltaTime;

            if(stageListInGame[i].transform.position.x < stageWidth * -2)
            {
                Destroy(stageListInGame[i]);
                stageListInGame.RemoveAt(i);
                int index = Random.Range(0, stages.Count);
                GameObject stage = Instantiate(stages[index], new  Vector3((stageListInGame.Count -1) * stageWidth, -5, 0),  Quaternion.identity);
                stageListInGame.Add(stage);
                createdStateCount++;
            }
        }
    }

    private void AdjustStageSpeed()
    {
        switch (StageSpeedStateEnum._currentStageSpeedState)
        {
            case StageSpeedStateEnum.StageSpeedState.Normal:
                speed = speedNomal;
                break;
            case StageSpeedStateEnum.StageSpeedState.Fast:
                speed = speedFast;
                break;
            case StageSpeedStateEnum.StageSpeedState.VeryFast:
                speed = speedVeryFast;
                break;
            case StageSpeedStateEnum.StageSpeedState.Fastest:
                speed = speedFastest;
                break;
        }
    }

    private void ChangeSpeedState()
    {
        if (createdStateCount > 9)
        {
            StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.Fast;
            Debug.Log("ステージのスピードが速くなった");
        }
        if (createdStateCount > 13)
        {
            StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.VeryFast;
            Debug.Log("ステージのスピードが非常に速くなった");
        }
        if (createdStateCount > 17)
        {
            StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.Fastest;
            Debug.Log("ステージのスピードが最速になった");
        }
    }
}
