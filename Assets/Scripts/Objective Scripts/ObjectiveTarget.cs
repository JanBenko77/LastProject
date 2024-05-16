using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
    bool objectiveActive;
    public ObjectiveType requiredType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.TryGetComponent(out ObjectiveItem item) && objectiveActive){
            if(item.ObjectiveType == requiredType){
                EventBus<OnObjectiveComplete>.Invoke(new OnObjectiveComplete(item));
            }
        }
        if( other.gameObject.layer == LayerMask.NameToLayer("PlayerBody") && !objectiveActive){
            objectiveActive = true;
            EventBus<OnObjectiveActivated>.Invoke(new OnObjectiveActivated(requiredType));
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
