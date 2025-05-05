using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameObject lifeIcon;

    void Start()
    {
        for (int i = 0; i < Const.life; i++)
        {
            GameObject icon = Instantiate(lifeIcon, new Vector3(0, 0, 0), Quaternion.identity);
            icon.transform.SetParent(transform);
            icon.transform.localPosition = new Vector3(i * 80, 0, 0);
            icon.transform.localScale = new Vector3(30, 30, 30);
        }
    }
}