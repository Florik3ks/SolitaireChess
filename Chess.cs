using System.Collections.Generic;
namespace SolitaireChess
{
    static class Chess
    {
        public const int rows = 4;
        public enum Piece
        {
            None, Pawn, Rook, Bishop, Knight, Queen, King
        };
        public static string[] symbols = new string[]{
            " ", "♙", "♖", "♗", "♘", "♕", "♔"
        };

        public static int[] CanMoveTo(Piece piece, int positionIndex, int[] board)
        {
            List<int> possiblePositions = new List<int>();
            switch (piece)
            {
                case Piece.None:
                    break;
                case Piece.Pawn:
                    possiblePositions.AddRange(Pawn(positionIndex, board));
                    break;
                case Piece.Rook:
                    possiblePositions.AddRange(Rook(positionIndex, board));
                    break;
                case Piece.Bishop:
                    possiblePositions.AddRange(Bishop(positionIndex, board));
                    break;
                case Piece.Knight:
                    possiblePositions.AddRange(Knight(positionIndex, board));
                    break;
                case Piece.Queen:
                    possiblePositions.AddRange(Bishop(positionIndex, board));
                    possiblePositions.AddRange(Rook(positionIndex, board));
                    break;
                case Piece.King:
                    possiblePositions.AddRange(King(positionIndex, board));
                    break;
                default:
                    break;
            }

            // remove non-occupied squares
            for (int i = possiblePositions.Count - 1; i >= 0; i--)
            {
                if (board[possiblePositions[i]] == (int)Piece.None)
                {
                    possiblePositions.RemoveAt(i);
                }
            }
            return possiblePositions.ToArray();
        }

        static int[] Pawn(int positionIndex, int[] board)
        {
            List<int> possiblePositions = new List<int>();
            if (positionIndex >= rows)
            {
                if (positionIndex % rows != 0) // left
                {
                    possiblePositions.Add(positionIndex - rows - 1);
                }
                if (positionIndex % rows != rows - 1) // right
                {
                    possiblePositions.Add(positionIndex - rows + 1);
                }
            }
            return possiblePositions.ToArray();
        }
        static int[] Rook(int positionIndex, int[] board)
        {
            List<int> possiblePositions = new List<int>();

            for (int i = 1; i <= positionIndex / rows; i++) // up
            {
                possiblePositions.Add(positionIndex - rows * i);
                if (board[positionIndex - rows * i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            for (int i = 1; i <= rows - (positionIndex / rows) - 1; i++) // down
            {
                possiblePositions.Add(positionIndex + rows * i);
                if (board[positionIndex + rows * i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            for (int i = 1; i <= positionIndex % rows; i++) // left
            {
                possiblePositions.Add(positionIndex - i);
                if (board[positionIndex - i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            for (int i = 1; i < rows - (positionIndex % rows); i++) // right
            {
                possiblePositions.Add(positionIndex + i);
                if (board[positionIndex + i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            return possiblePositions.ToArray();
        }
        static int[] Bishop(int positionIndex, int[] board)
        {
            List<int> possiblePositions = new List<int>();
            for (int i = positionIndex - rows - 1; i > 0 && i % rows != rows - 1; i = i - rows - 1) // up left
            {
                possiblePositions.Add(i);
                if (board[i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            for (int i = positionIndex - rows + 1; i > 0 && i % rows != 0; i = i - rows + 1) // up right
            {
                possiblePositions.Add(i);
                if (board[i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            for (int i = positionIndex + rows - 1; i < rows * rows && i % rows != rows - 1; i = i + rows - 1) // down left
            {
                possiblePositions.Add(i);
                if (board[i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            for (int i = positionIndex + rows + 1; i < rows * rows && i % rows != 0; i = i + rows + 1) // down right
            {
                possiblePositions.Add(i);
                if (board[i] != (int)Chess.Piece.None)
                {
                    break;
                }
            }
            return possiblePositions.ToArray();
        }
        static int[] Knight(int positionIndex, int[] board)
        {
            List<int> possiblePositions = new List<int>();

            int spacesUp = positionIndex / rows;
            int spacesDown = rows - (positionIndex / rows) - 1;
            int spacesRight = rows - (positionIndex % rows) - 1;
            int spacesLeft = positionIndex % rows;
            if (spacesUp >= 2 && spacesRight >= 1)
            {
                possiblePositions.Add(positionIndex - rows * 2 + 1);
            }
            if (spacesUp >= 1 && spacesRight >= 2)
            {
                possiblePositions.Add(positionIndex - rows + 2);
            }
            if (spacesDown >= 1 && spacesRight >= 2)
            {
                possiblePositions.Add(positionIndex + rows + 2);
            }
            if (spacesDown >= 2 && spacesRight >= 1)
            {
                possiblePositions.Add(positionIndex + rows * 2 + 1);
            }
            if (spacesDown >= 2 && spacesLeft >= 1)
            {
                possiblePositions.Add(positionIndex + rows * 2 - 1);
            }
            if (spacesDown >= 1 && spacesLeft >= 2)
            {
                possiblePositions.Add(positionIndex + rows - 2);
            }
            if (spacesUp >= 1 && spacesLeft >= 2)
            {
                possiblePositions.Add(positionIndex - rows - 2);
            }
            if (spacesUp >= 2 && spacesLeft >= 1)
            {
                possiblePositions.Add(positionIndex - rows * 2 - 1);
            }
            return possiblePositions.ToArray();
        }
        static int[] King(int positionIndex, int[] board)
        {
            List<int> possiblePositions = new List<int>();
            if (positionIndex >= rows) // up
            {
                possiblePositions.Add(positionIndex - rows);
                if (positionIndex % rows != 0) // left
                {
                    possiblePositions.Add(positionIndex - rows - 1);
                }
                if (positionIndex % rows != rows - 1) // right
                {
                    possiblePositions.Add(positionIndex - rows + 1);
                }
            }
            if (positionIndex % rows != 0) // left
            {
                possiblePositions.Add(positionIndex - 1);
            }
            if (positionIndex % rows != rows - 1) // right
            {
                possiblePositions.Add(positionIndex + 1);
            }
            if (positionIndex + rows < rows * rows) // down
            {
                possiblePositions.Add(positionIndex + rows);
                if (positionIndex % rows != 0) // left
                {
                    possiblePositions.Add(positionIndex + rows - 1);
                }
                if (positionIndex % rows != rows - 1) // right
                {
                    possiblePositions.Add(positionIndex + rows + 1);
                }
            }
            return possiblePositions.ToArray();
        }
    }
}