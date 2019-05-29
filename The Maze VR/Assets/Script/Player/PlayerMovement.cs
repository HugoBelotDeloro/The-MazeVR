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
    private bool _paused;
    private int _timer;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_timer > 0)
        {
            _timer--;
        }
        if (Input.GetAxis("Echap") > 0.1 && _timer == 0)
        {
            _paused = !_paused;
            _timer = 20;
            pauseMenu.SetActive(_paused);
        }
        if (!_paused) 
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

    public void Resume()
    {
        _paused = false;
        pauseMenu.SetActive(false);
    }
}