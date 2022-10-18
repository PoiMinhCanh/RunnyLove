using UnityEngine;
using UnityEngine.UI;

public class EndGameMessage : MonoBehaviour
{

    private Text text;

    private const string _wonMessage = "You Won!";
    private const string _lostMessage = "You Lost!";

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        GameState gameState = GameStateController.Instance.gameState;
        if (gameState == GameState.WON)
        {
            text.text = _wonMessage;
        }
        else if (gameState == GameState.LOST)
        {
            text.text = _lostMessage;
        }
    }

}
