using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigidbody;
    float bulletSpeed = 10f;
    public int bulletLevel = 0;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        Invoke("BulletDestroy", 2f);
    }
    private void Update()
    {
        rigidbody.velocity = transform.forward * bulletSpeed;
    }
    public void BulletDestroy()
    {
        rigidbody.velocity = Vector3.zero;
        transform.localScale = new Vector3(1f, 1f, 1f);
        bulletLevel = 0;
        SimplePool.Despawn(this.gameObject);
    }


}
