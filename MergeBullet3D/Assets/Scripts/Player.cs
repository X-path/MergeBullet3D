using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    Rigidbody rigidbody;
    const float DragSpeed = 0.0068f;
    const float DragIncrementSpeed = 850;
    float maxPosX = 2f;
    float minPosX = -2f;
    float _mouseX;
    float moveSpeed = 3f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement()
    {

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, moveSpeed);

    }
    public void Drag()
    {

#if UNITY_EDITOR
        _mouseX = Mathf.MoveTowards(_mouseX, Mathf.Clamp((_mouseX + Input.GetAxis("Mouse X") * DragSpeed * 12.5f), minPosX, maxPosX), DragIncrementSpeed * Time.fixedDeltaTime);
#else
            _mouseX = Mathf.MoveTowards(_mouseX,  Mathf.Clamp ((_mouseX + Input.touches[0].deltaPosition.x * DragSpeed),minPosX,maxPosX), DragIncrementSpeed * Time.fixedDeltaTime);
#endif

        transform.position = new Vector3(_mouseX, transform.position.y, transform.position.z);


    }
    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
    }
}
