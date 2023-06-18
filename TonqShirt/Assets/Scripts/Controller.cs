using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private Tshirt _tshirt;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _tshirt.ToggleMeshVisibility();
        }
    }
}
