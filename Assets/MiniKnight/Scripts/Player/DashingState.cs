using System;
using UnityEngine;

namespace MiniKnight.Player {
   public partial class CharacterController2D {
       private static readonly int IsDashing = Animator.StringToHash("IsDashing");
       public class DashingState: CharacterStateBase {
            private float timeElapsed = 0f;
            
            public DashingState(CharacterController2D controller) : base(controller) {
                    
            }
            
            public override bool BeginState(out CharacterStateBase alternateState) {
                controller.stateData.ActiveStateText = "Dash";
                timeElapsed = 0f;
                controller._rigidbody.velocity = new Vector2(controller.transform.localScale.x * controller.stateData.DashForce, 0);
                controller._animator.SetBool(IsDashing, true);
                controller.stateData.IsDashUsed = true;
                return base.BeginState(out alternateState);
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
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
                        break;
                    default:
                        return null;
                }
                return null;
            }

            public override CharacterStateBase FixedUpdate() {
                
                UpdateChecks();
                // Wall Hit
                
                
                // var velocity = controller._rigidbody.velocity;
                // if (Math.Abs(velocity.x) < 0.01f) {
                //     return controller.AllStates.IdleState;
                // }

                if (timeElapsed > controller.stateData.DashDuration) {
                    return controller.AllStates.IdleState;
                }
                controller._rigidbody.velocity = new Vector2(controller.transform.localScale.x * controller.stateData.DashForce, 0);
                return null;
            }

            public override CharacterStateBase Update(float deltaTime) {
                timeElapsed += deltaTime;
                return base.Update(deltaTime);
            }

            public override bool EndState() {
                controller._animator.SetBool(IsDashing, false);
                return base.EndState();
            }
       }
    }
}