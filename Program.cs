using System;
using System.Collections.Generic;

namespace SolitaireChess
{
    class Program
    {
        // 0  1  2  3
        // 4  5  6  7
        // 8  9  10 11
        // 12 13 14 15
        static void Main(string[] args)
        {
            // Console.InputEncoding = System.Text.Encoding.UTF8;
            int[] board = new int[16];
            board[1] = (int)Chess.Piece.Knight;
            board[3] = (int)Chess.Piece.Bishop;
            board[8] = (int)Chess.Piece.Rook;
            board[9] = (int)Chess.Piece.Rook;
            board[12] = (int)Chess.Piece.Knight;
            board[14] = (int)Chess.Piece.Bishop;


            List<Move> moves = SolveProblem(board, new List<Move>());
            if(moves == null){
                Console.WriteLine("Keine Lösung gefunden");
                return;
            }
            PrintBoard(board);
            foreach (Move m in moves)
            {
                board[m.endPosition] = board[m.startPosition];
                board[m.startPosition] = (int)Chess.Piece.None;
                PrintBoard(board);
            }
            Console.Read();
        }
        static List<Move> SolveProblem(int[] board, List<Move> moves)
        {
            if (OnlyOnePieceIsRemaining(board))
            {
                return moves;
            }
            foreach (int piecePosition in GetPositionsOfPieces(board))
            {
                foreach (int move in Chess.CanMoveTo((Chess.Piece)board[piecePosition], piecePosition, board))
                {
                    Move m = new Move(piecePosition, move, (Chess.Piece)board[move]);
                    moves.Add(m);
                    int[] boardCopy = (int[])board.Clone();
                    boardCopy[move] = board[piecePosition];
                    boardCopy[piecePosition] = (int)Chess.Piece.None;
                    List<Move> newMoves = SolveProblem(boardCopy, moves);
                    if (newMoves != null)
                    {
                        return newMoves;
                    }

                    moves.Remove(m);
                }
            }
            return null;
        }
        static int[] GetPositionsOfPieces(int[] board)
        {
            List<int> positions = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] != (int)Chess.Piece.None)
                {
                    positions.Add(i);
                }
            }
            return positions.ToArray();
        }
        static bool OnlyOnePieceIsRemaining(int[] board)
        {
            int c = 0;
            foreach (int i in board)
            {
                if (i != (int)Chess.Piece.None)
                {
                    c++;
                }
            }
            return c == 1;
        }
        static void PrintBoard(int[] board)
        {
            string[] printedBoard = new string[board.Length];
            for (int i = 0; i < board.Length; i++)
            {
                printedBoard[i] = board[i] == (int)Chess.Piece.None ? ((i + i / Chess.rows) % 2 == 0 ? "▓▓" : "░░") : Chess.symbols[board[i]];
            }
            int c = 0;
            string result = "";
            foreach (string item in printedBoard)
            {
                result += item;
                c++;
                if (c % Chess.rows == 0)
                {
                    result += "\n";

                }
            }
            Console.WriteLine(result);
        }
    }
}
