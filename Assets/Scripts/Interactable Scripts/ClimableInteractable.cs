using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimableInteractable : XRSimpleInteractable
{
    private PhysicsHand lastHand;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        selectEntered.AddListener(OnGrabEnter);
        selectExited.AddListener(OnGrabExit);
        hoverEntered.AddListener(OnHoverEnter);
        hoverExited.AddListener(OnHoverExit);
        selectMode = InteractableSelectMode.Multiple;
        rb = GetComponent<Rigidbody>();
    }

    //Try check here to disable both hands for hookes law
    void OnHoverEnter(HoverEnterEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        EventBus<OnClimbHoverEnter>.Invoke(new OnClimbHoverEnter(interactor.physicsHand));
    }

    void OnHoverExit(HoverExitEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        EventBus<OnClimbHoverExit>.Invoke(new OnClimbHoverExit(interactor.physicsHand));
    }

    void OnGrabEnter(SelectEnterEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        EventBus<OnGrabEnter>.Invoke(new OnGrabEnter(interactor.physicsHand));
    }
    void OnGrabExit(SelectExitEventArgs args){
        PhysicsHandInteractor interactor = args.interactorObject.transform.gameObject.GetComponent<PhysicsHandInteractor>();
        EventBus<OnGrabExit>.Invoke(new OnGrabExit(interactor.physicsHand));
    }
}