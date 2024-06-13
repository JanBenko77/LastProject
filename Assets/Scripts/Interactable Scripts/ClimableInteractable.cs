using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimableInteractable : XRSimpleInteractable
{
    private PhysicsHand lastHand;
    //private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        selectEntered.AddListener(OnGrabEnter);
        selectExited.AddListener(OnGrabExit);
        hoverEntered.AddListener(OnHoverEnter);
        hoverExited.AddListener(OnHoverExit);
        selectMode = InteractableSelectMode.Multiple;
        //rb = GetComponent<Rigidbody>();
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
        //if(lastHand != null)
        //lastHand.DestroyJoint();
        lastHand = interactor.physicsHand;
        //if(rb != null)
            //lastHand.CreateJoint(rb);
        //else
            lastHand.CreateJoint();
    }
    void OnGrabExit(SelectExitEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        PhysicsHand hand = interactor.physicsHand;
        hand.DestroyJoint();
        if(lastHand == hand)
            lastHand = null;
    }
}