using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class InteractableScript : MonoBehaviour
{
    private List<Collider> interactorCollider = new List<Collider>();
    private List<GameObject> attachedHands = new List<GameObject>();
    public int detachLayer;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake(){
        detachLayer = LayerMask.NameToLayer("Default");
    }
    void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnInteractEnter);
        interactable.selectExited.AddListener(OnInteractExit);
        rb = GetComponent<Rigidbody>();

    }

    void ChangeLayer(string layer){
        gameObject.SetLayerRecursively(LayerMask.NameToLayer(layer));
    }
    void ChangeLayer(int layer){
        gameObject.SetLayerRecursively(layer);
    }
    void OnInteractEnter(SelectEnterEventArgs args){
        ChangeLayer("Interacting");
        interactorCollider.Add(args.interactorObject.transform.gameObject.GetComponent<Collider>());
        attachedHands.Add(args.interactorObject.transform.gameObject);
        rb.useGravity = true;
    }
    void OnInteractExit(SelectExitEventArgs args){
         attachedHands.Remove(args.interactorObject.transform.gameObject);
    }

    void OnTriggerExit(Collider other){
        if(interactorCollider.Contains(other) && attachedHands.Count <= 0){
            ChangeLayer(detachLayer);
            interactorCollider.Remove(other);
        }
    }
}
