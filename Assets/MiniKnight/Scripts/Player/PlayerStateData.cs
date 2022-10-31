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
        public float GroundedRadius;
        public bool IsGrounded;

        [Header("Skills")]
        public bool CanDoubleJump;
        public bool CanWallGrab;
        public bool CanDash;
        
        [Header("Animations")]
        public ParticleSystem JumpUpParticles;
        public ParticleSystem JumpDownParticles; 
        
        [Header("Active State Data")]
        public string ActiveStateText;
        public float Movement;
        public bool IsFacingLeft;
        public bool IsDoubleJumping;
        public bool InAir;
        public Vector2 velocity;

        [Header("Transform Checks")][Space]
        public Transform GroundCheck;
        public Transform WallCheck;
    }
}