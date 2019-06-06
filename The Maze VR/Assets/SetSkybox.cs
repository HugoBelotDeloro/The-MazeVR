using UnityEngine;

public class SetSkybox : MonoBehaviour
{
    [SerializeField] private Material skyboxMaterial;
    private Material _defaultMaterial;
    
    void Start()
    {
        _defaultMaterial = RenderSettings.skybox;
        RenderSettings.skybox = skyboxMaterial;
        
    }

    private void OnDestroy()
    {
        RenderSettings.skybox = _defaultMaterial;
    }
}
