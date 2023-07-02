using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectButtonManager : MonoBehaviour
{
    [SerializeField] public Tshirt tshirt;
    public Color onColor;
    public Color offColor;

    public void UpdateModel(ModelSelectButton button)
    {
        tshirt.SetButton(button);
    }
}
