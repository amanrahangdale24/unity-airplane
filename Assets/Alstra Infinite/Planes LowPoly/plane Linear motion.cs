using UnityEngine;

public class AirplaneMotion : MonoBehaviour
{
    public float takeoffSpeed = 2f;   // Takeoff motion speed
    public float upwardSpeed = 2f;    // Upward motion speed
    public float forwardSpeed = 5f;   // Forward motion speed
    public float landingSpeed = 2f;   // Landing motion speed
    public float stopDistance = 30f;  // Distance to travel before stopping

    private enum MotionState
    {
        Takeoff,
        Upward,
        Forward,
        Landing,
        Stopped
    }

    private MotionState motionState = MotionState.Takeoff; // Initial motion state
    private Vector3 initialPosition;   // Initial position of the airplane

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        switch (motionState)
        {
            case MotionState.Takeoff:
                // Move the airplane upward during takeoff
                transform.Translate(Vector3.up * takeoffSpeed * Time.deltaTime);

                // Check if the desired takeoff height is reached
                if (transform.position.y >= 5f)
                {
                    motionState = MotionState.Upward; // Change the state to start upward motion
                }
                break;

            case MotionState.Upward:
                // Move the airplane upward
                transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime);

                // Check if the desired height is reached
                if (transform.position.y >= 10f)
                {
                    motionState = MotionState.Forward; // Change the state to start forward motion
                }
                break;

            case MotionState.Forward:
                // Move the airplane forward
                transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

                // Check if the desired distance is reached
                if (Vector3.Distance(initialPosition, transform.position) >= stopDistance)
                {
                    motionState = MotionState.Landing; // Change the state to start landing motion
                }
                break;

            case MotionState.Landing:
                // Move the airplane downward during landing
                transform.Translate(Vector3.down * landingSpeed * Time.deltaTime);

                // Check if the airplane has returned to the initial position
                if (transform.position.y <= initialPosition.y)
                {
                    transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
                    motionState = MotionState.Stopped; // Change the state to stop all motion
                }
                break;

            case MotionState.Stopped:
                // Do nothing, the airplane has stopped
                break;
        }
    }
}
