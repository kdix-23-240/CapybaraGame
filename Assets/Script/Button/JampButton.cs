using UnityEngine;
using UnityEngine.UI;

public class JampButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float frameCounter = 0;
    private float longPushTime = 0.17f;
    private bool isPushed = false;

    void Update()
    {
        if (GameStateEnum._currentGameState == GameStateEnum.GameState.Game)
        {
            if (isPushed)
            {
                frameCounter += Time.deltaTime;
                if (frameCounter > longPushTime)
                {
                    player.GetComponent<PlayerController>().LongPushed();
                    frameCounter = 0;
                    isPushed = false;
                }
            }
        }
    }

    public void OnDown()
    {
        Debug.Log("OnDown");
        isPushed = true;
    }

    public void OnUp()
    {
        Debug.Log("OnUp" + frameCounter);
        Jump();
        isPushed = false;
    }

    private void Jump()
    {
        if (frameCounter < longPushTime)
        {
            player.GetComponent<PlayerController>().ShortPushed();
        }
        frameCounter = 0;
    }
}