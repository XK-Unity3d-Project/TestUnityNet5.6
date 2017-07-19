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
			SetupServer();
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			SetupClient();
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			SetupServer();
			SetupLocalClient();
		}
	}

	void OnGUI()
	{
		if (netManager.isNetworkActive)
		{
			return;
		}
		GUI.Label(new Rect(2, 10, 150, 100), "Press S for server");     
		GUI.Label(new Rect(2, 30, 150, 100), "Press B for both");       
		GUI.Label(new Rect(2, 50, 150, 100), "Press C for client");
	}

	// Create a server and listen on a port
	public void SetupServer()
	{
		//Debug.Log("isNetworkActive " + netManager.isNetworkActive);
		if (netManager.isNetworkActive) {
			return;
		}
		netManager.StartHost();
	}

	// Create a client and connect to the server port
	public void SetupClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);     
		myClient.Connect("127.0.0.1", 4444);
	}

	// Create a local client and connect to the local server
	public void SetupLocalClient()
	{
		myClient = ClientScene.ConnectLocalServer();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);
	}

	// client function
	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
	}
}