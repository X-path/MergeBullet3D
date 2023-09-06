using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelEndCube : MonoBehaviour
{
    [SerializeField] TextMeshPro obstacleCountText;
    [SerializeField] int obstacleCount;
    void Start()
    {
        TextUpdate();
    }
    void TextUpdate()
    {
        obstacleCountText.text = obstacleCount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GameManager.instance.LevelEndWin();
        }
        if (other.gameObject.layer == 9)
        {
            obstacleCount -= other.GetComponent<Bullet>().bulletLevel;
            other.GetComponent<Bullet>().BulletDestroy();
            TextUpdate();
            if (obstacleCount <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }
}
