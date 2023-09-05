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
          /*  if (isUpgradeBuyBtnActive == true)
            {
                UpgradeBuyBtn.GetComponent<Animator>().SetInteger("Start", 1);
            }
            else
            {
                UpgradeBuyBtn.GetComponent<Animator>().SetInteger("Start", 0);
            }
        */

        }
        else
        {
            UpgradeBuyBtn.interactable = false;
            // isSpeedBtnActive = false;
          //  UpgradeBuyBtn.GetComponent<Animator>().SetInteger("Start", 0);
        }
       /* if (_money >= LevelFeaturesManager.instance.levelFeaturesSO.IncomeBtnMoney)
        {
            UpgradeIncomeBtn.interactable = true;
            if (isUpgradeIncomeBtnActive == true)
            {
                UpgradeIncomeBtn.GetComponent<Animator>().SetInteger("Start", 1);
            }
            else
            {
                UpgradeIncomeBtn.GetComponent<Animator>().SetInteger("Start", 0);
            }

        }
        else
        {
            UpgradeIncomeBtn.interactable = false;
            //isAddWorkerBtnActive = false;
            UpgradeIncomeBtn.GetComponent<Animator>().SetInteger("Start", 0);
        }*/
        /*   if (_money >= LevelFeaturesManager.instance.levelFeaturesSO.addWorkerBtnMoney)
           {
               addWorkerBtn.interactable = true;
               if (isAddWorkerBtnActive == true)
               {
                   addWorkerBtn.GetComponent<Animator>().SetInteger("Start", 1);
               }
               else
               {
                   addWorkerBtn.GetComponent<Animator>().SetInteger("Start", 0);
               }

           }
           else
           {
               addWorkerBtn.interactable = false;
               //isAddWorkerBtnActive = false;
               addWorkerBtn.GetComponent<Animator>().SetInteger("Start", 0);
           }
           if (_money >= LevelFeaturesManager.instance.levelFeaturesSO.incomeBtnMoney)
           {
               incomeBtn.interactable = true;
               if (isIncomeBtnActive == true)
               {
                   incomeBtn.GetComponent<Animator>().SetInteger("Start", 1);
               }
               else
               {
                   incomeBtn.GetComponent<Animator>().SetInteger("Start", 0);
               }

           }
           else
           {
               incomeBtn.interactable = false;
               incomeBtn.GetComponent<Animator>().SetInteger("Start", 0);
               // isIncomeBtnActive = false;
           }
           */
    }
    void BtnLevelCompare()
    {
        List<int> btnsLevel = new List<int>();
        btnsLevel.Add(LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoneyLevel);
        // btnsLevel.Add(LevelFeaturesManager.instance.levelFeaturesSO.addWorkerBtnLevel);
        // btnsLevel.Add(LevelFeaturesManager.instance.levelFeaturesSO.incomeBtnLevel);
        btnsLevel.Sort();

        if (btnsLevel[0] == LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoneyLevel)
        {
            isUpgradeBuyBtnActive = true;
            // return;
        }
        else
        {
            isUpgradeBuyBtnActive = false;
        }
      /*  if (btnsLevel[0] == LevelFeaturesManager.instance.levelFeaturesSO.UpgradeIncomeBtnLevel)
        {
            isUpgradeIncomeBtnActive = true;
            // return;
        }
        else
        {
            isUpgradeIncomeBtnActive = false;
        }*/
        /*    if (btnsLevel[0] == LevelFeaturesManager.instance.levelFeaturesSO.addWorkerBtnLevel)
            {
                isAddWorkerBtnActive = true;
                // return;
            }
            else
            {
                isAddWorkerBtnActive = false;
            }
            if (btnsLevel[0] == LevelFeaturesManager.instance.levelFeaturesSO.incomeBtnLevel)
            {
                isIncomeBtnActive = true;
                //return;
            }
            else
            {
                isIncomeBtnActive = false;
            }

            */

    }
}
