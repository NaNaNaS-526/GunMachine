using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct Gun
    {
        public Transform gunTransform;
        public float rotation;
        public float maxRotation;
        public bool isShooting;
        
        public Rigidbody2D bullet;
        public Transform bulletSpawnPoint;
        public float bulletForce;
    }
}