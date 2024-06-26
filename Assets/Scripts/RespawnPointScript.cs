using UnityEngine;

public class RespawnPointScript : MonoBehaviour
{
    private Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        if(other.tag == "RespawnPlane")
            transform.position = spawnPoint;
        if (other.tag == "WinPlane")
            EventBus<OnTeleporterEntered>.Invoke(new OnTeleporterEntered());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "WinPlane")
            EventBus<OnTeleporterEntered>.Invoke(new OnTeleporterEntered());
    }
}
