using UnityEngine;

namespace TriplanoTest.Gameplay
{
    public interface IPushable 
    {
        Vector3 Position { get; }
        bool IsMoving { get; }
        Vector3 GetHoldPoint(Transform pivot, out Vector3 direction);
        bool Push(Vector3 direction, float speed);
    }
}