using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class PhysicsHandInteractor : XRDirectInteractor
{
    public PhysicsHand physicsHand;
    [SerializeField] private RenderSettings renderSettings;
    void Start(){
        physicsHand.interactor = this;
    }
    public void SetGrabMaterial(){
        physicsHand.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = renderSettings.grabMaterial;
    }

    public void SetHoverMaterial(){
        if(!physicsHand.isGrabbing)
            physicsHand.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = renderSettings.hoverMaterial;
    }

    public void SetDefaultMaterial(){
        if(!physicsHand.isGrabbing)
            physicsHand.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = renderSettings.defaultMaterial;
    }
}


