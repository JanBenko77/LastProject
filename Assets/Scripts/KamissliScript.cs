using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KamissliScript : MonoBehaviour
{

    [SerializeField] private UnityEvent OnTriggerEnter;

    Animator anime;

    private void OnEnable()
    {
        EventBus<OnAnimationComplete>.OnEvent += Func;
    }

    private void OnDisable()
    {
        EventBus<OnAnimationComplete>.OnEvent -= Func;
    }

    private void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void Func(OnAnimationComplete pEvent)
    {
        Debug.Log("Started"); 
        anime.SetTrigger("TrGrow");
    }
}
