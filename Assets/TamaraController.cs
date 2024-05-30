using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamaraController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("animHasStarted", true);
        }
    }
}
