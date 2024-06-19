using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatform : MonoBehaviour
{
    public Transform destination;
    [SerializeField] private Transform player;

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
            //player = other.GetComponentInParent<GameObject>().transform;
            EventBus<OnTeleporterEntered>.Invoke(new OnTeleporterEntered());
        }
    }

    private void Teleport(OnAnimationComplete pEvent)
    {
        Debug.Log("This got called");
        player.position = destination.position + new Vector3(0, 1, 0);
    }
}
