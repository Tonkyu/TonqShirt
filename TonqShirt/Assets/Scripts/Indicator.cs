using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{

    [SerializeField] private GameObject _indicatorShoulder;
    [SerializeField] private GameObject _indicatorSleeve;
    [SerializeField] private GameObject _indicatorGirth;

    public void SetCenter(Vector3 vec)
    {
        gameObject.transform.localPosition = vec;
    }

    public void TurnIndicator(Controller.ControlMode mode)
    {
        _indicatorGirth.SetActive(false);
        _indicatorShoulder.SetActive(false);
        _indicatorSleeve.SetActive(false);

        switch (mode)
        {
            case Controller.ControlMode.Sleeve:
                _indicatorSleeve.SetActive(true);
                break;

            case Controller.ControlMode.Girth:
                _indicatorGirth.SetActive(true);
                break;

            case Controller.ControlMode.Shoulder:
                _indicatorShoulder.SetActive(true);
                break;

            default:
                break;
        }
    }
}
