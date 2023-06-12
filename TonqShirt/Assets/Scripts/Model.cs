using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Model : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private Image _img;

    void Start()
    {
        _img = GetComponent<Image>();
        UpdateImage(0);
    }

    void Update()
    {
        
    }

    public void UpdateImage(int imgId)
    {
        _img.sprite = _sprites[imgId];
        _img.preserveAspect = true;
    }
}
