using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectButtonManager : MonoBehaviour
{
    [SerializeField] private Tshirt _tshirt;
    public Color onColor;
    public Color offColor;

    public void UpdateModel(ModelSelectButton button)
    {
        _tshirt.SetButton(button);
    }
}
