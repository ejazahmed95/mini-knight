using UnityEngine;

namespace MiniKnight.Player {
    [System.Serializable]
    public struct PlayerStateData {
        [Header("Defaults")][Space]
        public float JumpForce;
        public float MoveSpeed;
        public float MovementSmoothing;
        public LayerMask WhatIsGround;
        public bool AirControl;
        
        [Header("Active State Data")]
        public string ActiveStateText;
        public float Movement;
        public bool IsFacingLeft;
        public bool InAir;
        public Vector2 velocity;

        [Header("Transform Checks")][Space]
        public Transform GroundCheck;
        public Transform WallCheck;
    }
}