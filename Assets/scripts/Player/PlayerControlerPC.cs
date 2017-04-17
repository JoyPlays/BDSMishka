using UnityEngine;
using System.Collections;

public class PlayerControlerPC : MonoBehaviour
{
    public bool ZAxisControl;

    public CharacterController MyController;

    public float Speed = 3f;
    public float AerialSpeed = 2f;
    public float GravityStrenght = 5f;
    public float JumpSpeed = 10f;
    public Transform CameraTransform;

    private bool canJump;
    private float verticalVelocity;

    private Vector3 velocity;
    private Vector3 groundedVelocity;
    private Vector3 normal;
    private bool onWall = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 myVector = Vector3.zero;
        Vector3 input = Vector3.zero;

        input.x = Input.GetAxis("Horizontal");
        input.z = ZAxisControl ? Input.GetAxis("Vertical") : 0;

        input = Vector3.ClampMagnitude(input, 1);

        if (MyController.isGrounded)
        {
            myVector = input;

            // Quaternion inputRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(CameraTransform.forward, Vector3.up), Vector3.up);
            // myVector = myVector * inputRotation;

            myVector *= Speed;
        }
        else
        {
            myVector = groundedVelocity + input * AerialSpeed;
        }

        myVector *= Time.deltaTime;


        verticalVelocity -= GravityStrenght * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (onWall)
            {
                groundedVelocity = Vector3.Reflect(groundedVelocity, normal);
            }
            if (canJump)
            {
                verticalVelocity += JumpSpeed;
            }
        }

        myVector.y = verticalVelocity * Time.deltaTime;

        CollisionFlags flags = MyController.Move(myVector);
        velocity = myVector / Time.deltaTime;

        canJump = (flags & (CollisionFlags.Sides | CollisionFlags.Below)) != 0;
        onWall = (flags & CollisionFlags.Sides) != 0;
        if ((flags & CollisionFlags.Below) != 0)
        {
            verticalVelocity = -3f;
            groundedVelocity = Vector3.ProjectOnPlane(velocity,Vector3.up);
            onWall = false;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        normal = hit.normal;
       
    }
}
