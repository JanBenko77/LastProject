using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    [SerializeField] private Transform playerL;

    [SerializeField] private Transform playerR;

    public void FreezePlayer()
    {
        playerL.gameObject.SetActive(false);
        playerR.gameObject.SetActive(false);
    }
}
