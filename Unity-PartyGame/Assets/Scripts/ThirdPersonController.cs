using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private KeyCode danceKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode walkForwardKey = KeyCode.W;
    [SerializeField] private KeyCode walkBackKey = KeyCode.S;
    [SerializeField] private KeyCode walkLeftKey = KeyCode.A;
    [SerializeField] private KeyCode walkRightKey = KeyCode.D;
    //[SerializeField] private KeyCode punchKey = KeyCode.Space;
    private bool isDancing;

    private void Update() {
        HandleMovementInput();
        HandleAnimationInput();
    }

    private void HandleMovementInput() {
        if(Input.GetKey(walkForwardKey)) {

        } else if (Input.GetKey(walkBackKey)) {
        
        } else if (Input.GetKey(walkLeftKey)) {

        } else if (Input.GetKey(walkRightKey)) {

        }
    }

    private void HandleAnimationInput() {
        if(Input.GetKeyDown(danceKey)) {
            if(isDancing) {
                anim.SetBool("EnableHipHop", false);
                anim.SetBool("EnableIdle", true);
                isDancing = false;
            } else {
                anim.SetBool("EnableHipHop", true);
                anim.SetBool("EnableIdle", false);
                isDancing = true;
            }
        }
    }
}
