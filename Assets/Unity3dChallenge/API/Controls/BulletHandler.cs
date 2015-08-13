using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletHandler : MonoBehaviour
{
    public float BulletVelocity = 5.0f;
    public float BulletLifeTime = 5.0f;
    public GameObject Bullet;
    public List<Transform> Muzzles;

    public void Start()
    {
        ObjectPool.CreatePool(Bullet, 10);
    }

    public void Fire()
    {
        FireProjectile();
    }

    private void FireProjectile()
    {
        for (int i = 0; i < Muzzles.Count; i++)
		{
            var bullet = ObjectPool.Spawn(Bullet, null, Muzzles[i].position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * BulletVelocity);
            StartCoroutine(PoolBulletAfterTime(BulletLifeTime, bullet));
		}
    }

    private IEnumerator PoolBulletAfterTime(float time, GameObject bullet)
    {
        yield return new WaitForSeconds(time);

        bullet.Recycle();
    }
}
