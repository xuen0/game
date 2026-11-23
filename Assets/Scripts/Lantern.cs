using UnityEditor;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public bool isLightOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetLight(isLightOn);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Pass in true to turn on light, pass in false to turn off light
    public void SetLight(bool on)
    {
        Material material = GetComponent<MeshRenderer>().material;
        if (on)
        {
            material.EnableKeyword("_EMISSION");
            isLightOn = true;
            GameObject.FindWithTag("Rabbit").GetComponent<RabbitMovement>().AddToMoveQueue(gameObject);
        }
        else
        {
            material.DisableKeyword("_EMISSION");
            isLightOn = false;
        }
    }
}

// Custom Editor for the Inspector for testing before player interaction implementation
[CustomEditor(typeof(Lantern))]
public class LanternEditor : Editor
{
    override public void OnInspectorGUI()
    {
        Lantern lantern = (Lantern)target;
        if (GUILayout.Button("On/Off"))
        {
            lantern.SetLight(!lantern.isLightOn);
        }
        DrawDefaultInspector();
    }
}