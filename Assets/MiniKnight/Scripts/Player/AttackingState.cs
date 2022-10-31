namespace MiniKnight.Player {
    public class AttackingState: CharacterController2D.CharacterStateBase {

        public override CharacterController2D.CharacterStateBase HandleInput(InputCommandData inputCommandData) {
            throw new System.NotImplementedException();
        }
        public AttackingState(CharacterController2D controller) : base(controller) {
        }
    }
}