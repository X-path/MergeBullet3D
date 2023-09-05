using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelFeatures Data", menuName = "LevelFeatures Data")]
public class LevelFeaturesSO : ScriptableObject
{
    [Header("FallBtn Save")]
    [SerializeField] public List<int> pointsLevel = new List<int>();

    [Header("Btn Money")]
    [SerializeField] public float BuyBtnMoney = 0;
    [SerializeField] public float BuyBtnExtraMoney = 0;

    [Header("Btn Levels")]
    [SerializeField] public int BuyBtnMoneyLevel = 0;

}
