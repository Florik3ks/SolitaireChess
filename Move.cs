namespace SolitaireChess
{
    class Move
    {
        public int startPosition;
        public int endPosition;
        public Chess.Piece captured;
        public Move(int a, int b, Chess.Piece captured){
            startPosition = a;
            endPosition = b;
            this.captured = captured;
        }
    }
}