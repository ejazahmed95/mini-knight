namespace MiniKnight.Player {
    public class FallingState: CharacterController2D.CharacterStateBase {

        public override CharacterController2D.CharacterStateBase HandleInput(InputCommandData inputCommandData) {
            throw new System.NotImplementedException();
        }
        public FallingState(CharacterController2D controller) : base(controller) {
        }
    }
}