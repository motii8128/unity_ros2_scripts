using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROS2
{

    public class swerve_drive_unity : MonoBehaviour
    {
        [SerializeField] WheelCollider wheelR, wheelL;
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private ISubscription<std_msgs.msg.Float32> sub_right_wheel;
        private ISubscription<std_msgs.msg.Float32> sub_left_wheel;
        private ISubscription<std_msgs.msg.Float32> sub_left_dir;
        private ISubscription<std_msgs.msg.Float32> sub_right_dir;
        private float power_left;
        private float power_right;
        private float dir_left;
        private float dir_right;



        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
            wheelL.steerAngle = 0;
            wheelR.steerAngle = 0;
        }

        void Update()
        {
            if (ros2Node == null && ros2Unity.Ok())
            {
                ros2Node = ros2Unity.CreateNode("ROS2UnityListenerNode");
                sub_left_wheel = ros2Node.CreateSubscription<std_msgs.msg.Float32>(
                "motor/power/left", msg => power_left = msg.Data * 100);

                sub_right_wheel = ros2Node.CreateSubscription<std_msgs.msg.Float32>(
                "motor/power/right", msg => power_right = msg.Data * 100);

                sub_left_dir = ros2Node.CreateSubscription<std_msgs.msg.Float32>(
                "motor/direction", msg => dir_left = msg.Data);

                sub_right_dir = ros2Node.CreateSubscription<std_msgs.msg.Float32>(
                "motor/direction", msg => dir_right = msg.Data);
            }

            wheelL.motorTorque = power_left;
            wheelL.steerAngle = wheelL.steerAngle + dir_left;
            wheelR.steerAngle = wheelR.steerAngle + dir_right;
            wheelR.motorTorque = power_right;
        }
    }

}  // namespace ROS2
