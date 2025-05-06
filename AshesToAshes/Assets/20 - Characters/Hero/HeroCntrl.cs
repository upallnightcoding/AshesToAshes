using A2A;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace A2A
{
    public class HeroCntrl : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 1.0f;
        [SerializeField] private float runSpeed = 3.0f;
        [SerializeField] private float animationBlendSpeed = 8.9f;
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private float rotationSpeed = 20.0f;
        [SerializeField] private Transform triggerPoint;
        [SerializeField] private ProjectileSO projectileSO;
        [SerializeField] private GameObject projectilePrefab;

        private PlayerInputCntrl inputCntrl;
        private Animator animator;

        private int xVelocityId;
        private int yVelocityId;
        private int fireId;

        private States currentState = States.MOVING;

        private Vector2 currentVelocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            inputCntrl = GetComponent<PlayerInputCntrl>();

            xVelocityId = Animator.StringToHash("xVelocity");
            yVelocityId = Animator.StringToHash("yVelocity");
            fireId = Animator.StringToHash("Fire");
        }

        private void Update() 
        {
            switch(currentState)
            {
                case States.MOVING:
                    currentState = State_Move();
                    break;
                case States.FIRE:
                    currentState = State_Fire();
                    break;
            }    
        }

        private States State_Move()
        {
            States state = States.MOVING;

            Move();

            if (inputCntrl.Fire)
            {
                state = States.FIRE;
            }

            return (state);
        }

        private States State_Fire()
        {
            States state = States.MOVING;

            OnAttack();
            inputCntrl.Fire = false;

            return (state);
        }

        // Update is called once per frame
        private void Move()
        {
            float targetRotation = mainCamera.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0.0f, targetRotation, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            float moveSpeed = inputCntrl.Run ? runSpeed : walkSpeed;
            currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputCntrl.Move.x * moveSpeed, animationBlendSpeed * Time.deltaTime);
            currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputCntrl.Move.y * moveSpeed, animationBlendSpeed * Time.deltaTime);

            animator.SetFloat(xVelocityId, currentVelocity.x);
            animator.SetFloat(yVelocityId, currentVelocity.y);
        }

        public void OnAttack()
        {
            Vector3 position = triggerPoint.position;
            Vector3 direction = transform.forward;

            GameObject go = Instantiate(projectilePrefab, position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(direction * projectileSO.speed, ForceMode.Impulse);
            go.GetComponent<ProjectileCntrl>().SetProjectileSO(projectileSO).CreateProjectileTrail(go.transform, position);
            Destroy(go, projectileSO.duration);
        }
    }

    public enum States
    {
        MOVING,
        FIRE
    }
}