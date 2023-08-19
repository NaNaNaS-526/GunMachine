using UnityEngine;

namespace Character.Movement
{
    public class MovementController : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private WheelJoint2D rearWheel;
        [SerializeField] private WheelJoint2D frontWheel;

        [Range(0.0f, 1000.0f)] [SerializeField] private float movementSpeed;

        public void Move(float direction)
        {
            RotateWheel(rearWheel, direction);
            RotateWheel(frontWheel, direction);
        }

        private void RotateWheel(WheelJoint2D wheel, float direction)
        {
            var motor = wheel.motor;
            motor.motorSpeed = direction * movementSpeed;
            wheel.motor = motor;
        }
    }
}