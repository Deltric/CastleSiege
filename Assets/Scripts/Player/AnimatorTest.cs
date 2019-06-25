﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorTest : MonoBehaviour
{

    [SerializeField]
    private Animator charAnimator;
    private float moveSpeed = 3.0f;

    public SpriteRenderer charKing;
    public Transform charTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            kill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            charAnimator.SetBool("IsDead", false);
            charAnimator.Update(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            charAnimator.SetTrigger("Attack");
        }

        Vector3 move_vector = new Vector3();
        bool isDead = charAnimator.GetBool("IsDead");

        if (Input.GetKey(KeyCode.A) && !isDead)
        {
            charKing.flipX = true;
            move_vector.x -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && !isDead)
        {
            charKing.flipX = false;
            move_vector.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) && !isDead)
        {
            move_vector.y += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && !isDead)
        {
            move_vector.y -= moveSpeed * Time.deltaTime;
        }
        if (move_vector.magnitude > 0)
        {
            charAnimator.SetTrigger("StartWalking");
            charTransform.position += move_vector;
        }
        else { charAnimator.SetTrigger("ReturnToIdle"); }
    }

    void kill()
    {
        charAnimator.SetBool("IsDead", true);
        charAnimator.Update(0);
    }

}
