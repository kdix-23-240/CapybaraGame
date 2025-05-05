using UnityEngine;
using UnityEngine.UI;

public class JampButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float frameCounter = 0;// 時間を計測するための変数
    private float longPushTime = 0.17f;// 長押しと判定する時間
    private bool isPushed = false;// ボタンが押されているかどうかのフラグ

    void Update()
    {
        // ゲーム中なら判定を行う
        if (GameStateEnum._currentGameState == GameStateEnum.GameState.Game)
        {
            // ボタンが押されている場合、時間を計測する
            if (isPushed)
            {
                frameCounter += Time.deltaTime;// 時間を計測
                // 長押し時間を超えた場合、長押しと判定する
                if (frameCounter > longPushTime)
                {
                    player.GetComponent<PlayerController>().LongPushed();// 長押しアクションを実行
                    frameCounter = 0;// 時間をリセット
                    isPushed = false;// ボタンを離した状態にする
                }
            }
        }
    }

    /// <summary>
    /// ボタンが押されている状態
    /// </summary>
    public void OnDown()
    {
        Debug.Log("OnDown");
        isPushed = true;
    }

    /// <summary>
    /// ボタンが離された状態
    /// </summary>
    public void OnUp()
    {
        Debug.Log("OnUp" + frameCounter);
        Jump();
        isPushed = false;
    }

    /// <summary>
    /// ボタンが押されたときの処理
    /// 短押しと長押しの判定を行い、アクションを実行する
    /// </summary>
    private void Jump()
    {
        if (frameCounter < longPushTime)
        {
            player.GetComponent<PlayerController>().ShortPushed();
        }
        frameCounter = 0;
    }
}