using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class KillerPlaneHandler : MonoBehaviour
{
    [Header("Какие слои считаются игроками и киллерами")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask killerLayer;

    [Header("Что делать при «смерти»")]
    [SerializeField] private bool restartOnDeath = true;
    [SerializeField] private Transform respawnPoint;

    private void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isKiller  = ((1 << gameObject.layer) & killerLayer) != 0;
        bool isPlayer  = ((1 << other.gameObject.layer) & playerLayer) != 0;
        if (!isKiller || !isPlayer) return;

        if (restartOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (respawnPoint != null)
        {
            other.transform.position = respawnPoint.position;
            if (other.TryGetComponent<Rigidbody>(out var rb))
                rb.velocity = Vector3.zero;
            else if (other.TryGetComponent<CharacterController>(out var cc))
                ;
        }
    }
}
