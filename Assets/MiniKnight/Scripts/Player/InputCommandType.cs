namespace MiniKnight.Player {
    public enum InputCommandType {
        NONE,
        MOVE,
        JUMP,
        ATTACK,
        SHOOT,
        DASH
    }

    public struct InputCommandData {
        public InputCommandType Type;
        public bool ValueBool;
        public float ValueFloat;
        // public bool ValueVec;
    }
}