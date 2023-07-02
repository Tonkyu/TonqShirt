using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelSelectButton : MonoBehaviour
{
    public int imgId;
    public Vector3 neckCenter;
    public Button button;
    [SerializeField] private ModelSelectButtonManager _manager;
    public ModelSelectButton leftButton;
    public ModelSelectButton rightButton;

    public float Girth;
    public float Sleeve;
    public float Shoulder;

    void Start()
    {
        Girth = _manager.tshirt.minGirth;
        Sleeve = _manager.tshirt.minSleeveLength;
        Shoulder = _manager.tshirt.minShoulderWidth;
    }
    public void OnClick()
    {
        _manager.UpdateModel(this);
    }

    public void SetColor(bool state)
    {
        button.GetComponent<Image>().color = state ? _manager.onColor : _manager.offColor;
    }
}
