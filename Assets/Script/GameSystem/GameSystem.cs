using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    [SerializeField] UIController UIcontroller;
    [SerializeField] StageController Stagecontroller;
    
    private int score = 0;
    private float time = 0;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.OnJump();
        }
        time += Time.deltaTime;
        if(time >= 0.5f)
        {
            score += 10;
            time = 0;
        }
        UIcontroller.SetScoreText(score);
        UIcontroller.SetLifeText(Player.Life);
        if(score > 600)
        {
            Stagecontroller.StageSpeed = 7;
        }
    }
}
