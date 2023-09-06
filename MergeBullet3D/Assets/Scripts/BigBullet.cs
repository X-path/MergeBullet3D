using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GunsManager.instance.GunsTypeChange(ShotType.Big);
        }
    }
}
