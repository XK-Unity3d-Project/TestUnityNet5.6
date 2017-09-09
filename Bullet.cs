using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float TimeSpawn = 0f;
    bool IsDestoryObj = false;
    EnemyControllers EnemyCom;
    Rigidbody Rig;
    void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("OnCollisionEnter...");
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();
		if (health  != null)
		{
			health.TakeDamage(10);
		}
        DestroyThis();
    }
    void Update()
    {
        if (Time.time - TimeSpawn < 2f || IsDestoryObj)
        {
            return;
        }
        DestroyThis();
    }
    public void InitBulletInfo(EnemyControllers enemyScript = null)
    {
        EnemyCom = enemyScript;
        TimeSpawn = Time.time;
    }
    public void MoveBullet(float speed)
    {
        if (Rig == null)
        {
            Rig = GetComponent<Rigidbody>();
        }
        IsDestoryObj = false;
        Rig.isKinematic = false;
        Rig.velocity = transform.forward * 6;
    }
    void DestroyThis()
    {
        try
        {
            IsDestoryObj = true;
            if (EnemyCom != null)
            {
                EnemyCom.ObjListManage.CloseObjectInfoFromList(gameObject);
                Rig.velocity = Vector3.zero;
                Rig.isKinematic = true;
                transform.position = new Vector3(0f, -99999f, 0f);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Unity: -> " + ex);
            throw;
        }
    }
}