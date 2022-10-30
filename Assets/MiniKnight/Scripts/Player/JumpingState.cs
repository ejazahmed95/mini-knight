namespace MiniKnight.Player {
    public class JumpingState: CharacterController2D.CharacterStateBase {

        public override void HandleInput(InputCommandData inputCommandData) {
            throw new System.NotImplementedException();
        }
        public JumpingState(CharacterController2D controller) : base(controller) {
        }
    }
}