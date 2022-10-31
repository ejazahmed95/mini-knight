namespace MiniKnight.Player {
    
    public class WallGrabState: CharacterController2D.CharacterStateBase {

        public override CharacterController2D.CharacterStateBase HandleInput(InputCommandData inputCommandData) {
            throw new System.NotImplementedException();
        }
        public WallGrabState(CharacterController2D controller) : base(controller) {
        }
    }
}