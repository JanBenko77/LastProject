using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    [SerializeField] private ObjectiveType _objectiveType;
    private GameObject player;
    public ObjectiveType ObjectiveType{ get{ return _objectiveType; } set { _objectiveType = value; } }
    // Start is called before the first frame update
    void Start(){
        EventBus<OnObjectiveComplete>.OnEvent += OnItemDelivered;
        EventBus<OnObjectiveActivated>.OnEvent += OnItemActivated;
        gameObject.layer = LayerMask.NameToLayer("Default");
        player = FindObjectOfType<XROrigin>().gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, player.transform.position - transform.position,out RaycastHit hit,5);
        if(hit.rigidbody.gameObject == player){
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Interacting"));
        }
        else{
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Objective"));
        }
    }

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
