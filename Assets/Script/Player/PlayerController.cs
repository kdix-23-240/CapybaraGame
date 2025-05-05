using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private const float LongPushTime = 0.17f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float strongJumpPower = 10.0f;
    [SerializeField] private GameObject lifeIcon;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (gameObject.transform.position.x < -8)
        {
            GameOver();
        }
    }

    public void ShortPushed()
    {
        if(jumpCount >= 1)
        {
            return;
        }

        /* 短押しアクション */
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpCount++;
    }

    public void LongPushed()
    {
        if (jumpCount >= 1)
        {
            return;
        }

        /* 長押しアクション */
        rb.AddForce(Vector2.up * strongJumpPower, ForceMode2D.Impulse);
        jumpCount++;
    }

    private void GameOver()
    {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
        Debug.Log("Game Over!!!!!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            Const.life--;
            // lifeIconの子オブジェクトを一つ削除
            if (Const.life > 0)
            {
                Destroy(lifeIcon.transform.GetChild(Const.life).gameObject);
            }
            Debug.Log("岩に当たった");
            if (Const.life <= 0)
            {
                GameOver();
            }
        }
    }
}
