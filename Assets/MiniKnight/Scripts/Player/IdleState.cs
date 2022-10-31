
using System;

namespace MiniKnight.Player {
    partial class CharacterController2D {
        public class IdleState: CharacterStateBase {
            public IdleState(CharacterController2D characterController2D) : base(characterController2D) {
                // throw new System.NotImplementedException();
            }

            public override bool BeginState(out CharacterStateBase alternateState) {
                base.BeginState(out alternateState);
                controller.stateData.ActiveStateText = "Idling";
                controller._debugger.SetStateText("Idling");
                return true;
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        SetPlayerMovement(inputCommandData.ValueFloat);
                        return controller.AllStates.MovingState;
                        break;
                    case InputCommandType.JUMP:
                        break;
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