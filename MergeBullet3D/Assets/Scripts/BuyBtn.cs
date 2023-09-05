using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtn : MonoBehaviour
{
    [HideInInspector] public bool isBtnActive = true;
    [SerializeField] TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText.text = "$" + LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoney.ToString();
    }

    public void BuyBtnAction()
    {
        if (!isBtnActive)
            return;

        isBtnActive = false;
        LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoneyLevel++;

        UIManager.instance.GemTextUpdate(false, LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoney);

        MergeAreaManager.instance.AddBullet();

        LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoney += LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnExtraMoney;
        moneyText.text = "$" + LevelFeaturesManager.instance.levelFeaturesSO.BuyBtnMoney.ToString();
      
        UpgradeBtnControl.Instance.BtnControl();
        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(.25f);
            isBtnActive = true;
        }

        UIManager.instance.BtnControl(true);
    }
}
