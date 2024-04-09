using System;

class Connect4Board
{
    private const int Rows = 6;
    private const int Cols = 7;

    private char[,] board;

    public Connect4Board()
    {
        board = new char[Rows, Cols];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
            {
                board[i, j] = '#';
            }
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine("  1 2 3 4 5 6 7 ");
        Console.WriteLine("|---------------|");
        for (int i = 0; i < Rows; i++)
        {
            Console.Write("|");
            for (int j = 0; j < Cols; j++)
            {
                Console.Write(" " + board[i, j]);
            }
            Console.WriteLine(" |");
        }
        Console.WriteLine("|---------------|");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Connect4Board board = new Connect4Board();
        board.PrintBoard();
    }
}