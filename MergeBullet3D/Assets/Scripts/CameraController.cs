using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraStae
{
    Idle,
    BulletFollow,
    PlayerFollow
}
public class CameraController : MonoBehaviour
{
    public CameraStae cameraState;
    public static CameraController instance;
    Transform targetBullet;
    Transform targetPlayer;
    Transform target;
    public Transform camTransform;
    public Vector3 offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }


    void FixedUpdate()
    {
        if (cameraState == CameraStae.Idle)
            return;

        if (cameraState == CameraStae.BulletFollow)
        {
            if (MergeAreaManager.instance.mergeBullets.Count == 0)
                return;

            UpdateTarget();
            target = targetBullet;
        }

        if (cameraState == CameraStae.PlayerFollow)
        {
            target = targetPlayer;
        }

        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, transform.position.y, targetPosition.z), ref velocity, SmoothTime);






        /*     if (!isFollow)
                 return;

             if (MergeAreaManager.instance.mergeBullets.Count == 0)
                 return;

             UpdateTarget();

             if (transform.position.z > camDeepPosY && currTarget != null)
             {
                 Vector3 desiredPosition = currTarget.position + offset;
                 Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                 smoothedPosition.x = 0;

                 // if (smoothedPosition.y > lastTarget.position.y)
                 //     return;


                 transform.position = smoothedPosition;
             }
             */

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
}
