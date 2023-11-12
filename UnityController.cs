using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class UnityController : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private IPublisher<geometry_msgs.msg.Twist> unity_controller;

    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
    }

    void Update()
    {
        if (ros2Unity.Ok())
        {
            if (ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("UnityControllerNode");
                unity_controller = ros2Node.CreatePublisher<geometry_msgs.msg.Twist>("/turtle1/cmd_vel");
            }

            geometry_msgs.msg.Twist msg = new geometry_msgs.msg.Twist();
            msg.Linear.X = Fix(Input.GetAxis("left_x"));
            msg.Linear.Y = Fix(-1.0 * Input.GetAxis("left_y"));
            msg.Angular.Z = Fix(-1.0 * Input.GetAxis("right_x"));

            
            unity_controller.Publish(msg);
        }
    }

    double Fix(double value)
    {
        if(value > 0.0)
        {
            if(value < 0.1)
            {
                value = 0.0;
            }
        }
        else
        {
            if(value > -0.1)
            {
                value = 0.0;
            }
        }

        return value;
    }
}

}  // namespace ROS2
