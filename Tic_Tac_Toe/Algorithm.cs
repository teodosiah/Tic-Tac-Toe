using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class Algorithm
    {
        static int[,] winningStates = new int[8, 3]{ { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                                                     { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                                                     { 0, 4, 8 }, { 2, 4, 6 }
                                                   };
        public enum Cell { E = 0, X = 1, O = 2 };

        public Cell[] Board = new Cell[9];
        public Cell turn = Cell.X;
        public int Choice = 0;
        public Cell AI;
        public Cell mySelf;
        public int depth;

        public Algorithm()
        {
            turn = Cell.X;
            mySelf = Cell.X;
            Board = new Cell[9];
        }
        public Algorithm(Player player1, Player player2)
        {
            turn = Cell.X;
            Board = new Cell[9];
            if (player1.Symbol == 'X')
            {
                mySelf = Cell.X;
                AI = Cell.O;
            }
            else
            {
                mySelf = Cell.O;
                AI = Cell.X;
            }
        }
        public void setPlayer(Cell Player)
        {
            this.mySelf = Player;
            this.AI = switchCell(Player);
        }
        static Cell switchCell(Cell Cell)
        {
            if (Cell == Cell.X) return Cell.O;
            else return Cell.X;
        }
        public void MakeMove(int Move, int depth, double alpha, double beta)
        {
            if (turn == mySelf)
            {
                Board = makeBoardMove(Board, turn, Move);
                turn = switchCell(turn);
            }
            if (turn == AI)
            {
                minimax(cloneBoard(Board), turn, depth, alpha, beta);                
                Board = makeBoardMove(Board, turn, Choice);                
                turn = switchCell(turn);
            }
        }
        int minimax(Cell[] InputBoard, Cell Player, int depth, double alpha, double beta)
        {
            Cell[] Board = cloneBoard(InputBoard);

            if (checkScore(Board, AI, depth) != 0)
                return checkScore(Board, AI, depth);
            else if (checkGameEnd(Board)) return 0;

            depth += 1;

            List<int> scores = new List<int>();
            List<int> moves = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (Board[i] == Cell.E)
                {
                    scores.Add(minimax(makeBoardMove(Board, Player, i), switchCell(Player), depth, alpha, beta));
                    moves.Add(i);
                }
            }

            if (Player == AI)
            {
                int MaxScoreIndex = scores.IndexOf(scores.Max());
                Choice = moves[MaxScoreIndex];
                return scores.Max();
            }
            else
            {
                int MinScoreIndex = scores.IndexOf(scores.Min());
                Choice = moves[MinScoreIndex];
                return scores.Min();
            }

        }

        static int checkScore(Cell[] Board, Cell Player, int depth)
        {
            if (checkGameWin(Board, Player)) return 10 - depth;

            else if (checkGameWin(Board, switchCell(Player))) return depth - 10;

            else return 0;
        }

        static bool checkGameWin(Cell[] Board, Cell Player)
        {
            for (int i = 0; i < 8; i++)
            {
                if
                (
                    Board[winningStates[i, 0]] == Player &&
                    Board[winningStates[i, 1]] == Player &&
                    Board[winningStates[i, 2]] == Player
                )
                {
                    return true;
                }
            }
            return false;
        }
        public string showGameSate(Cell[] Board, Cell Player)
        {
            string message = "";

            if (checkGameWin(Board, mySelf) == true)
            {
                message = "You Win!";
            }

            if (checkGameWin(Board, AI) == true)
            {
                message = "PC Wins!";
            }

            if (((checkGameWin(Board, AI) == false) && (checkGameEnd(Board)) == true) || 
                ((checkGameWin(Board, mySelf) == false) && (checkGameEnd(Board)) == true))
            {
                message = "Draw!";
            }

            return message;
        }
        public static bool checkGameEnd(Cell[] Board)
        {
            foreach (Cell p in Board) if (p == Cell.E) return false;
            return true;
        }
        static Cell[] cloneBoard(Cell[] Board)
        {
            Cell[] Clone = new Cell[9];
            for (int i = 0; i < 9; i++)
            {
                Clone[i] = Board[i];
            }

            return Clone;
        }

        static Cell[] makeBoardMove(Cell[] Board, Cell Move, int Position)
        {
            Cell[] newBoard = cloneBoard(Board);
            newBoard[Position] = Move;
            return newBoard;
        }
    }
}
