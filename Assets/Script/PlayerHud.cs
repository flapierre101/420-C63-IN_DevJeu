using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    // Start is called before the first frame update
    public Player Player { get; private set; }


    private void Start()
    {

        Player = GameManager.Instance.Player;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
