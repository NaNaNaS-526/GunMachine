using System;

namespace Input
{
    public interface IInputService
    {
        public event Action<float> OnGunRotationChanged;
        public event Action<float> OnGunPowderCoefficientChanged;
        public event Action OnShootAction;
        public float GetInputDirection();
    }
}