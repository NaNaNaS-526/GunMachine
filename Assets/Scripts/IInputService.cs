using System;

public interface IInputService
{
    public event Action<float> OnGunRotationSliderValueChanged;
    public event Action OnShootAction;
    public float GetInputDirection();
}