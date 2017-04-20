using UnityEngine;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class airconTest : MonoBehaviour {

    // Use this for initialization
    void Start() {
        AirConsole.instance.onMessage += OnMessage;
    }

    void OnMessage(int from, JToken data)
    {
        AirConsole.instance.Message(from, "Full of pixels!");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
