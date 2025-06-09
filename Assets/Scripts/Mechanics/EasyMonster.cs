using Cinemachine;
using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class SimpleScreamer : MonoBehaviour
{
    [SerializeField] private GameObject screamerObject;
    [SerializeField] private AudioClip screamSound;
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private LayerMask triggerLayer;
    [SerializeField] private bool oneShot = true;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private CinemachineVirtualCamera Ccamera;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float _noiseAmplitude = 10f;

    private AudioSource audioSrc;
    private bool hasTriggered = false;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && oneShot) return;

        if (((1 << other.gameObject.layer) & triggerLayer.value) != 0)
            StartCoroutine(DoScream());
    }

    private IEnumerator DoScream()
    {
        hasTriggered = true;

        if (Ccamera)
        {
            cameraShake.ShakeCamera(shakeDuration, _noiseAmplitude);
        }

        if (screamerObject != null)
            screamerObject.SetActive(true);

        if (screamSound != null)
            audioSrc.PlayOneShot(screamSound);

        yield return new WaitForSeconds(displayTime);

        if (screamerObject != null)
            screamerObject.SetActive(false);
    }
}
