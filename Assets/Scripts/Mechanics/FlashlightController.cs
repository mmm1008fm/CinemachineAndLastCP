using System;
using Unity.Mathematics;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    [SerializeField] private Light flashlight;
    [SerializeField] private ParticleSystem sparkEffect;
    [SerializeField] private int burstCount = 15;
    [SerializeField] private float burstDelay = 0.1f;

    private bool isOn = true;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            StartCoroutine(ToggleAndBurst());
    }

    private System.Collections.IEnumerator ToggleAndBurst()
    {
        isOn = !isOn;
        flashlight.enabled = isOn;

        if (sparkEffect != null)
        {
            yield return new WaitForSeconds(burstDelay);
            sparkEffect.Emit(burstCount);
        }
    }
}