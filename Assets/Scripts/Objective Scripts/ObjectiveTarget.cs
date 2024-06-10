using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
    [SerializeField] private List<GameObject> attachmentsToDisable;
    [SerializeField] private List<GameObject> attachmentsToEnable;
    public ObjectiveType requiredType;
    private bool objectiveActive;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (GameObject obj in attachmentsToDisable)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in attachmentsToEnable)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectiveActive && other.TryGetComponent(out ObjectiveItem item))
        {
            if (item.ObjectiveType == requiredType)
            {
                EventBus<OnObjectiveComplete>.Invoke(new OnObjectiveComplete(item));
            }
        }
        if (!objectiveActive && other.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            objectiveActive = true;
            EventBus<OnObjectiveActivated>.Invoke(new OnObjectiveActivated(requiredType));
            foreach (GameObject obj in attachmentsToDisable)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in attachmentsToEnable)
            {
                obj.SetActive(true);
            }
        }
    }

    public ObjectiveItem FindObjectiveItemOfSameType()
    {
        return FindObjectsOfType<ObjectiveItem>().FirstOrDefault(item => item.ObjectiveType == requiredType);
    }
}