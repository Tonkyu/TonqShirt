using System;
using System.Collections;
using System.Collections.Generic;
using JoyconLib;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private Tshirt _tshirt;
    [SerializeField] private Worker worker;
    [SerializeField] private float _velocityShoulder;
    [SerializeField] private float _velocitySleeve;
    [SerializeField] private float _velocityGirth;
    [SerializeField] private Indicator _indicator;
    [SerializeField] private Highlite _highlite;
    [SerializeField] private JoyconButton _Abutton;
    [SerializeField] private JoyconButton _Bbutton;

    private static readonly Joycon.Button[] buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    public enum ControlMode
    {
        Shoulder,
        Sleeve,
        Girth,
        None,
    }

    public enum JoyconMode
    {
        Selected,
        Deselected,
    }

    public ControlMode controlMode;
    public JoyconMode joyconMode;

    private List<Joycon>    joycons;
    private Joycon          joyconL;
    private Joycon          joyconR;
    private ControlMode indicatingMode;


    void Start()
    {
        joycons = JoyconManager.Instance.j;
        controlMode = ControlMode.None;
        SetJoyconMode(JoyconMode.Deselected);

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
        if (joyconR != null) UpdateParameter();
    }

    private void UpdateParameter()
    {
        float stick_x = joyconR.GetStick()[0];
        float stick_y = joyconR.GetStick()[1];
        if (stick_x != 0 || stick_y != 0)
        {
            switch (controlMode)
            {
                case ControlMode.Sleeve:
                    ControlSleeve(stick_x);
                    break;

                case ControlMode.Girth:
                    ControlGirth(stick_x);
                    break;

                case ControlMode.Shoulder:
                    ControlShoulder(stick_x);
                    break;
                default:
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (joyconR == null) return;

        UpdateIndicatingMode();
        _highlite.TurnHighlite(indicatingMode);

        if (Input.GetKeyDown(KeyCode.R) || joyconR.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            MoveModel(true);
        }

        if (Input.GetKeyDown(KeyCode.L) || joyconR.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            MoveModel(false);
        }

        if (Input.GetKeyDown(KeyCode.X) || joyconR.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            ToggleTransparency();
        }

        if (joyconMode == JoyconMode.Selected && joyconR.GetButton(Joycon.Button.DPAD_DOWN)) // B-button
        {
            SetJoyconMode(JoyconMode.Deselected);
            UpdateControlMode();
            _indicator.TurnIndicator(controlMode);
        }

        if (joyconMode == JoyconMode.Deselected && joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT)) // A-button
        {
            SetJoyconMode(JoyconMode.Selected);
            UpdateControlMode();
            _indicator.TurnIndicator(controlMode);
        }
    }

    private void ControlGirth(float x)
    {
        if (x > 0)
        {
            _tshirt.IncreaseGirth(_velocityGirth);
        }
        else
        {
            _tshirt.IncreaseGirth(-_velocityGirth);
        }
    }

    private void ControlShoulder(float x)
    {
        if (x > 0)
        {
            _tshirt.IncreaseShoulder(_velocityShoulder);
        }
        else
        {
            _tshirt.IncreaseShoulder(-_velocityShoulder);
        }
    }

    private void ControlSleeve(float x)
    {
        if (x > 0)
        {
            _tshirt.IncreaseSleeve(_velocitySleeve);
        }
        else
        {
            _tshirt.IncreaseSleeve(-_velocitySleeve);
        }
    }

    private void UpdateControlMode()
    {
        if (joyconMode == JoyconMode.Selected)
        {
            controlMode = indicatingMode;
        }
        else if (joyconMode == JoyconMode.Deselected)
        {
            controlMode = ControlMode.None;
        }
        Debug.Log(controlMode);
    }

    void ToggleTransparency()
    {
        _tshirt.ToggleMeshVisibility();
    }

    void MoveModel(bool direction)
    {
        _tshirt.OnMoveModel(direction);
    }

    void SetJoyconMode(JoyconMode mode)
    {
        joyconMode = mode;
        if (mode == JoyconMode.Selected)
        {
            _Bbutton.Activate();
            _Abutton.Deactivate();
            _highlite.gameObject.SetActive(false);
        }
        else if (mode == JoyconMode.Deselected)
        {
            _Abutton.Activate();
            _Bbutton.Deactivate();
            _highlite.gameObject.SetActive(true);
        }
    }

    private void UpdateIndicatingMode()
    {
        if (joyconMode == JoyconMode.Selected) return;
        indicatingMode = worker.Infer(joyconR.GetVector());
    }
}
