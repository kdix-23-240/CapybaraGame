using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    private float gameTime;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Text scoreText;

    void Start()
    {
        gameTime = 0;
        startPanel.SetActive(true);
        resultPanel.SetActive(false);
        GameStateEnum._currentGameState = GameStateEnum.GameState.Start;
    }

    void Update()
    {
        if(GameStateEnum._currentGameState == GameStateEnum.GameState.Start)
        {
            resultPanel.SetActive(false);
            startPanel.SetActive(true);
        }
        else if(GameStateEnum._currentGameState == GameStateEnum.GameState.Game)
        {
            startPanel.SetActive(false);
            resultPanel.SetActive(false);
            gameTime += Time.deltaTime;
        }
        else if(GameStateEnum._currentGameState == GameStateEnum.GameState.Result)
        {
            startPanel.SetActive(false);
            resultPanel.SetActive(true);
            scoreText.text = "すこあ: " + ((int)(gameTime * 10)).ToString();
        }
    }
}