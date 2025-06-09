using UnityEngine;

public class PlayerViewControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody;    // сам Player
    [SerializeField] private Transform cameraPivot;   // пустышка или сама камера

    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minPitch = -60f;
    [SerializeField] private float maxPitch = 60f;

    private float pitch = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // yaw body rotate
        playerBody.Rotate(Vector3.up * mX);

        // pitch camera tilt
        pitch = Mathf.Clamp(pitch - mY, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}