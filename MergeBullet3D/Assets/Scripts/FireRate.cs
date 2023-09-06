using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireRate : MonoBehaviour
{
    [SerializeField] TextMeshPro fireRateCountText;
    [SerializeField] int fireRateCount;
    void Start()
    {
        TextUpdate();
        ColorChange();
    }
    void TextUpdate()
    {
        fireRateCountText.text = "FireRate" + "\n" + fireRateCount.ToString();
    }
    void ColorChange()
    {
        if (fireRateCount > 0)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GunsManager.instance.GunsFireRateChange((float)fireRateCount);
        }
        if (other.gameObject.layer == 9)
        {
            fireRateCount+=other.GetComponent<Bullet>().bulletLevel;
            TextUpdate();
            ColorChange();
        }
    }
}
