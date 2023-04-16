using UnityEngine;
namespace RunAndGun.Space
{
    public struct EnemyTarget
    {
        public bool IsFound;
        public bool IsSeen;
        public Vector3 Position;
        public Vector3 LastKnownPosition;
    }
}