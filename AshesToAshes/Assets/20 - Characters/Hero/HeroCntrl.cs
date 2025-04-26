using A2A;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 3.0f;
    [SerializeField] private float animationBlendSpeed = 8.9f;

    private PlayerInputCntrl inputCntrl;
    private Animator animator;

    private int xVelocity;
    private int yVelocity;

    private Vector2 currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputCntrl = GetComponent<PlayerInputCntrl>();

        xVelocity = Animator.StringToHash("xVelocity");
        yVelocity = Animator.StringToHash("yVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        float targetSpeed = inputCntrl.Run ? runSpeed : walkSpeed;
        if (inputCntrl.Move == Vector2.zero) targetSpeed = 0.0f;

        currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputCntrl.Move.x * targetSpeed, animationBlendSpeed * Time.deltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputCntrl.Move.y * targetSpeed, animationBlendSpeed * Time.deltaTime);

        //float xVelDiff = currentVelocity.x - rigidBody.velocity.x;
        //float zVelDiff = currentVelocity.y - rigidBody.velocity.z;

        //float xVelDiff = currentVelocity.x;
        //float zVelDiff = currentVelocity.y;

        float xVelDiff = inputCntrl.Move.x * targetSpeed;
        float zVelDiff = inputCntrl.Move.y * targetSpeed;

        animator.SetFloat(xVelocity, currentVelocity.x);
        animator.SetFloat(yVelocity, currentVelocity.y);
    }
}
