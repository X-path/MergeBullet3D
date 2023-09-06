using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MergeBullet : MonoBehaviour
{
    [SerializeField] public int bulletLevel = 0;
    [SerializeField] public TextMeshPro mergeBulletLevelText;
    [SerializeField] public bool isMergeBulletStart = false;
    [SerializeField] float mergeBulletSpeed = 5f;
    Rigidbody rigidbody;

    void Start()
    {
        FindComponent();
        LevelTextUpdate();

    }
    void FindComponent()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    public void LevelTextUpdate()
    {
        mergeBulletLevelText.text = bulletLevel.ToString();
    }

    private void FixedUpdate()
    {

        if (isMergeBulletStart)
        {
            rigidbody.velocity = Vector3.forward * mergeBulletSpeed;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            StartBlockContact(other.gameObject);
        }
        else if (other.gameObject.layer == 8)
        {
            GunContact(other.gameObject);
        }
    }

    void StartBlockContact(GameObject _go)
    {
        bulletLevel--;
        LevelTextUpdate();
        Destroy(_go);
        if (bulletLevel == 0)
        {
            MergeAreaManager.instance.MergeBulletsCountControll(this, false);
        }
    }
    void GunContact(GameObject _gun)
    {
        GunsManager.instance.GunsAdd(_gun, bulletLevel);
        MergeAreaManager.instance.MergeBulletsCountControll(this, true);
    }
}
