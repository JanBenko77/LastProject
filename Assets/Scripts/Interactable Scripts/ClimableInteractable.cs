using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimableInteractable : XRSimpleInteractable
{
    private PhysicsHand lastHand;
    // Start is called before the first frame update
    void Start()
    {
        selectEntered.AddListener(OnGrabEnter);
        selectExited.AddListener(OnGrabExit);
        hoverEntered.AddListener(OnHoverEnter);
        hoverExited.AddListener(OnHoverExit);
        selectMode = InteractableSelectMode.Multiple;
    }

    //Try check here to disable both hands for hookes law
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
        if(lastHand != null)
            DisconectJoint(lastHand);
        lastHand = interactor.physicsHand;
        ConnectJoint(lastHand);
    }
    void OnGrabExit(SelectExitEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        PhysicsHand hand = interactor.physicsHand;
        DisconectJoint(hand);
        if(lastHand == hand)
            lastHand = null;
    }

    void DisconectJoint(PhysicsHand physicsHand){
        physicsHand.isGrabbing = false;
        physicsHand.interactor.SetHoverMaterial();
        Destroy(physicsHand.gameObject.GetComponent<FixedJoint>());
    }
    void ConnectJoint(PhysicsHand physicsHand){
        physicsHand.isGrabbing = true;
        physicsHand.interactor.SetGrabMaterial();
        FixedJoint fixedJoint = physicsHand.gameObject.AddComponent<FixedJoint>();
        fixedJoint.autoConfigureConnectedAnchor = false;
    }
}