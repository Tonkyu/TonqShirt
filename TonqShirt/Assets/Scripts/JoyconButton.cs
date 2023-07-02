using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoyconButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private SpriteRenderer renderer;

    public void Activate()
    {
        Color tmp = renderer.color;
        tmp.a = 1.0f;
        renderer.color = tmp;

        tmp = text.color;
        tmp.a = 1.0f;
        text.color = tmp;
    }

    public void Deactivate()
    {
        Color tmp = renderer.color;
        tmp.a = 0.5f;
        renderer.color = tmp;

        tmp = text.color;
        tmp.a = 0.5f;
        text.color = tmp;
    }
}
