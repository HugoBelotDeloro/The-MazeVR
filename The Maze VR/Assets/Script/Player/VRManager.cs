using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SwitchVRDevice("Cardboard", true));
    }

    private IEnumerator SwitchVRDevice(string device, bool enable)
    {
        XRSettings.LoadDeviceByName(device);
        yield return null;
        XRSettings.enabled = enable;
    }

    void OnDestroy()
    {
        StartCoroutine(SwitchVRDevice("None", false));
    }
}
