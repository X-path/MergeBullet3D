using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;



public enum LevelState
{
    Stop,
    Playing,
    Win,
    Lose
}

public class UIManager : MonoBehaviour
{
    public LevelState levelState;

    public static UIManager instance = null;

    [SerializeField]
    private GameObject levelCompletePanel;

    [SerializeField]
    private GameObject levelFailedPanel;

    [SerializeField]
    TextMeshProUGUI currentLevelText;


    [SerializeField]
    TextMeshProUGUI gemText;

    float gemCount;


    [SerializeField] GameObject shotBtn, mergeMoneyBtn;
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



        if (PlayerPrefs.GetInt("level") == 0)
        {
            PlayerPrefs.SetFloat("Gem", 50);
        }
    }
    private void Start()
    {

        UpdateGemCount();
        UpdateLevelTexts();

    }
    void UpdateGemCount()
    {

        gemCount = PlayerPrefs.GetFloat("Gem");
        gemText.text = "$" + gemCount.ToString();

    }

    public void BtnControl(bool _value)
    {
        if (_value)
        {
            shotBtn.SetActive(true);
        }
        else
        {
            shotBtn.SetActive(false);
            mergeMoneyBtn.SetActive(false);
        }
    }

    void UpdateLevelTexts()
    {

        int levelNum = PlayerPrefs.GetInt("level");
        currentLevelText.text = "Level" + " " + levelNum.ToString();

    }

    public void OnTapToContinue()
    {
        int res = Random.Range(2, SceneManager.sceneCountInBuildSettings);

        if (PlayerPrefs.GetInt("level") > SceneManager.sceneCountInBuildSettings - 1)
        {

            if (res == SceneManager.GetActiveScene().buildIndex)
            {
                OnTapToContinue();
            }

            else
            {
                PlayerPrefs.SetInt("sceneNumber", res);
                SceneManager.LoadScene(PlayerPrefs.GetInt("sceneNumber"));

            }


        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        }
        PlayerPrefs.SetInt("FirstLogin", 2);
        PlayerPrefs.SetFloat("Gem", gemCount);
    }

    public void OnTapToRetry()
    {
        PlayerPrefs.SetFloat("Gem", gemCount);
        PlayerPrefs.SetInt("FirstLogin", 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public IEnumerator WinPanel(float waitTime)
    {

        levelState = LevelState.Win;

        int levelNum = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("level", levelNum + 1);

        yield return new WaitForSeconds(waitTime);
        levelCompletePanel.SetActive(true);

        yield return null;
    }

    public IEnumerator LosePanel(float waitTime)
    {

        levelState = LevelState.Lose;

        yield return new WaitForSeconds(waitTime);
        levelFailedPanel.SetActive(true);

        yield return null;
    }

    public void GemTextUpdate(bool _value, float _moneyValue)
    {
        if (_value)
        {
            gemCount += _moneyValue;

        }
        else
        {
            gemCount -= _moneyValue;

            if (gemCount < 0)
            {
                gemCount = 0;
            }

        }

        gemText.text = "$" + gemCount.ToString();
        PlayerPrefs.SetFloat("Gem", gemCount);


        UpgradeBtnControl.Instance.BtnControl();
    }

    public void ShotBtnAction()
    {

        for (int i = 0; i < MergeAreaManager.instance.mergePoints.Count; i++)
        {
            if (MergeAreaManager.instance.mergePoints[i].mergePointObj != null)
            {
                LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel[i] = MergeAreaManager.instance.mergePoints[i].mergePointObj.GetComponent<MergeBullet>().bulletLevel;
            }
            else
            {
                LevelFeaturesManager.instance.levelFeaturesSO.pointsLevel[i] = 0;
            }

        }

        BtnControl(false);

        MergeAreaManager.instance.ShotMergeBullet();

        CameraController.instance.cameraState = CameraStae.BulletFollow;
    }
}
