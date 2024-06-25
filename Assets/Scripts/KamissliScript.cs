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

    [SerializeField] private Transform player;

    private void OnEnable()
    {
        EventBus<OnAnimationComplete>.OnEvent += Func;
        EventBus<OnPlayerThrow>.OnEvent += UnParentPlayerAndMakeItFly;
    }

    private void OnDisable()
    {
        EventBus<OnAnimationComplete>.OnEvent -= Func;
        EventBus<OnPlayerThrow>.OnEvent -= UnParentPlayerAndMakeItFly;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yep");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            OnTrigger.Invoke();
        }
    }

    private void Func(OnAnimationComplete pEvent)
    {
        Debug.Log("Started"); 
        anime.SetTrigger("TrGrow");
    }

    private void UnParentPlayerAndMakeItFly(OnPlayerThrow pEvent)
    {
        player.parent = null;
        player.GetComponent<Freeze>().fly = false;
        player.GetComponentInChildren<Rigidbody>().useGravity = true;
        player.GetComponentInChildren<Rigidbody>().AddForce(Vector3.forward * 1000, ForceMode.Impulse);
    }
}
