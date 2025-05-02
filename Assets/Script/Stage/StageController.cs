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
        stageListInGame.Add(initialStage);
        
        for(int i = 0; i < initialStageCount; i++)
        {
            int index = Random.Range(0, stages.Count);
            GameObject stage = Instantiate(stages[index], new Vector3((i + 1) * stageWidth - 0.001f, -4, 0), Quaternion.identity);
            stageListInGame.Add(stage);
        }
    }

    void Update()
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
