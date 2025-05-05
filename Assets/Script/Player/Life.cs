using UnityEngine;

/// <summary>
/// ライフを管理するクラス
/// </summary>
public class Life : MonoBehaviour
{
    [SerializeField] private GameObject lifeIcon;

    void Start()
    {
        Reset();
    }

    /// <summary>
    /// ライフの初期化
    /// ライフのアイコンを生成し、親オブジェクトに設定する
    /// ライフの数だけアイコンを生成し、横に並べる
    /// アイコンのサイズを設定する
    /// </summary>
    private void Reset()
    {
        // ライフの初期化
        for (int i = 0; i < Const.life; i++)
        {
            GameObject icon = Instantiate(lifeIcon, new Vector3(0, 0, 0), Quaternion.identity);
            icon.transform.SetParent(transform);
            icon.transform.localPosition = new Vector3(i * 80, 0, 0);
            icon.transform.localScale = new Vector3(30, 30, 30);
        }
    }
}