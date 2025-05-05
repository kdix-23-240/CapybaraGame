using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    private float gameTime;// ゲーム時間を計測する変数
    [SerializeField] private GameObject startPanel;// スタート画面のUI
    [SerializeField] private GameObject resultPanel;// リザルト画面のUI
    [SerializeField] private Text scoreText;// リザルト画面のスコアを表示するUI
    [SerializeField] private Button jumpButton;// ジャンプボタンのUI
    [SerializeField] private Life life;

    void Start()
    {
        
        startPanel.SetActive(true);
        resultPanel.SetActive(false);
        GameStateEnum._currentGameState = GameStateEnum.GameState.Start;
    }

    /// <summary>
    /// ゲームの状態を更新する
    /// ゲームの状態に応じて、UIを切り替える
    /// </summary>
    void Update()
    {
        if(GameStateEnum._currentGameState == GameStateEnum.GameState.Start)
        {
            resultPanel.SetActive(false);
            jumpButton.interactable = false;
            startPanel.SetActive(true);
            gameTime = 0;
        }
        else if(GameStateEnum._currentGameState == GameStateEnum.GameState.Game)
        {
            startPanel.SetActive(false);
            resultPanel.SetActive(false);
            jumpButton.interactable = true;
            gameTime += Time.deltaTime;
        }
        else if(GameStateEnum._currentGameState == GameStateEnum.GameState.Result)
        {
            startPanel.SetActive(false);
            jumpButton.interactable = false;
            resultPanel.SetActive(true);
            scoreText.text = "すこあ: " + ((int)(gameTime * 10)).ToString();
        }
    }

}