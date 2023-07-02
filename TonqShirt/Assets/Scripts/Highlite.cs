using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlite : MonoBehaviour
{
    [SerializeField] private GameObject _highliteShoulder;
    [SerializeField] private GameObject _highliteSleeve;
    [SerializeField] private GameObject _highliteGirth;

    public void SetCenter(Vector3 vec)
    {
        gameObject.transform.localPosition = vec;
    }

    public void TurnHighlite(Controller.ControlMode mode)
    {
        switch (mode)
        {
            case Controller.ControlMode.Sleeve:
                if (!_highliteSleeve.activeSelf) _highliteSleeve.SetActive(true);
                _highliteGirth.SetActive(false);
                _highliteShoulder.SetActive(false);
                break;

            case Controller.ControlMode.Girth:
                if (!_highliteGirth.activeSelf) _highliteGirth.SetActive(true);
                _highliteSleeve.SetActive(false);
                _highliteShoulder.SetActive(false);
                break;

            case Controller.ControlMode.Shoulder:
                if (!_highliteShoulder.activeSelf) _highliteShoulder.SetActive(true);
                _highliteGirth.SetActive(false);
                _highliteSleeve.SetActive(false);
                break;
            case Controller.ControlMode.None:
                _highliteGirth.SetActive(false);
                _highliteShoulder.SetActive(false);
                _highliteSleeve.SetActive(false);
                break;
            default:
                break;
        }
    }
}
