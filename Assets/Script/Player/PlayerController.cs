using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int jumpCount = 0;
    [SerializeField] private float jumpPower = 15.0f;
    [SerializeField] private float damageTime = 10f;
    [SerializeField] private float flashTime = 0.1f;
    private int life;
    private bool alive;

    public bool Alive{get => alive;}
    public int Life{get => life;}
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        life = 3;
        alive = true;
    }

    //ジャンプを制限する関数
    //GameSystemクラスで実行している
    public void OnJump()
    {
        if(jumpCount >= 1) return;

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpCount++;
    }

    //タグでどのオブジェクトと衝突したか判定するメソッド
    //ステージに接触したらジャンプカウントを0にしてもういちどジャンプできるようにする
    //ダメージオブジェクトに衝突したらダメージ関数を呼び出す。
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Stage"))
        {
            jumpCount = 0;
        }
        
        if(other.gameObject.CompareTag("Damage"))
        {
            Damage();
            StartCoroutine(DamageCoroutine());
        }
    }

    //無敵時間を作るコルーチン
    //ダメージを受けたときに実行する
    //colorのa値を変えて透明不透明を切り替える
    IEnumerator DamageCoroutine()
    {
        Color color = spriteRenderer.color;

        for(int i = 0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void Damage()
    {   
        life--;
        Debug.Log("岩に当たった");
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        if(life <= 0)
        {
                alive = false;
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                #else
                    Application.Quit();//ゲームプレイ終了
                #endif
                Debug.Log("Game Over!!!!!");
        }
    }
}
