
namespace MiniKnight.Player {
    public partial class CharacterController2D {
        public class FallingState: CharacterStateBase {
            public FallingState(CharacterController2D controller) : base(controller) {
                
            }
            
            public override bool BeginState(out CharacterStateBase alternateState) {
                //ApplyJumpForce();
                controller.stateData.ActiveStateText = "Fall";
                return base.BeginState(out alternateState);
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        SetPlayerMovement(inputCommandData.ValueFloat);
                        return null;
                    case InputCommandType.JUMP:
                        if (controller.stateData.CanDoubleJump && controller.stateData.IsDoubleJumping == false) {
                            controller.stateData.IsDoubleJumping = true;
                            return controller.AllStates.JumpingState;
                        }
                        return null;
                    case InputCommandType.ATTACK:
                        break;
                    case InputCommandType.SHOOT:
                        break;
                    case InputCommandType.DASH:
                        if (controller.stateData.CanDoubleJump && controller.stateData.IsDashUsed == false) {
                            return controller.AllStates.DashingState;
                        }
                        break;
                    default:
                        return null;
                }
                return null;
            }

            public override CharacterStateBase FixedUpdate() {
                // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
                // This can be done using layers instead but Sample Assets will not overwrite your project settings.
                ref var data = ref controller.stateData;
                var velocity = controller._rigidbody.velocity;
                UpdateChecks();
                if (data.IsGrounded) {
                    data.JumpDownParticles.Play();
                    controller._animator.SetBool(IsJumping, false);
                    return controller.AllStates.IdleState;
                }
                
                if (data.CanWallGrab && data.IsWallSliding) {
                    return controller.AllStates.WallGrabState;
                }
                
                MoveCharacterHorizontal();
                return null;

                // m_IsWall = false;
            }
            
        }
    }
}