using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigidbody;
    float bulletSpeed = 10f;
   
    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        Invoke("BulletDestroy", 2f);
    }
    private void Update()
    {
        rigidbody.velocity = transform.forward * bulletSpeed;
    }
    void BulletDestroy()
    {
        rigidbody.velocity = Vector3.zero;
        transform.localScale=new Vector3(1f, 1f, 1f);
        SimplePool.Despawn(this.gameObject);
    }


}
