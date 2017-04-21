﻿using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;

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
        if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0)
            if (AirConsole.instance.GetControllerDeviceIds().Count == GameManager.manager.playerCount)
                AirConsole.instance.SetActivePlayers(GameManager.manager.playerCount);
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
            if (AirConsole.instance.GetControllerDeviceIds().Count >= 2)
            {
                //StartGame();
                Debug.Log("Disconnect but still here");
            }
            else
            {
                AirConsole.instance.SetActivePlayers(0);
                Debug.Log("Disconnect");
                //ResetBall(false);
                //uiText.text = "PLAYER LEFT - NEED MORE PLAYERS";
            }
        }
    }

    /// <summary>
    /// We check which one of the active players has moved the paddle.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="data">Data.</param>
    void OnMessage(int device_id, JToken data)
    {
        Debug.Log("Message: " + data.ToString());
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (active_player == -1) return;
        JToken parsedData = JToken.Parse(data.ToString());
        if ((string)parsedData["type"] == "move")
        {
            Vector2 dir = Vector2.zero;
            if ((string)parsedData["axes"] == "x")
                dir.x = (float)parsedData["moveAmount"];
            if ((string)parsedData["axes"] == "y")
                dir.y = (float)parsedData["moveAmount"];
            GameManager.manager.GetPlayer(active_player).SetDirection(dir);
        }
        else if ((string)parsedData["type"] == "bomb")
        {
            if ((bool)parsedData["bomb"])
                GameManager.manager.GetPlayer(active_player).SetBomb();
        }
    }
}