using UnityEngine;

public class Lantern : MonoBehaviour
{
    public bool isLightOn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetLight(isLightOn);
    }


    // Pass in true to turn on light, pass in false to turn off light
    void SetLight(bool on)
    {
        Material material = GetComponent<MeshRenderer>().material;
        if (on)
        {
            material.EnableKeyword("_EMISSION");
            isLightOn = true;
        }
        else
        {
            material.DisableKeyword("_EMISSION");
            isLightOn = false;
        }
    }
}
