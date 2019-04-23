using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerView;
    private const float MovementAmount = 0.08f;
    private const float RotationAmount = 5f;
    private const float HeadPanSpeed = 0.01f;
    private const float MinHeadTilt = 0.4f;
    private const float MaxHeadTilt = -0.5f;
    private Rigidbody _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        int forwardMovement = Convert.ToInt32(Input.GetKey("z")) - Convert.ToInt32(Input.GetKey("s"));
        int rotation = Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("q"));
        transform.position += transform.forward * MovementAmount * forwardMovement;
        transform.Rotate(Vector3.up * rotation * RotationAmount, Space.World);
        Quaternion r = playerView.transform.localRotation;
        float clamped = Mathf.Clamp(r.x + HeadPanSpeed * (Convert.ToInt32(Input.GetKey("w")) - Convert.ToInt32(Input.GetKey("a"))), MaxHeadTilt, MinHeadTilt);
        r.Set(clamped, r.y, r.z, r.w);
        playerView.transform.localRotation = r;
        _playerRigidbody.angularVelocity = Vector3.zero;
    }
}