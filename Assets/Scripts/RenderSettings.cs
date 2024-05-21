using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

[CreateAssetMenu(fileName = "RenderSettings", menuName = "ScriptableObjects/RenderSettings")]
public class RenderSettings : ScriptableObject
{
    public Material hoverMaterial;
    public Material grabMaterial;
    public Material defaultMaterial;
}
