using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }

}
