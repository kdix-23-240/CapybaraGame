using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// 背景の動きの制御を行うクラス
/// webサイトから拾ってきたので理解はしてない
/// </summary>
[RequireComponent(typeof(Image))]
public class BackGroundMover : MonoBehaviour
{
	private const float k_maxLength = 1f;
	private const string k_propName = "_MainTex";

	private Vector2 m_offsetSpeed;
    [SerializeField] private float m_offsetSpeedNormal = 0.1f;
    [SerializeField] private float m_offsetSpeedFast = 0.2f;
    [SerializeField] private float m_offsetSpeedVeryFast = 0.3f;
    [SerializeField] private float m_offsetSpeedFastest = 0.4f;

	private Material m_copiedMaterial;

	private void Start()
	{
		var image = GetComponent<Image>();
        m_offsetSpeed = new Vector2(m_offsetSpeedNormal, 0f);
		m_copiedMaterial = image.material;

		// マテリアルがnullだったら例外が出ます。
		Assert.IsNotNull(m_copiedMaterial);
	}

	private void Update()
	{
        if(GameStateEnum._currentGameState == GameStateEnum.GameState.Game)
        {
            ChangeSpeedState();
            BGMove();
        }
	}

    private void BGMove()
    {
        if (Time.timeScale == 0f)
		{
			return;
		}

		// xとyの値が0 ～ 1でリピートするようにする
		var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
		var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
		var offset = new Vector2(x, y);
		m_copiedMaterial.SetTextureOffset(k_propName, offset);
    }

    private void ChangeSpeedState()
    {
        switch (StageSpeedStateEnum._currentStageSpeedState)
        {
            case StageSpeedStateEnum.StageSpeedState.Normal:
                m_offsetSpeed = new Vector2(m_offsetSpeedNormal, 0f);
                break;
            case StageSpeedStateEnum.StageSpeedState.Fast:
                m_offsetSpeed = new Vector2(m_offsetSpeedFast, 0f);
                break;
            case StageSpeedStateEnum.StageSpeedState.VeryFast:
                m_offsetSpeed = new Vector2(m_offsetSpeedVeryFast, 0f);
                break;
            case StageSpeedStateEnum.StageSpeedState.Fastest:
                m_offsetSpeed = new Vector2(m_offsetSpeedFastest, 0f);
                break;
        }
    }
}