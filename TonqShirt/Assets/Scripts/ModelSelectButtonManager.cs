using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectButtonManager : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private Tshirt _tshirt;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateModel(ModelSelectButton button)
    {
        _model.UpdateImage(button.imgId);
        _tshirt.neckCenter = button.neckCenter;
        _tshirt.DrawTshirt();
    }
}
