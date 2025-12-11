using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    bool isTalking = false;

    float speed, rotationAroundY;
    Animator anim;
    CharacterController controller;
    AnimatorStateInfo info;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Diana" && !isTalking)
        {
            hit.collider.gameObject.GetComponent<DialogueSystem>().startDialogue();
            isTalking = true;
            anim.SetFloat("speed", 0);
            hit.collider.isTrigger = true;
            hit.collider.gameObject.GetComponent<BoxCollider>().size = new Vector3(2, 1, 2);
        }
    }
    public void EndTalking()
    {
        isTalking = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTalking) return;

        info = anim.GetCurrentAnimatorStateInfo(0);
        speed = Input.GetAxis("Vertical");
        rotationAroundY = Input.GetAxis("Horizontal"); anim.SetFloat("speed", speed);
        gameObject.transform.Rotate(0, rotationAroundY, 0);
        if (speed > 0) controller.Move(transform.forward * speed * 2.0f * Time.deltaTime);
    }
}
