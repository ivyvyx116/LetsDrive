using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public void Start()
    {
        // Set the timer, and deactivate all tracks at the start
        mt = FindObjectOfType<MissionTimer>();
        foreach (GameObject track in tracks){
            track.SetActive(false);
        }
    }
    public void GetInput()
	{
        // Get the inputs for left and right, forward and back, and brake and reset
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.Space);

        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            ResetCar();
        }
    }

    private void Steer()
	{
        // Set the steering correctly
		steeringAngle = maxSteerAngle * horizontalInput;
		frontLeft.steerAngle = steeringAngle;
		frontRight.steerAngle = steeringAngle;
	}

    private void Accelerate()
	{
        // If braking, then brake
        if (brakeInput)
        {
            Brake();
        }
        else
        {
            //Set the wheels to the appropriate torque
            frontLeft.motorTorque = verticalInput * motorForce;
            frontRight.motorTorque = verticalInput * motorForce;
        }
    }

    private void Brake()
    {
        // Get the velocity to determine the direcetion of the car, to know whether to brake forwards or back
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 localVel = transform.InverseTransformDirection(velocity);

        if (localVel.z > 0)
        {
            // Brake forwards
            gameObject.GetComponent<Rigidbody>().AddForce(-(transform.forward * brake));
        }
        else
        {
            // Brake back
            gameObject.GetComponent<Rigidbody>().AddForce((transform.forward * brake));
        }
    }

    private void ResetCar()
    {
        // Reset car for when it is turned over, etc.
        gameObject.transform.SetPositionAndRotation(gameObject.transform.position + move, rotate);
    }

    private void UpdateWheelPoses()
	{
        // Call the function that makes the wheels look correct on all wheels
		UpdateWheelPose(frontLeft, frontLeftT);
		UpdateWheelPose(frontRight, frontRightT);
		UpdateWheelPose(backLeft, backLeftT);
		UpdateWheelPose(backRight, backRightT);
	}

    private void UpdateWheelPose(WheelCollider currCollider, Transform currTransform)
	{
        // Get the wheel's 
		Vector3 pos = currTransform.position;
		Quaternion quat = currTransform.rotation;

		currCollider.GetWorldPose(out pos, out quat);

        // Set the positions to be correct
		currTransform.position = pos;
		currTransform.rotation = quat;
	}

    private void FixedUpdate()
	{

        // Call all previous functions
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}

    private void OnTriggerEnter(Collider col)
    {
        // If collectable, deactivate the collectable and count down
        if (col.gameObject.tag == "Collectable")
        {
            col.gameObject.SetActive(false);
            numToCollect--;

            if (numToCollect <= 0)
            {
                mt.GetComponent<MissionTimer>().Win();
            }
        }
        else if (col.gameObject.tag == "battery")
        {
            // Get the battery information
            battery = col.gameObject;
            batteryScript = battery.GetComponent<RandomMovement>();
            batteryScript.ActivateLevel.SetActive(true);
            // set canvas displays
            batteryScript.md.NewMission(batteryScript.missionDescription, batteryScript.missionMinute, batteryScript.missionSeconds);
            // set the description accordingly;
            // set time;
            batteryScript.mt.BeginTiming(batteryScript.missionMinute, batteryScript.missionSeconds);
            // call timer and update
            numToCollect = batteryScript.objectsToCollect;
            battery.SetActive(false);
        }
    }

    public void resetTrack()
    {
        // If lose, reset the battery
        battery.SetActive(true);
        battery.GetComponent<RandomMovement>().ActivateLevel.SetActive(false);
    }

    // Variables
    public Vector3 move = new Vector3(0, 2, 0);
    public Quaternion rotate = new Quaternion(0, 0, 0, 1);

    private float horizontalInput;
	private float verticalInput;
    private bool brakeInput;
    private float steeringAngle;

	public WheelCollider frontLeft, frontRight;
	public WheelCollider backLeft, backRight;
	public Transform frontLeftT, frontRightT;
	public Transform backLeftT, backRightT;
	public float maxSteerAngle;
	public float motorForce;
    public float brake;

    public int numToCollect;
    public MissionTimer mt;
    public GameObject battery;
    private RandomMovement batteryScript;
    public GameObject[] tracks;
}
