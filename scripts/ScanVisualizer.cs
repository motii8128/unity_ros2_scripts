using System;
using UnityEngine;

namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class ScanVisualizer : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private ISubscription<sensor_msgs.msg.LaserScan> subscription;

    void Start()
    {
        ros2Unity = GetComponent<ROS2UnityComponent>();
    }

    void Update()
    {
        if (ros2Node == null && ros2Unity.Ok())
        {
            ros2Node = ros2Unity.CreateNode("ScanVisualizerNode");
            subscription = ros2Node.CreateSubscription<sensor_msgs.msg.LaserScan>(
              "/scan", msg=>Debug.Log(msg));
        }
    }
}

}  // namespace ROS2
