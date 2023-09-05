using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeAreaManager : MonoBehaviour
{
    public static MergeAreaManager instance;
    [SerializeField] GameObject mergeBulletPrefab;
    [SerializeField] public List<MergePoint> mergePoints = new List<MergePoint>();
    [SerializeField] public List<MergeBullet> mergeBullets = new List<MergeBullet>();
    MergeAreaParent mergeAreaParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }
    void Start()
    {
        mergeAreaParent = FindObjectOfType<MergeAreaParent>();

        for (int i = 0; i < mergeAreaParent.transform.childCount; i++)
        {
            mergePoints.Add(mergeAreaParent.transform.GetChild(i).transform.GetComponent<MergePoint>());
        }


        BulletStartCreate();
    }

    void BulletStartCreate()
    {
        int z = 0;

        for (int i = 0; i < LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel.Count; i++)
        {
            if (LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel[i] != 0)
            {
                MergeBulletCreate(i, true);
                z++;
            }

        }

        if (z != 0)
        {
            UIManager.instance.BtnControl(true);
        }
    }
    public void AddBullet()
    {
        for (int i = 0; i < mergePoints.Count; i++)
        {
            if (mergePoints[i].mergePointObj == null)
            {
                MergeBulletCreate(i, false);
                break;
            }

        }
    }
    void MergeBulletCreate(int _i, bool _check)
    {
        GameObject _go = Instantiate(mergeBulletPrefab, Vector3.zero, Quaternion.identity);
        _go.transform.parent = mergePoints[_i].transform;
        _go.transform.localPosition = new Vector3(0, 0.7f, 0);
        mergePoints[_i].mergePointObj = _go;
        mergeBullets.Add(_go.transform.GetComponent<MergeBullet>());

        if (_check)
        {
            _go.transform.GetComponent<MergeBullet>().bulletLevel = LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel[_i];
        }

    }
    public void ShotMergeBullet()
    {
        for (int i = 0; i < mergeBullets.Count; i++)
        {
            mergeBullets[i].transform.parent = null;
            mergeBullets[i].GetComponent<MergeBullet>().isMergeBulletStart = true;
        }
    }

    public void MergeBulletsCountControll(MergeBullet _go)
    {
        MergeBullet _mb = _go;
        mergeBullets.Remove(_go);
        Destroy(_mb.gameObject);

        if (mergeBullets.Count == 0)
        {
            CameraController.instance.cameraState = CameraStae.Idle;
            StartCoroutine(UIManager.instance.LosePanel(.25f));
        }
    }

}
