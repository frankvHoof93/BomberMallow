using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager manager = null;

    public int playerCount;

    private Dictionary<int, Player> players;

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
        playerCount = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        players = new Dictionary<int, Player>();
        if (playerCount >= 2)
        {
            GameObject player1 = (GameObject)Instantiate(Resources.Load<GameObject>("Player1"), new Vector3(1, 0.5f, 9), Quaternion.identity);
            player1.name = "Player1";
            GameObject player2 = (GameObject)Instantiate(Resources.Load<GameObject>("Player2"), new Vector3(9, 0.5f, 1), Quaternion.identity);
            player2.name = "Player2";
            players.Add(AirConsole.instance.GetActivePlayerDeviceIds[0], player1.GetComponent<Player>());
            players.Add(AirConsole.instance.GetActivePlayerDeviceIds[1], player2.GetComponent<Player>());
        }
        if (playerCount >= 3)
        {
            GameObject player3 = (GameObject)Instantiate(Resources.Load<GameObject>("Player3"), new Vector3(9, 0.5f, 9), Quaternion.identity);
            player3.name = "Player3";
            players.Add(AirConsole.instance.GetActivePlayerDeviceIds[2], player3.GetComponent<Player>());
        }
        if (playerCount >= 4)
        {
            GameObject player4 = (GameObject)Instantiate(Resources.Load<GameObject>("Player4"), new Vector3(1, 0.5f, 1), Quaternion.identity);
            player4.name = "Player4";
            players.Add(AirConsole.instance.GetActivePlayerDeviceIds[3], player4.GetComponent<Player>());
        }
    }
    
    /// <summary>
    /// 0-Indexed!!
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Player GetPlayer(int playerID)
    {
<<<<<<< HEAD
=======
        foreach (int k in players.Keys)
        {
            Debug.Log("Devid " + k);
        }
>>>>>>> origin/master
        return players[playerID];
    }

    public void RemovePlayer(int devId)
    {
        if (players.ContainsKey(devId))
            players.Remove(devId);
        if (SceneManager.GetActiveScene().buildIndex == 1)
            Debug.Log("GAMEOVER!!!!!!!!!!!");
    }
}
