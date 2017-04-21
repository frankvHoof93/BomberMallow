using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
}

public class GameManager : MonoBehaviour {

    public static GameManager manager = null;

    public int playerCount;

    private List<Player> players;

    private void OnEnable()
    {
        if (manager == null)
            manager = this;
        else Destroy(this.gameObject);
    }

    /// <summary>
    /// Instantiates Players
    /// </summary>
    void Start() {
        players = new List<Player>();
        if (playerCount >= 2)
        {
            GameObject player1 = Instantiate(Resources.Load<GameObject>("Player1"), new Vector3(1, 0.5f, 9), Quaternion.identity);
            player1.name = "Player1";
            GameObject player2 = Instantiate(Resources.Load<GameObject>("Player2"), new Vector3(9, 0.5f, 1), Quaternion.identity);
            player2.name = "Player2";
            players.Add(player1.GetComponent<Player>());
            players.Add(player2.GetComponent<Player>());
        }
        if (playerCount >= 3)
        {
            GameObject player3 = Instantiate(Resources.Load<GameObject>("Player3"), new Vector3(9, 0.5f, 9), Quaternion.identity);
            player3.name = "Player3";
            players.Add(player3.GetComponent<Player>());
        }
        if (playerCount >= 4)
        {
            GameObject player4 = Instantiate(Resources.Load<GameObject>("Player4"), new Vector3(1, 0.5f, 1), Quaternion.identity);
            player4.name = "Player4";
            players.Add(player4.GetComponent<Player>());
        }
    }
    
    /// <summary>
    /// 0-Indexed!!
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Player GetPlayer(int player)
    {
        if (player >= playerCount || player < 0)
            return null;
        return players[player];
    }
}
