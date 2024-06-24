using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KamissliScript : MonoBehaviour
{
    [SerializeField] private BoxCollider bc;

    [SerializeField] private Animator anime;

    [SerializeField] private UnityEvent OnTrigger;


    private void OnEnable()
    {
        EventBus<OnAnimationComplete>.OnEvent += Func;
    }

    private void OnDisable()
    {
        EventBus<OnAnimationComplete>.OnEvent -= Func;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bc != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnTrigger.Invoke();
            }
        }
    }

    private void Func(OnAnimationComplete pEvent)
    {
        Debug.Log("Started"); 
        anime.SetTrigger("TrGrow");
    }
}
