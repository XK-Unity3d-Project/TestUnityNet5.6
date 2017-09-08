using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyControllers : NetworkBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    Transform BulletListTr;
    public ObjectListManage ObjListManage = new ObjectListManage();
    void Start()
    {
        GameObject obj = new GameObject();
        obj.name = name + "BulletList";
        BulletListTr = obj.transform;
    }
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
        GameObject bullet = ObjListManage.FindObjectFromList();
        if (bullet == null)
        {
            // Create the Bullet from the Bullet Prefab
            bullet = (GameObject)Instantiate( bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.transform.parent = BulletListTr;
            ObjListManage.AddObjectToList(bullet);
        }
        else
        {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
        }

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);
        Bullet bulletCom = bullet.GetComponent<Bullet>();
        bulletCom.InitBulletInfo(this);
        bulletCom.MoveBullet(6f);
    }
}