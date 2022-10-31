using System;
using RangerRPG.Core;
using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D {
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int JumpUp = Animator.StringToHash("JumpUp");
        
        public abstract class CharacterStateBase {
            protected CharacterController2D controller;

            public CharacterStateBase(CharacterController2D controller) {
                this.controller = controller;
            }
            
            public virtual bool BeginState(out CharacterStateBase alternateState) {
                alternateState = null;
                return true;
            }

            public virtual bool EndState() {
                return true;
            }

            public virtual CharacterStateBase Update(float deltaTime) {
                return null;
            }

            public virtual CharacterStateBase FixedUpdate() {
                return null;
            }

            public abstract CharacterStateBase HandleInput(InputCommandData inputCommandData);


            #region Sandbox Methods to be used by Subclass States
            protected void MoveCharacterHorizontal() {
                var velocity = controller._rigidbody.velocity;
                ref var data = ref controller.stateData;
                controller._animator.SetFloat(Speed, Math.Abs(data.Movement));
                var targetVel = new Vector2(data.Movement * data.MoveSpeed, velocity.y);
                controller._rigidbody.velocity = Vector2.SmoothDamp(velocity, targetVel, ref controller.stateData.velocity, data.MovementSmoothing);
            }

            protected void StopMovement() {
                controller._animator.SetFloat(Speed, 0);
                controller._rigidbody.velocity = Vector2.zero;
                controller.stateData.velocity = Vector2.zero;
            }

            protected void SetPlayerMovement(float valueFloat) {
                controller.stateData.Movement = valueFloat;
                //controller._animator.SetFloat(Speed, Math.Abs(valueFloat));
                switch (valueFloat) {
                    case > 0 when controller.stateData.IsFacingLeft:
                        Flip();
                        break;
                    case < 0 when (controller.stateData.IsFacingLeft == false):
                        Flip();
                        break;
                }
            }

            protected void UpdateChecks() {
                ref var data = ref controller.stateData;
                data.IsGrounded = false;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(data.GroundCheck.position, data.GroundedRadius, data.WhatIsGround);
                foreach (var collider in colliders) {
                    if (collider.gameObject != controller.gameObject) {
                        data.IsDoubleJumping = false;
                        data.IsGrounded = true;
                        //Log.Info($"Ground Collision with {collider.gameObject.name}; Position = {data.GroundCheck.position}; Radius = {data.GroundedRadius}");
                        return;
                        //return;
                    }
                    // if (!wasGrounded )
                    // {
                    //     OnLandEvent.Invoke();
                    //     if (!m_IsWall && !isDashing) 
                    //         particleJumpDown.Play();
                    //     canDoubleJump = true;
                    //     if (m_Rigidbody2D.velocity.y < 0f)
                    //         limitVelOnWallJump = false;
                    // }
                }
                //Log.Info("In Air!!!");
            }

            private void Flip() {
                controller.stateData.IsFacingLeft = !controller.stateData.IsFacingLeft;

                // Multiply the player's x local scale by -1.
                Vector3 theScale = controller.transform.localScale;
                theScale.x *= -1;
                controller.transform.localScale = theScale;
            }

            #endregion
            
        }
    }
}