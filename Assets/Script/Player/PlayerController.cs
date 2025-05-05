using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private const float LongPushTime = 0.17f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float strongJumpPower = 10.0f;
    [SerializeField] private GameObject lifeIcon;
    private bool isHit = false;// ダメージを受けたかどうか
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float damageTime = 10f;
    [SerializeField] private float flashTime = 0.1f;
    private int life;
    private bool alive;
    private Color color;

    public bool Alive { get => alive; }
    public int Life { get => life; }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        life = 3;
        alive = true;
    }

    void Update()
    {
        if (GameStateEnum._currentGameState == GameStateEnum.GameState.Start)
        {
            Reset();
        }
        if (gameObject.transform.position.x < -8)
        {
            GameOver();
        }
    }

    /// <summary>
    /// 短押しアクション
    /// 小さく飛ぶ
    /// </summary>
    public void ShortPushed()
    {
        if (jumpCount >= 1)
        {
            return;
        }

        /* 短押しアクション */
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpCount++;
    }

    /// <summary>
    /// 長押しアクション
    /// 大きく飛ぶ
    /// </summary>
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

    /// <summary>
    /// 初期化
    /// </summary>
    private void Reset()
    {
        Debug.Log("リセット");
        Const.life = 3;
        jumpCount = 0;
        spriteRenderer.color = color;

    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    private void GameOver()
    {
        Debug.Log("GameOver");
        GameStateEnum._currentGameState = GameStateEnum.GameState.Result;
    }

    /// <summary>
    /// 衝突判定
    /// ステージに衝突したらジャンプカウントをリセット
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            jumpCount = 0;
        }
    }

    /// <summary>
    /// 衝突判定
    /// 岩に衝突したらライフを減らす
    /// ライフが0になったらゲームオーバー
    /// ライフアイコンを一つ削除
    /// 岩はすり抜けるようにした
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ダメージを受けてすぐは無敵
        if (!isHit)
        {
            if (other.gameObject.CompareTag("Damage"))
            {
                Const.life--;
                gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
                // lifeIconの子オブジェクトを一つ削除
                if (Const.life >= 0)
                {
                    Destroy(lifeIcon.transform.GetChild(Const.life).gameObject);
                }
                Debug.Log("岩に当たった");
                StartCoroutine(DamageCoroutine());

                if (Const.life <= 0)
                {
                    GameOver();
                }
            }
        }
    }

    //無敵時間を作るコルーチン
    //ダメージを受けたときに実行する
    //colorのa値を変えて透明不透明を切り替える
    private IEnumerator DamageCoroutine()
    {
        isHit = true;
        Debug.Log("コルーチン");
        for (int i = 0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
        isHit = false;
    }
}