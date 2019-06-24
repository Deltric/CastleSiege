using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorTest : MonoBehaviour
{

    [SerializeField]
    private Animator charAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            charAnimator.SetBool("IsDead", true);
            charAnimator.Update(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            charAnimator.SetBool("IsDead", false);
            charAnimator.Update(0);
        }
    }

}
