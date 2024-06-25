using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    [SerializeField] private ObjectiveType _objectiveType;
    public ObjectiveType ObjectiveType{ get{ return _objectiveType; } set { _objectiveType = value; } }
    // Start is called before the first frame update
    void Start(){
        EventBus<OnObjectiveComplete>.OnEvent += OnItemDelivered;
        EventBus<OnObjectiveActivated>.OnEvent += OnItemActivated;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }


    // Update is called once per frame
    void OnItemDelivered(OnObjectiveComplete pEvent){
        if(pEvent.item == this){
            Debug.Log("Item Delivered");
            Destroy(gameObject);
        }
    }
    void OnItemActivated(OnObjectiveActivated pEvent){
       if(pEvent.type == _objectiveType){
        gameObject.SetLayerRecursively(LayerMask.NameToLayer("Objective"));
        if(TryGetComponent(out InteractableScript interactabe)){
            interactabe.detachLayer = LayerMask.NameToLayer("Objective");
        }
       }
    }
}
