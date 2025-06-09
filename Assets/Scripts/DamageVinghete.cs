using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class DamageVinghete : MonoBehaviour
{
    [SerializeField] private Volume vinghette;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float intensity = 1f;
    private Vignette _vignette;

    private void Awake()
    {
        vinghette.profile.TryGet(out _vignette);
        //DamageVinghette();
    }
    public void DamageVinghette()
    {
        float localIntensity = 0;
        //increase amplitude
        DOTween.To(() => localIntensity, x => localIntensity = x, intensity, duration / 2)
        .OnUpdate(() =>
        {
            Debug.Log(intensity);
            _vignette.intensity.value = localIntensity;
        })
        //deacrease amplitude
        .OnComplete(() =>
        {
            DOTween.To(() => localIntensity, x => localIntensity = x, 0f, duration / 2)
            .OnUpdate(() =>
            {
                Debug.Log(localIntensity);
                _vignette.intensity.value = localIntensity;
            }).SetEase(Ease.Linear);
        }).SetEase(Ease.InOutElastic);
    }

    // private IEnumerator FlashVignhette()
    // {
    //     float defaultIntensity = _vignette.intensity.value;
    //     _vignette.intensity.value = intensity;
    //     yield return new WaitForSeconds(duration);
    //     _vignette.intensity.value = defaultIntensity;
    // }
}
