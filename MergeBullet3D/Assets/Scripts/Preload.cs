using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Facebook.Unity;
//using GameAnalyticsSDK;

/*#if UNITY_IOS
// Include the IosSupport namespace if running on iOS:
using Unity.Advertisement.IosSupport;
#endif*/

public class Preload : MonoBehaviour
{
    public float waitTime;
    public string levelNumber_Haskey;
    public int minimumRandomLevelNumber;



    private void Start()
    {
        Invoke("WaitStartLoadGame", waitTime);

    }

    private void WaitStartLoadGame()
    {
        if (!PlayerPrefs.HasKey(levelNumber_Haskey))
        {
            PlayerPrefs.SetInt(levelNumber_Haskey, 1);
            SceneManager.LoadScene(1);
        }
        else
        {
            int level = PlayerPrefs.GetInt(levelNumber_Haskey);
            int levelCountInProject = SceneManager.sceneCountInBuildSettings;
            if (level >= levelCountInProject)
            {
                int randomLevel = Random.Range(minimumRandomLevelNumber, levelCountInProject);
                SceneManager.LoadScene(randomLevel);
            }
            else
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt(levelNumber_Haskey));
            }
        }

        PlayerPrefs.SetInt("FirstLogin", 1);
    }
   
}
