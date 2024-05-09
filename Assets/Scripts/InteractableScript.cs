using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class InteractableScript : MonoBehaviour
{
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

    void OnInteractEnter(SelectEnterEventArgs args){
        ChangeLayer("Interacting");
    }

    void OnInteractExit(SelectExitEventArgs args){
        Invoke("SetDefaultLayer",0.2f);
    }

    void ChangeLayer(string layer){
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    void SetDefaultLayer(){
        ChangeLayer("Default");
    }
}
