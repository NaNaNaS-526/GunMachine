using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct Wheel
    {
        public float speed;
        public WheelJoint2D wheel;
    }
}