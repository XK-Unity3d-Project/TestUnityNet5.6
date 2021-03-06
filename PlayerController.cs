﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public MeshRenderer PlayerMesh;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire(2f);
		}
	}

	public override void OnStartLocalPlayer()
	{
		PlayerMesh.material.color = Color.blue;
	}

	[Command]
	void CmdFire(float timeVal)
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, timeVal);        
	}
}