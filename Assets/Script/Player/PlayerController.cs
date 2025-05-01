using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private int jumpCount = 0;
    [SerializeField] private float jumpPower = 5.0f;
    private int life;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        life = 3;
    }

    
    public void OnJump()
    {
        if(jumpCount >= 1) return;

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Stage"))
        {
            jumpCount = 0;
        }
        
        if(other.gameObject.CompareTag("Damage"))
        {
            life--;
            Debug.Log("岩に当たった");
            if(life <= 0)
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                #else
                    Application.Quit();//ゲームプレイ終了
                #endif
                Debug.Log("Game Over!!!!!");
            }
        }
    }
}
