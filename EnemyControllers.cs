using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyControllers : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("isServer "+isServer);
		if (!isServer)
		{
			return;
		}

		if (Time.frameCount % 100 != 0) {
			return;
		}
		CmdFire(2f);
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