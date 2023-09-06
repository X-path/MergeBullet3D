using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotType
{
    Forward,
    Triple,
    Big
}
public class Gun : MonoBehaviour
{
    public ShotType shotType;
    public int gunBulletLevel = 0;

    public float fireRate = .15f;

    private float nextFireTime = 0.0f;
    GameObject bulletPrefab;

    [SerializeField] Transform forward;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    GameObject bullet;
    private void Start()
    {
        bulletPrefab = GunsManager.instance.bulletPrefab;
    }
    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (shotType == ShotType.Forward)
            {
                FireCreate(forward);
            }
            else if (shotType == ShotType.Triple)
            {
                FireCreate(forward);
                FireCreate(left);
                FireCreate(right);
            }
            else if (shotType == ShotType.Big)
            {
                FireCreate(forward);
                bullet.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
            }
        }
    }
    void FireCreate(Transform _shotTransfom)
    {
        GameObject _go = SimplePool.Spawn(bulletPrefab, _shotTransfom.position, _shotTransfom.rotation);
        bullet = _go;

    }
}
