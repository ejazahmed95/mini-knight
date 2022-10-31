using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D {
        private static readonly int IsDoubleJumping = Animator.StringToHash("IsDoubleJumping");
        public class JumpingState: CharacterStateBase {
            public JumpingState(CharacterController2D controller) : base(controller) {
                
            }
            
            public override bool BeginState(out CharacterStateBase alternateState) {
                controller.stateData.ActiveStateText = "Jump";
                ApplyJumpForce(controller.stateData.IsDoubleJumping);
                return base.BeginState(out alternateState);
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        SetPlayerMovement(inputCommandData.ValueFloat);
                        return null;
                    case InputCommandType.JUMP:
                        if (controller.stateData.CanDoubleJump && controller.stateData.IsDoubleJumping == false) {
                            ApplyJumpForce(true);
                        }
                        return null;
                    case InputCommandType.ATTACK:
                        break;
                    case InputCommandType.SHOOT:
                        break;
                    case InputCommandType.DASH:
                        break;
                    default:
                        return null;
                }
                return null;
            }

            public override CharacterStateBase FixedUpdate() {
                // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
                // This can be done using layers instead but Sample Assets will not overwrite your project settings.
                UpdateChecks();
                var velocity = controller._rigidbody.velocity;
                if (velocity.y < 0) {
                    return controller.AllStates.FallingState;
                }
                MoveCharacterHorizontal();
                return null;
                //
                // Collider2D[] colliders = Physics2D.OverlapCircleAll(data.GroundCheck.position, k_GroundedRadius, );
                // for (int i = 0; i < colliders.Length; i++)
                // {
                //     if (colliders[i].gameObject != gameObject)
                //         m_Grounded = true;
                //     if (!wasGrounded )
                //     {
                //         OnLandEvent.Invoke();
                //         if (!m_IsWall && !isDashing) 
                //             particleJumpDown.Play();
                //         canDoubleJump = true;
                //         if (m_Rigidbody2D.velocity.y < 0f)
                //             limitVelOnWallJump = false;
                //     }
                // }
                //
                // m_IsWall = false;
            }


            protected void ApplyJumpForce(bool isDoubleJump) {
                controller.stateData.IsDoubleJumping = isDoubleJump;
                if (isDoubleJump) {
                    controller._rigidbody.velocity = new Vector2(controller._rigidbody.velocity.x, 0);
                }
                controller._rigidbody.AddForce(new Vector2(0, controller.stateData.JumpForce));
                controller._animator.SetBool(IsJumping, true);
                controller._animator.SetBool(JumpUp, true);
                controller._animator.SetBool(IsDoubleJumping, isDoubleJump);
            }
        }
    }
}