using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectButton : MonoBehaviour
{
    [SerializeField] private int _imgId;
    [SerializeField] private Model _model;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        _model.UpdateImage(_imgId);
    }
}
