using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum GameState
{
    Merge,
    Shooter,
    End
}
public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public static GameManager instance;
    [SerializeField] public GameObject myObj;
    Vector3 originalMyObjPos;
    bool isRaycastFailed = false;

    [SerializeField] public GameObject player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }
    void Start()
    {
        gameState = GameState.Merge;

    }

    void Update()
    {
        if (InputManager.instance.inputState == InputState.MouseDown)
        {
            if (gameState == GameState.Merge)
            {
                if (myObj == null)
                {
                    RaycastControl(true);
                }
            }

        }
        if (InputManager.instance.inputState == InputState.MouseHold)
        {
            if (gameState == GameState.Merge)
            {
                if (myObj != null)
                {

                    Vector3 mousePositionOffset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 30, 9.25f));
                    myObj.transform.position = mousePositionOffset;
                }
            }

            if (gameState == GameState.Shooter)
            {
                Player.instance.Movement();
                Player.instance.Drag();
                GunsManager.instance.GunsFire();
            }
        }
        if (InputManager.instance.inputState == InputState.MouseUp)
        {
            if (gameState == GameState.Merge)
            {
                if (myObj != null)
                {
                    RaycastControl(false);

                }
            }

            if (gameState == GameState.Shooter)
            {
                Player.instance.Stop();
            }

        }

    }

    void RaycastControl(bool _isDown)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, 50f, 1 << 6))
        {
            if (_isDown)
            {
                if (hit.transform.GetComponent<MergePoint>().mergePointObj == null)
                    return;


                myObj = hit.transform.GetComponent<MergePoint>().mergePointObj;

                originalMyObjPos = new Vector3(myObj.transform.localPosition.x, 0.7f, myObj.transform.localPosition.z);
            }
            else
            {
                if (hit.transform.GetComponent<MergePoint>().mergePointObj != null)
                {
                    if (hit.transform.GetComponent<MergePoint>().mergePointObj.GetComponent<MergeBullet>().bulletLevel != myObj.GetComponent<MergeBullet>().bulletLevel)
                    {
                        myObj.transform.DOLocalMove(originalMyObjPos, .25f).OnComplete(() =>
                        {
                            myObj = null;
                        });
                        

                    }
                    else
                    {
                        if (hit.transform.GetComponent<MergePoint>().mergePointObj != myObj)
                        {
                            hit.transform.GetComponent<MergePoint>().mergePointObj.GetComponent<MergeBullet>().bulletLevel += 1;
                            hit.transform.GetComponent<MergePoint>().mergePointObj.GetComponent<MergeBullet>().LevelTextUpdate();
                            myObj.transform.parent.GetComponent<MergePoint>().mergePointObj = null;
                            if (MergeAreaManager.instance.mergeBullets.Contains(myObj.GetComponent<MergeBullet>()))
                            {
                                MergeAreaManager.instance.mergeBullets.Remove(myObj.GetComponent<MergeBullet>());
                            }


                            Destroy(myObj);

                            myObj = null;
                        }
                        else
                        {
                            myObj.transform.DOLocalMove(originalMyObjPos, .25f).OnComplete(() =>
                            {
                                myObj = null;
                            });
                        }

                    }

                }
                else if (hit.transform.GetComponent<MergePoint>().mergePointObj == null)
                {
                    myObj.transform.parent.GetComponent<MergePoint>().mergePointObj = null;
                    myObj.transform.parent = hit.transform;
                    hit.transform.GetComponent<MergePoint>().mergePointObj = myObj;
                    myObj.transform.localPosition = new Vector3(0, 0.7f, 0);
                    myObj = null;
                }
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red);

            return;

        }
        else if (myObj != null && isRaycastFailed == false)
        {
            isRaycastFailed = true;
            myObj.transform.DOLocalMove(originalMyObjPos, .25f).OnComplete(() =>
             {
                 isRaycastFailed = false;
                 myObj = null;
             });


        }

    }


}


