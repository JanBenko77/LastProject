using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class InteractableScript : MonoBehaviour
{
    private List<Collider> interactorCollider = new List<Collider>();
    private List<GameObject> attachedHands = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnInteractEnter);
        interactable.selectExited.AddListener(OnInteractExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeLayer(string layer){
        gameObject.SetLayerRecursively(LayerMask.NameToLayer(layer));
    }

    void OnInteractEnter(SelectEnterEventArgs args){
        ChangeLayer("Interacting");
        interactorCollider.Add(args.interactorObject.transform.gameObject.GetComponent<Collider>());
        attachedHands.Add(args.interactorObject.transform.gameObject);
    }
    void OnInteractExit(SelectExitEventArgs args){
         attachedHands.Remove(args.interactorObject.transform.gameObject);
    }

    void OnTriggerExit(Collider other){
        if(interactorCollider.Contains(other) && attachedHands.Count <= 0){
            ChangeLayer("Default");
            interactorCollider.Remove(other);
        }
    }
}
