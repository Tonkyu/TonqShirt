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
        if (Input.GetKeyDown(KeyCode.X) || joyconR.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            ToggleTransparency();
        }

        if (Input.GetKeyDown(KeyCode.R) || joyconR.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            MoveModel(true);
        }

        if (Input.GetKeyDown(KeyCode.L) || joyconR.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            MoveModel(false);
        }

        if (joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            Debug.Log(joyconR.GetGyro());
            Debug.Log(joyconR.GetAccel());
            Debug.Log(joyconR.GetVector());
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
}
