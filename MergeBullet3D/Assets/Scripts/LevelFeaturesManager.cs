using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFeaturesManager : MonoBehaviour
{
    public static LevelFeaturesManager instance = null;
    [SerializeField] public LevelFeaturesSO levelFeaturesSO;
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
}
