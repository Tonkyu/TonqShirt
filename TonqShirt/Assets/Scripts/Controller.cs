using System;
using System.Collections;
using System.Collections.Generic;
using JoyconLib;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private Tshirt _tshirt;

    private static readonly Joycon.Button[] buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    joycons;
    private Joycon          joyconL;
    private Joycon          joyconR;
    private Joycon.Button?  pressedL;
    private Joycon.Button?  pressedR;
    private Joycon.Button?  prevPressedL;
    private Joycon.Button?  prevPressedR;


    void Start()
    {
        joycons = JoyconManager.Instance.j;

        if ( joycons == null || joycons.Count <= 0 ) return;

        joyconL = joycons.Find( c =>  c.isLeft );
        joyconR = joycons.Find( c => !c.isLeft );

        if (joyconL == null)
        {
            Debug.Log("Couldn't find JoyconL");
        }
        if (joyconR == null)
        {
            Debug.Log("Couldn't find JoyconR");
        }

    }
    void Update()
    {
        prevPressedL = pressedL;
        prevPressedR = pressedR;
        pressedL = null;
        pressedR = null;

        if ( joycons == null || joycons.Count <= 0 ) return;

        foreach ( var button in buttons )
        {
            if ( joyconL != null && joyconL.GetButton(button) )
            {
                pressedL = button;
            }
            if ( joyconR != null && joyconR.GetButton(button) )
            {
                pressedR = button;
            }
        }

        if (Input.GetKeyDown(KeyCode.X) || NewlyPressed(false, Joycon.Button.DPAD_UP))
        {
            ToggleTransparency();
        }

        if (Input.GetKeyDown(KeyCode.R) || NewlyPressed(false, Joycon.Button.SHOULDER_1))
        {
            MoveModel(true);
        }

        if (Input.GetKeyDown(KeyCode.L) || NewlyPressed(false, Joycon.Button.SHOULDER_2))
        {
            MoveModel(false);
        }
        if ( Input.GetKey ( KeyCode.Joystick2Button0 ) )
        {
            Debug.Log("A pressed");
        }
    }

    void ToggleTransparency()
    {
        _tshirt.ToggleMeshVisibility();
    }

    void MoveModel(bool direction)
    {
        _tshirt.OnMoveModel(direction);
    }

    private bool NewlyPressed(bool isL, Joycon.Button button)
    {
        if (isL)
        {
            return prevPressedL != button && pressedL == button;
        }
        else
        {
            return prevPressedR != button && pressedR == button;
        }
    }
}
