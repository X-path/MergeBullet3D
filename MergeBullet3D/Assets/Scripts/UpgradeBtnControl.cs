using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtnControl : MonoBehaviour
{
    public static UpgradeBtnControl Instance;
    [Header("Btn")]
    [SerializeField] Button UpgradeBuyBtn;
    bool isUpgradeBuyBtnActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        BtnControl();
    }

    public void BtnControl()
    {
        float _money = PlayerPrefs.GetFloat("Gem");

        BtnLevelCompare();

        if (_money >= LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoney)
        {
            UpgradeBuyBtn.interactable = true;
         

        }
        else
        {
            UpgradeBuyBtn.interactable = false;
          
        }
      
    }
    void BtnLevelCompare()
    {
        List<int> btnsLevel = new List<int>();
        btnsLevel.Add(LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoneyLevel);
        btnsLevel.Sort();

        if (btnsLevel[0] == LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoneyLevel)
        {
            isUpgradeBuyBtnActive = true;
           
        }
        else
        {
            isUpgradeBuyBtnActive = false;
        }

    }
}
