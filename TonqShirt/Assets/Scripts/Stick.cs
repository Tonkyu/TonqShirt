using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] circleBlack;
    [SerializeField] private SpriteRenderer circleWhite;
    [SerializeField] private SpriteRenderer[] arrow;

    public void Activate()
    {
        Color white = circleWhite.color;
        white.a = 1.0f;
        for (int i = 0; i < arrow.Length; i++) arrow[i].color = white;
        Color black = circleBlack[0].color;
        black.a = 1.0f;
        for (int i = 0; i < circleBlack.Length; i++) circleBlack[i].color = black;
    }

    public void Deactivate()
    {
        Color white = circleWhite.color;
        white.a = 0.5f;
        for (int i = 0; i < arrow.Length; i++) arrow[i].color = white;
        Color black = circleBlack[0].color;
        black.a = 0.5f;
        for (int i = 0; i < circleBlack.Length; i++) circleBlack[i].color = black;
    }
}
