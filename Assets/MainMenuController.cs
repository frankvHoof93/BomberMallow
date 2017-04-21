using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public static MainMenuController controller = null;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private void Awake()
    {
        if (controller == null)
        {
            controller = this;
            SetPlayerAmount(0);
        }
    }

    public void SetPlayerAmount(int amount)
    {
        player1.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        if (amount > 0)
            player1.SetActive(true);
        if (amount > 1)
            player2.SetActive(true);
        if (amount > 2)
            player3.SetActive(true);
        if (amount > 3)
            player4.SetActive(true);
    }
}
