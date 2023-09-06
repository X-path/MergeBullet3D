using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunsManager : MonoBehaviour
{
    public static GunsManager instance;
    public List<GameObject> guns = new List<GameObject>();
    public List<GameObject> gunsPosList = new List<GameObject>();
    [SerializeField]public GameObject bulletPrefab;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }
    private void Start()
    {
        SimplePool.Preload(bulletPrefab, 150);
    }
    public void GunsAdd(GameObject _gun, int _bulletLevel)
    {
        if (!guns.Contains(_gun))
        {
            guns.Add(_gun);
            _gun.GetComponent<Gun>().gunBulletLevel = _bulletLevel;
        }

    }
    public IEnumerator GunsOrganizing()
    {
        CameraController.instance.ShotStartPos();

        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].transform.DOMove(gunsPosList[i].transform.position, .15f).OnComplete(() =>
            {
                guns[i].transform.parent = GameManager.instance.player.transform;
            });

            yield return new WaitForSeconds(.15f);

        }
    }
    public void GunsFire()
    {
        for(int i=0;i<guns.Count;i++)
        {
            guns[i].GetComponent<Gun>().Fire();
        }
    }
    public void GunsTypeChange(ShotType _shotType)
    {
        for(int i=0;i<guns.Count;i++)
        {
            guns[i].GetComponent<Gun>().shotType=_shotType;
        }
    }
}
