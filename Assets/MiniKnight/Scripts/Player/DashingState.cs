namespace MiniKnight.Player {
    public class DashingState: CharacterController2D.CharacterStateBase {

        public override CharacterController2D.CharacterStateBase HandleInput(InputCommandData inputCommandData) {
            throw new System.NotImplementedException();
        }
        public DashingState(CharacterController2D controller) : base(controller) {
        }
    }
}