namespace MiniKnight.Player {
    public class ShootingState: CharacterController2D.CharacterStateBase {

        public override CharacterController2D.CharacterStateBase HandleInput(InputCommandData inputCommandData) {
            throw new System.NotImplementedException();
        }
        public ShootingState(CharacterController2D controller) : base(controller) {
        }
    }
}