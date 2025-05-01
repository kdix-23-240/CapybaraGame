using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] PlayerController Player;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.OnJump();
        }
    }
}
