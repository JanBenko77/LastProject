using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    public Transform destination;
    private Transform player;

    private void OnEnable()
    {
        EventBus<OnAnimationComplete>.OnEvent += Teleport;
    }

    private void OnDisable()
    {
        EventBus<OnAnimationComplete>.OnEvent -= Teleport;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.transform;
            EventBus<OnTeleporterEntered>.Invoke(new OnTeleporterEntered());
        }
    }

    private void Teleport(OnAnimationComplete pEvent)
    {
        player.position = destination.position + new Vector3(0, 1, 0);
    }
}
