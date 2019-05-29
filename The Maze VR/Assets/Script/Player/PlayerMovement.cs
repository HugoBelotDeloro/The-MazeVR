using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera PlayerView;
    private const float MovementAmount = 0.3f;
    private const float RotationAmount = 2f;
    private const float HeadPanSpeed = 1.3f;
    private const float MinHeadTilt = 0.4f;
    private const float MaxHeadTilt = -0.5f;
    private Rigidbody _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float forwardMovement = Input.GetAxis("Forward") * MovementAmount;
        float rotation = Input.GetAxis("Side") * RotationAmount;
        Transform transform1 = transform;
        transform1.position += MovementAmount * forwardMovement * transform1.forward;
        transform.Rotate(rotation * RotationAmount * Vector3.up, Space.World);
        Quaternion r = PlayerView.transform.localRotation;
        float clamped = Mathf.Clamp(r.x + HeadPanSpeed * Input.GetAxis("Head"), MaxHeadTilt, MinHeadTilt);
        r.Set(clamped, r.y, r.z, r.w);
        PlayerView.transform.localRotation = r;
        _playerRigidbody.angularVelocity = Vector3.zero;
    }
}