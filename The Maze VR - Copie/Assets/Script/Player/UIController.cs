using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int _selected;
    [SerializeField] private Button resume;
    [SerializeField] private Slider volume;
    [SerializeField] private Button exit;
    private int _timer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image volumeBackground;
    [SerializeField] private Image volumeFillArea;
    [SerializeField] private Image volumeHandle;
    private readonly Color _colorActive = new Color(127, 127, 64, 255);
    private readonly Color _colorInactive = new Color(255, 255, 255, 255);
    [SerializeField]private AudioMixer mixer;

    private void OnEnable()
    {
        _timer = 0;
        Deselect();
        _selected = 0;
        EventSystem.current.SetSelectedGameObject(resume.gameObject);
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer--;
        }
        else if (Math.Abs(Input.GetAxis("Forward")) > 0.1)
        {
            _timer = 20;
            Deselect();
            if (Input.GetAxis("Forward") < 0)
            {
                _selected += 1;
            }
            else
            {
                _selected -= 1;
            }
            _selected %= 3;
            _selected = _selected < 0 ? _selected + 3 : _selected;
            switch (_selected)
            {
                case 0:
                    EventSystem.current.SetSelectedGameObject(resume.gameObject);
                    break;
                case 1:
                    EventSystem.current.SetSelectedGameObject(volume.gameObject);
                    volumeBackground.color = _colorActive;
                    volumeHandle.color = _colorActive;
                    volumeFillArea.color = _colorActive;
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(exit.gameObject);
                    break;
            }
        }
        else if (_selected == 1 && Math.Abs(Input.GetAxis("Side")) > 0.1)
        {
            _timer = 3;
            volume.value += Input.GetAxis("Side") * 0.1f;
            mixer.SetFloat("MasterVolume", Mathf.Log10(volume.value) * 20);
        }
        else if (Input.GetAxis("Action") > 0)
        {
            if (_selected == 0)
            {
                playerMovement.Resume();
            }
            else
            {
                GameManager.Instance.LoadMainMenu();
            }
        }
    }

    private void Deselect()
    {
        switch (_selected)
        {
            case 0:
                resume.OnDeselect(null);
                break;
            case 1:
                volume.OnDeselect(null);
                volumeBackground.color = _colorInactive;
                volumeHandle.color = _colorInactive;
                volumeFillArea.color = _colorInactive;
                break;
            case 2:
                exit.OnDeselect(null);
                break;
        }
    }
}
