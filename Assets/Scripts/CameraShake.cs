using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Ccamera;
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float _noiseAmplitude = 1f;
    private CinemachineBasicMultiChannelPerlin _cameraNoise;
    private void Awake()
    {
        _cameraNoise = Ccamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cameraNoise.m_AmplitudeGain = 0f;
        //ShakeCamera();
        //StartCoroutine(ShakeCamera());
    }

    // [ContextMeuItem()]
    public void ShakeCamera(float shakeDuration, float noiseAmplitude)
    {
        float amplitude = 0;
        //increase amplitude
        DOTween.To(() => amplitude, x => amplitude = x, _noiseAmplitude, shakeDuration/2f)
        .OnUpdate(() =>
        {
             Debug.Log(amplitude);
            _cameraNoise.m_AmplitudeGain = amplitude;
        })
        //deacrease amplitude
        .OnComplete(() =>
        {
        DOTween.To(() => amplitude, x => amplitude = x, 0f, shakeDuration/2f)
        .OnUpdate(() =>
        {
            Debug.Log(amplitude);
            _cameraNoise.m_AmplitudeGain = amplitude;
        });
        });
    }
    // private IEnumerator ShakeCamera()
    // {
    //     _cameraNoise.m_AmplitudeGain = _noiseAmplitude;
    //     yield return new WaitForSeconds(shakeDuration);
    //     _cameraNoise.m_AmplitudeGain = 0f;
    // }
}
