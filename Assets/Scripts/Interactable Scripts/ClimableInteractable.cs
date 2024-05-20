using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimableInteractable : XRSimpleInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        selectEntered.AddListener(OnGrabEnter);
        selectExited.AddListener(OnGrabExit);
        hoverEntered.AddListener(OnHoverEnter);
        hoverExited.AddListener(OnHoverExit);
        selectMode = InteractableSelectMode.Multiple;
    }


    void OnHoverEnter(HoverEnterEventArgs args){
        Debug.Log("hovering");
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        interactor.SetHoverMaterial();
    }
    void OnHoverExit(HoverExitEventArgs args){
        Debug.Log("Hover exited");
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        interactor.SetDefaultMaterial();
    }

    void OnGrabEnter(SelectEnterEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        interactor.SetGrabMaterial();
        PhysicsHand hand = interactor.physicsHand.gameObject.GetComponent<PhysicsHand>();
        hand.isGrabbing = true;
        FixedJoint fixedJoint = interactor.physicsHand.gameObject.AddComponent<FixedJoint>();
        fixedJoint.autoConfigureConnectedAnchor = false;
        Debug.Log("Interacting");
    }
    void OnGrabExit(SelectExitEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        interactor.SetDefaultMaterial();
        PhysicsHand hand = interactor.physicsHand.gameObject.GetComponent<PhysicsHand>();
        hand.isGrabbing = false;
        Destroy(interactor.physicsHand.gameObject.GetComponent<FixedJoint>());
        Debug.Log("Not Interacting");

    }

}
