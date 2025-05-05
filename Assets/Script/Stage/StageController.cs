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
    private List<GameObject> stageListInGame;
    private int createdStateCount = 0;


    void Start()
    {
        
    }


    void Update()
    {
        if (GameStateEnum._currentGameState == GameStateEnum.GameState.Game)
        {
            ChangeSpeedState();
            AdjustStageSpeed();
            if (stageListInGame != null) 
            {
                MoveStage();
            }
        }
    }

    private void MoveStage()
    {
        
            for (int i = stageListInGame.Count - 1; i >= 0; i--)
            {
                if (stageListInGame[i] == null) continue;
                stageListInGame[i].transform.position += Vector3.left * speed * Time.deltaTime;

                if (stageListInGame[i].transform.position.x < stageWidth * -2)
                {
                    Destroy(stageListInGame[i]);
                    stageListInGame.RemoveAt(i);
                    int index = Random.Range(0, stages.Count);
                    GameObject stage = Instantiate(stages[index], new Vector3((stageListInGame.Count - 1) * stageWidth, -5, 0), Quaternion.identity);
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

    /// <summary>
    /// ステージの速度状態を変更する
    /// ステージの生成数に応じて速度状態を変更する
    /// ステージの速度が変わるタイミングはかなりテキトーなので、調整が必要
    /// </summary>
    private void ChangeSpeedState()
    {
        if (createdStateCount > 16)
        {
            StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.Fastest;
            Debug.Log("速度: " + StageSpeedStateEnum._currentStageSpeedState);
        }
        else if (createdStateCount > 12)
        {
            StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.VeryFast;
            Debug.Log("速度: " + StageSpeedStateEnum._currentStageSpeedState);
        }
        else if (createdStateCount > 8)
        {
            StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.Fast;
            Debug.Log("速度: " + StageSpeedStateEnum._currentStageSpeedState);
        }
    }

    public void Reset()
    {
        if(stageListInGame != null)
        {
            for (int i = stageListInGame.Count - 1; i >= 0; i--)
            {
                if (stageListInGame[i] != null)
                {
                    Destroy(stageListInGame[i]);
                    stageListInGame.RemoveAt(i);
                }
            }
            stageListInGame.Clear();
            
            stageSetUp();
        }else if(stageListInGame == null)
        {
            stageListInGame = new List<GameObject>();
            stageSetUp();
        }
        
    }

    public void stageSetUp()
    {   
        StageSpeedStateEnum._currentStageSpeedState = StageSpeedStateEnum.StageSpeedState.Normal;
        createdStateCount = 0;
        speed = speedNomal;
        GameObject initial = Instantiate(initialStage, new Vector3(0, -5, 0), Quaternion.identity);
        stageListInGame.Add(initial);  
        for (int i = 0; i < initialStageCount; i++)
        {
            int index = Random.Range(0, stages.Count);
            GameObject stage = Instantiate(stages[index], new Vector3((i + 1) * stageWidth, -5, 0), Quaternion.identity);
            stageListInGame.Add(stage);
            createdStateCount++;
        }
    }

}
