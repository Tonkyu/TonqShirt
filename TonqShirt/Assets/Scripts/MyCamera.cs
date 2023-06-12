using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour
{
    [SerializeField] private Image _img = default;
    [SerializeField] private WebCamTexture _webCamTexture;

    private void Start()
    {
        PlayCamera();
    }

    /// <summary>
    /// カメラを起動する
    /// </summary>
    private void PlayCamera()
    {
        try
        {
            StopCamera();
            var cameraDevice = WebCamTexture.devices[1];
            Debug.Log(WebCamTexture.devices.Length);
            var cameraImageSize = ((RectTransform) _img.transform).sizeDelta;
            _webCamTexture = new WebCamTexture(
                cameraDevice.name,
                (int) cameraImageSize.y,
                (int) cameraImageSize.x,
                30);
            _img.material.mainTexture = _webCamTexture;
            _img.preserveAspect = true;
            _webCamTexture.Play();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n" + e.StackTrace);
            throw;
        }
    }

    /// <summary>
    /// カメラを停止する
    /// </summary>
    private void StopCamera()
    {
        if (_webCamTexture == null)
            return;

        _webCamTexture.Stop();
        Destroy(_webCamTexture);
        _webCamTexture = null;
    }
}