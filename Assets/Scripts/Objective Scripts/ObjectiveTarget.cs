using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
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
        if(other.TryGetComponent(out ObjectiveItem item)){
            if(item.ObjectiveType == requiredType){

            }
        }
    }
}
