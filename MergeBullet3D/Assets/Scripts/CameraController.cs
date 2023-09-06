using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum CameraState
{
    Idle,
    BulletFollow,
    PlayerFollow
}
public class CameraController : MonoBehaviour
{
    public CameraState cameraState;
    public static CameraController instance;
    Transform targetBullet;
    Transform targetPlayer;
    Transform target;
    public Transform camTransform;
    public Vector3 offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Transform shooterCamTransform;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }


    void FixedUpdate()
    {
        if (cameraState == CameraState.Idle)
            return;

        if (cameraState == CameraState.BulletFollow)
        {
            if (MergeAreaManager.instance.mergeBullets.Count == 0)
                return;

            UpdateTarget();
            target = targetBullet;
        }

        if (cameraState == CameraState.PlayerFollow)
        {
            target = targetPlayer;
        }

        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, targetPosition.z), ref velocity, SmoothTime);

    }

    void UpdateTarget()
    {
        Transform farthestObj = null;

        float maxZ = 0;

        foreach (MergeBullet _mergeBullet in MergeAreaManager.instance.mergeBullets.ToArray())
        {

            if (_mergeBullet != null && _mergeBullet.transform.position.z < maxZ)
            {
                maxZ = _mergeBullet.transform.position.y;
                farthestObj = _mergeBullet.transform;
            }

        }

        targetBullet = farthestObj;
    }

    public void ShotStartPos()
    {
        transform.DOMove(shooterCamTransform.position, .5f);
        transform.DORotate(shooterCamTransform.eulerAngles, .5f).OnComplete(() =>
        {
            //offset.x-=3f;
            offset.z=-5f;
            cameraState = CameraState.PlayerFollow;
            GameManager.instance.gameState = GameState.Shooter;
            targetPlayer = GameManager.instance.player.transform;
            
            
        });
    }
}
