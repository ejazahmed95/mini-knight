using UnityEngine;

namespace MiniKnight.Player {
    
    public partial class CharacterController2D {
        private static readonly int IsWallSliding = Animator.StringToHash("IsWallSliding");
        public class WallGrabState: CharacterStateBase {
            public WallGrabState(CharacterController2D controller) : base(controller) {
                
            }
            
            public override bool BeginState(out CharacterStateBase alternateState) {
                //ApplyJumpForce();
                controller.stateData.ActiveStateText = "Wall";
                controller._animator.SetBool(IsWallSliding, true);
                //Flip();
                return base.BeginState(out alternateState);
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        controller.stateData.Movement = inputCommandData.ValueFloat;
                        // SetPlayerMovement(inputCommandData.ValueFloat);
                        return null;
                    case InputCommandType.JUMP:
                        float direction = controller.stateData.IsFacingLeft ? 1 : -1;
                        controller._rigidbody.AddForce(new Vector2(direction*controller.stateData.JumpForce, 0));
                        return controller.AllStates.JumpingState;
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
                controller._rigidbody.velocity = new Vector2(velocity.x, -2f);
                UpdateChecks();
                if (data.IsWallSliding == false) {
                    controller._animator.SetBool(IsWallSliding, false);
                    return controller.AllStates.IdleState;
                }
                MoveCharacterHorizontal();
                return null;

                // m_IsWall = false;
            }

            public override bool EndState() {
                //Flip();
                controller._animator.SetBool(IsWallSliding, false);
                return base.EndState();
            }

        }
    }
}