using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private Tshirt _tshirt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleTransparency();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MoveModel(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            MoveModel(false);
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
