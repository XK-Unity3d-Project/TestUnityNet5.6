using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour
{
	NetworkClient myClient;
	NetworkManager netManager;
	void Start()
	{
		netManager = GetComponent<NetworkManager>();
	}

	void Update () 
	{
		if (netManager.isNetworkActive)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			SetupServer(0);
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			SetupClient();
		}
	}

	void OnGUI()
	{
		if (netManager.isNetworkActive)
		{
			return;
		}

		GUI.Label(new Rect(2, 10, 150, 100), "Press S for server");
		GUI.Label(new Rect(2, 30, 150, 100), "Press C for client");
	}

	// Create a server and listen on a port
	public void SetupServer(byte key = 0)
	{
		//Debug.Log("isNetworkActive " + netManager.isNetworkActive);
		if (netManager.isNetworkActive) {
			return;
		}

		switch (key) {
			case 0:
				//创建服务器,同时在服务器也可以产生主角.
				netManager.StartHost();
				break;
			case 1:
				//创建服务器,同时在服务器不产生主角.
				netManager.StartServer();
				break;
		}

	}

	// Create a client and connect to the server port
	public void SetupClient()
	{
		netManager.StartClient();
	}
}