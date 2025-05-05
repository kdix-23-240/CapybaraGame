using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    [SerializeField] List<GameObject> stages;
    [SerializeField] GameObject initialStage;
    [SerializeField] float speed = 5.0f;
    private const float stageWidth = 20f;
    private const int initialStageCount = 5;
    private List<GameObject> stageListInGame = new List<GameObject>();
    public float StageSpeed{get => speed;set => speed = value;}


    void Start()
    {
        SetUpStage();
    }

    void Update()
    {
        MoveStage();
    }

    //最初にランダムに5つステージを作成し、生成する
    //ゲームに生成されたと同時にstageListInGameに追加する
    private void SetUpStage()
    {
        stageListInGame.Add(initialStage);
        
        for(int i = 0; i < initialStageCount; i++)
        {
            int index = Random.Range(0, stages.Count);
            GameObject stage = Instantiate(stages[index], new Vector3((i + 1) * stageWidth, -4, 0), Quaternion.identity);
            stageListInGame.Add(stage);
        }
    }

    //ステージをspeed移動させる
    //画面の外にstageWidth二個分移動したら削除されて新しく別のステージが生成される
    //生成されたと同時にリストに追加する
    private void MoveStage()
    {
        for(int i = 0; i < stageListInGame.Count; i++)
        {
            stageListInGame[i].transform.position += Vector3.left * speed * Time.deltaTime;

            if(stageListInGame[i].transform.position.x < stageWidth * -2)
            {
                Destroy(stageListInGame[i]);
                stageListInGame.RemoveAt(i);
                int index = Random.Range(0, stages.Count);
                GameObject stage = Instantiate(stages[index], new  Vector3((stageListInGame.Count -1) * stageWidth, -4, 0),  Quaternion.identity);
                stageListInGame.Add(stage);
            }
        }
    }

}
