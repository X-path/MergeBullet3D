using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSO : MonoBehaviour
{
    public static ResetSO instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ResetAll()
    {

        for (int i = 0; i < LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel.Count; i++)
        {
            LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel[i] = 0;
        }

        LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoney = 4;
        LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnExtraMoney = 3;

        LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoneyLevel = 0;

    }
    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        Debug.Log("SO clear");
        ResetAll();
#endif
    }
}
