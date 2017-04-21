using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.SceneManagement;

public class AirConsoleController : MonoBehaviour {

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    /// <summary>
	/// We start the game if 2 players are connected and the game is not already running (activePlayers == null).
	/// 
	/// NOTE: We store the controller device_ids of the active players. We do not hardcode player device_ids 1 and 2,
	///       because the two controllers that are connected can have other device_ids e.g. 3 and 7.
	///       For more information read: http://developers.airconsole.com/#/guides/device_ids_and_states
	/// 
	/// </summary>
	/// <param name="device_id">The device_id that connected</param>
	void OnConnect(int device_id)
    {
        AirConsole.instance.SetActivePlayers(4);
        if (SceneManager.GetActiveScene().buildIndex == 0)
            MainMenuController.controller.SetPlayerAmount(AirConsole.instance.GetActivePlayerDeviceIds.Count);
    }

    /// <summary>
    /// If the game is running and one of the active players leaves, we reset the game.
    /// </summary>
    /// <param name="device_id">The device_id that has left.</param>
    void OnDisconnect(int device_id)
    {
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (active_player != -1)
        {
        }
    }

    /// <summary>
    /// We check which one of the active players has moved the paddle.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="data">Data.</param>
    void OnMessage(int device_id, JToken data)
    {
        Debug.Log(device_id + " sent Message: " + data.ToString());
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (active_player == -1) return;
        JToken parsedData = JToken.Parse(data.ToString());
        if (SceneManager.GetActiveScene().buildIndex == 0)
            if ((string)parsedData["type"] == "start")
            {
                SceneManager.LoadScene(1);
            }
        if (SceneManager.GetActiveScene().buildIndex == 1)
            if ((string)parsedData["type"] == "move")
            {
                Vector2 dir = Vector2.zero;
                if ((string)parsedData["axes"] == "x")
                    dir.x = (float)parsedData["moveAmount"];
                if ((string)parsedData["axes"] == "y")
                    dir.y = (float)parsedData["moveAmount"];
                Debug.Log("PlayerNULL? " + (GameManager.manager.GetPlayer(device_id) == null));
                Player p = GameManager.manager.GetPlayer(device_id);
                p.SetDirection(dir);
                GameManager.manager.GetPlayer(device_id).SetDirection(dir);
            }
            else if ((string)parsedData["type"] == "bomb")
            {
                if ((bool)parsedData["bomb"])
                    GameManager.manager.GetPlayer(device_id).SetBomb();
            }
    }
}
