using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] private Transform playerL;

    [SerializeField] private Transform playerR;

    [SerializeField] private Transform player;
    [SerializeField] private Transform toBeThrown;

    public void FreezePlayer()
    {
        Debug.Log("Freeze");
        playerL.gameObject.SetActive(false);
        playerR.gameObject.SetActive(false);

        player.GetComponentInChildren<Rigidbody>().useGravity = false;
        player.parent = toBeThrown;
        player.position = toBeThrown.position;
    }
}
