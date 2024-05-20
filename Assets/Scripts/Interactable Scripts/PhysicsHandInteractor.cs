using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class PhysicsHandInteractor : XRDirectInteractor
{
    public Transform physicsHand;
    [SerializeField] private RenderSettings renderSettings;
    
    public void SetGrabMaterial(){
        physicsHand.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = renderSettings.grabMaterial;
    }

    public void SetHoverMaterial(){
        physicsHand.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = renderSettings.hoverMaterial;
    }

    public void SetDefaultMaterial(){
        physicsHand.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = renderSettings.defaultMaterial;
    }
}


