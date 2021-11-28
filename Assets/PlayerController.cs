using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public float speed;
    PhotonView view;
   // Camera m_MainCamera;

    private const string HORIZONTAL  = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteeringAngle;
    private float currentBreakingForce; 
    private bool isBreaking;

    [SerializeField] private float motorforce, breakforce, maxSteeringAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider, backLeftWheelCollider, backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;

  //  public Rigidbody rb;
   // public Transform car;


   /* Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 rotationLeft = new Vector3(0, -30, 0);

    Vector3 forward = new Vector3(0, 0, 1);
    Vector3 backward = new Vector3(0, 0, -1);*/

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);

        verticalInput = Input.GetAxis(VERTICAL);

        isBreaking = Input.GetKey(KeyCode.Space);

    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorforce * speed;
        frontRightWheelCollider.motorTorque = verticalInput * motorforce * speed;

        currentBreakingForce = isBreaking ? breakforce : 0f;

        ApplyBreaking();


    }


    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakingForce;
        frontRightWheelCollider.brakeTorque = currentBreakingForce;

        backLeftWheelCollider.brakeTorque = currentBreakingForce;
        backRightWheelCollider.brakeTorque = currentBreakingForce;

    }


    private void HandleSteering()
    {
        currentSteeringAngle = maxSteeringAngle * horizontalInput;


        frontLeftWheelCollider.steerAngle = currentSteeringAngle;
        frontRightWheelCollider.steerAngle = currentSteeringAngle;


    }


    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);

    }


    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.rotation = rot;

        wheelTransform.position = pos;
    }





    // Start is called before the first frame update
    void Start()
    {


        view = GetComponent<PhotonView>();
      //  rb = GetComponent<Rigidbody>();
       // car = GetComponent<Transform>();

                            

        if (view.IsMine)
        {
           // m_MainCamera.enabled = true;   ForTest
            //GetComponent<Camera>().enable = true;
        }
        else
        {
           // m_MainCamera.enabled = false; for Test

            //GetComponent<Camera>().enable = true;
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
           // Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
           // Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
           // transform.position += (Vector3)moveAmount;


           

        }
        
    }
}
