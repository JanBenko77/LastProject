using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTarget : MonoBehaviour
{
    [SerializeField] private UnityEvent OnObjectiveStart;
    [SerializeField] private UnityEvent OnObjectiveComplete;
    public ObjectiveType requiredType;
    private bool objectiveActive;

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectiveActive && other.TryGetComponent(out ObjectiveItem item))
        {
            if (item.ObjectiveType == requiredType)
            {
                EventBus<OnObjectiveComplete>.Invoke(new OnObjectiveComplete(item));
                OnObjectiveComplete.Invoke();
            }
        }
        if (!objectiveActive && other.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            objectiveActive = true;
            EventBus<OnObjectiveActivated>.Invoke(new OnObjectiveActivated(requiredType));
            OnObjectiveStart.Invoke();
        }
    }

    public ObjectiveItem FindObjectiveItemOfSameType()
    {
        return FindObjectsOfType<ObjectiveItem>().FirstOrDefault(item => item.ObjectiveType == requiredType);
    }
}