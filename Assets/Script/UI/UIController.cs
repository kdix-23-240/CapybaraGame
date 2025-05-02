using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;
    
    void Awake()
    {
        scoreText.text = "0 m";
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score + "m";
    }

    public void SetLifeText(int life)
    {
        lifeText.text = "Life" + life;
    }
}
