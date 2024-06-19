using UnityEngine;
public class TeleportPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody.CompareTag("Player"))
        {
            EventBus<OnTeleporterEntered>.Invoke(new OnTeleporterEntered());
        }
    }
}
