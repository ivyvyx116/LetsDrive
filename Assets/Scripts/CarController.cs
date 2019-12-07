using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] tracks;
    public void Start()
    {
        mt = FindObjectOfType<MissionTimer>();
        foreach (GameObject track in tracks){
            track.SetActive(false);
        }
    }
    public void GetInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey(KeyCode.LeftShift);
	}

    private void Steer()
	{
		steeringAngle = maxSteerAngle * horizontalInput;
		frontLeft.steerAngle = steeringAngle;
		frontRight.steerAngle = steeringAngle;
	}

    private void Accelerate()
	{
        if (brakeInput)
        {
            Brake();
        }
        else
        {
            frontLeft.motorTorque = verticalInput * motorForce;
            frontRight.motorTorque = verticalInput * motorForce;
        }
    }

    private void Brake()
    {
        var velocity = gameObject.GetComponent<Rigidbody>().velocity;
        var localVel = transform.InverseTransformDirection(velocity);

        print(gameObject.GetComponent<Rigidbody>().transform.forward);
        if (localVel.z > 0)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-(transform.forward * brake));
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().AddForce((transform.forward * brake));
        }
    }

    private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontLeft, frontLeftT);
		UpdateWheelPose(frontRight, frontRightT);
		UpdateWheelPose(backLeft, backLeftT);
		UpdateWheelPose(backRight, backRightT);
	}

    private void UpdateWheelPose(WheelCollider currCollider, Transform currTransform)
	{
		Vector3 pos = currTransform.position;
		Quaternion quat = currTransform.rotation;

		currCollider.GetWorldPose(out pos, out quat);

		currTransform.position = pos;
		currTransform.rotation = quat;
	}

    private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}

    private void OnTriggerEnter(Collider col)
    {
        print("sTART " + numToCollect);
        if (col.gameObject.tag == "Collectable")
        {
            col.gameObject.SetActive(false);
            // JINGLE SOUND
            numToCollect--;

            print("COLLECT " + numToCollect);

            if (numToCollect <= 0)
            {
                mt.GetComponent<MissionTimer>().Win();
            }
        }
        else if (col.gameObject.tag == "battery")
        {
            battery = col.gameObject;
            batteryScript = battery.GetComponent<RandomMovement>();
            print("bat " + batteryScript.ActivateLevel);
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
        battery.SetActive(true);
        battery.GetComponent<RandomMovement>().ActivateLevel.SetActive(false);
    }

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
}
