using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectButton : MonoBehaviour
{
    [SerializeField] private ModelSelectButtonManager _manager;
    [SerializeField] public int imgId;
    [SerializeField] public Vector3 neckCenter;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClick()
    {
        _manager.UpdateModel(this);
    }
}
