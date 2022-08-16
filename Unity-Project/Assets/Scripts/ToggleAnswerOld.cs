using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnswer : MonoBehaviour
{
    public GameObject BackgroundCube;
    private Toggle Toggle;
    private Renderer BackgroundRenderer;
    private TextMeshPro TextRenderer;
    private static Color32 ActiveColor = new Color32(255, 186, 8, 255);
    private static Color32 ActiveBgColor = new Color32(0, 166, 166, 255);
    private static Color32 InactiveColor = new Color32(255, 186, 8, 255);
    private static Color32 InactiveBgColor = new Color32(36, 36, 30, 255);
    List<string> leftHand = new List<string>()
    {
        "EthanLeftHandIndex4", "EthanLeftHandMiddle4", "EthanLeftHandPinky4", "EthanLeftHandRing4"
    };
    List<string> rightHand = new List<string>()
    {
        "EthanRightHandIndex4", "EthanRightHandMiddle4", "EthanRightHandPinky4", "EthanRightHandRing4"
    };

    void Awake()
    {
        Toggle = GetComponent<Toggle>();
        BackgroundRenderer = BackgroundCube.GetComponent<Renderer>();
        TextRenderer = GetComponent<TextMeshPro>();
        BackgroundRenderer.material.color = InactiveBgColor;
        TextRenderer.faceColor = InactiveColor;
    }

    void OnTriggerEnter(Collider other)
    {
        print("ENTER");
        Toggle.isOn = !Toggle.isOn;
        if (leftHand.Contains(other.name) || rightHand.Contains(other.name))
        {
           // Toggle.isOn = !Toggle.isOn;
        }
    }

    public void OnChangeValue()
    {
        bool isActive = gameObject.GetComponent<Toggle>().isOn;
        BackgroundRenderer.material.color = isActive ? ActiveBgColor : InactiveBgColor;
        TextRenderer.faceColor = isActive ? ActiveColor : InactiveColor;
    }

}
