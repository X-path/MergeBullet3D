using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputState
{

    MouseUp,
    MouseDown,
    MouseHold,
    MouseIdle
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public InputState inputState;


    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(this);


        Application.targetFrameRate = 60;

        inputState = InputState.MouseIdle;



    }


    void Update()
    {
        GetInput();

        
    }

    void GetInput()
    {

        if (Input.GetMouseButtonDown(0))
        {

            inputState = InputState.MouseDown;
          

        }

        else if (Input.GetMouseButton(0))
        {

            inputState = InputState.MouseHold;
            

        }

        else if (Input.GetMouseButtonUp(0))
        {

            inputState = InputState.MouseUp;


        }
        else
        {

            inputState = InputState.MouseIdle;
        }

    }




}