using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class InteractableScript : MonoBehaviour
{
    private Collider interactor;
    private bool isUsed;
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
        interactor = args.interactorObject.transform.gameObject.GetComponent<Collider>();
        isUsed = true;
    }
    void OnInteractExit(SelectExitEventArgs args){
        isUsed = false;
    }

    void OnTriggerExit(Collider other){
        if(interactor == other && !isUsed)
        ChangeLayer("Default");
    }
}
