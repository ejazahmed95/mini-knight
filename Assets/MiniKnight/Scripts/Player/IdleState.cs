

namespace MiniKnight.Player {
    partial class CharacterController2D {
        public class IdleState: CharacterStateBase {
            public IdleState(CharacterController2D characterController2D) : base(characterController2D) {
                // throw new System.NotImplementedException();
            }

            public override bool BeginState(out CharacterStateBase alternateState) {
                base.BeginState(out alternateState);
                UpdateChecks();
                if (controller.stateData.IsGrounded == false) {
                    alternateState = controller.AllStates.FallingState;
                    return false;
                } 
                if (controller.stateData.Movement != 0 || controller._rigidbody.velocity.magnitude > 0.01) {
                    alternateState = controller.AllStates.MovingState;
                    return false;
                }
                controller.stateData.ActiveStateText = "Idling";
                controller._debugger.SetStateText("Idling");

                StopMovement();
                return true;
            }

            public override CharacterStateBase FixedUpdate() {
                //UpdateChecks();
                // if (controller.stateData.IsGrounded == false) {
                //     return controller.AllStates.FallingState;
                // } 
                return null;
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        SetPlayerMovement(inputCommandData.ValueFloat);
                        return controller.AllStates.MovingState;
                    case InputCommandType.JUMP:
                        return controller.AllStates.JumpingState;
                    case InputCommandType.ATTACK:
                        break;
                    case InputCommandType.SHOOT:
                        break;
                    case InputCommandType.DASH:
                        break;
                    default:
                        break;
                }
                return null;
            }
        }
    }
}