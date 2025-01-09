using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotSpeed = 15f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float terminalVelocity = -10f;
    [SerializeField] private float minFall = -1.5f;
    [SerializeField] private float pushForce = 3f;
    [SerializeField] private float deceleration = 25f;
    [SerializeField] private float targetBuffer = 1.5f;

    private CharacterController charController;
    private float vertSpeed;
    private ControllerColliderHit contact;
    private Animator animator;
    private float curSpeed;
    private Vector3? targetPos;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        vertSpeed = minFall;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //float horInput = Input.GetAxis("Horizontal");
        //float vertInput = Input.GetAxis("Vertical");
        //if (horInput != 0 || vertInput != 0)
        //{
        //    Vector3 right = target.right;
        //    Vector3 forward = Vector3.Cross(right, Vector3.up);
        //    movement = right * horInput + forward * vertInput;
        //    movement *= moveSpeed;
        //    movement = Vector3.ClampMagnitude(movement, moveSpeed);
        //    Quaternion direction = Quaternion.LookRotation(movement);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        //}
        Vector3 movement = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(ray, out mouseHit))
            {
                targetPos = mouseHit.point;
                curSpeed = moveSpeed;
            }
        }

        if (targetPos != null)
        {
            if (curSpeed > moveSpeed * 0.5f)
            {
                Vector3 adjustedPos = new Vector3(targetPos.Value.x, transform.position.y, targetPos.Value.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            if (Vector3.Distance(targetPos.Value, transform.position) < targetBuffer)
            {
                curSpeed -= deceleration * Time.deltaTime;
                if (curSpeed <= 0)
                {
                    targetPos = null;
                }
            }
        }



        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
                animator.SetBool("Jumping", false);
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
            if (contact != null)
            {
                animator.SetBool("Jumping", true);
            }
            if (charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
