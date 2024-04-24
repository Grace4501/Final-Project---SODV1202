using System;

class Connect4Board
{
    private const int Rows = 6;
    private const int Cols = 7;

    private char[,] board;
    private int[] columnCount;

    public Connect4Board()
    {
        board = new char[Rows, Cols];
        columnCount = new int[Cols];
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

    public bool DropSymbol(char symbol, int column)
    {
        if(column < 1 ||  column > Cols)
        {
            Console.WriteLine("Invalid Placment! Pick a number between 1 and 7");
            return false;
        }

        int colIndex = column - 1;

        if (columnCount[colIndex] >= Rows)
        {
            Console.WriteLine("Invalid Placement! Pick another column");
            return false;
        }

        int rowIndex = Rows - columnCount[colIndex] - 1;
        board[rowIndex, colIndex] = symbol;
        columnCount[colIndex]++;

        if (columnCount[colIndex] == Rows)
        {
            Console.WriteLine("Column is full! Pick another column");
        }
        return true;
    }
}

class PlayerAdder
{
    public static (string, string) AddPlayers()
    {
        Console.WriteLine("Enter name for Player 1 (O): ");
        string player1 = Console.ReadLine();
        Console.WriteLine("Enter name for Player 2 (X): ");
        string player2 = Console.ReadLine();
        Console.WriteLine("Players added successfully");
        return (player1, player2);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Connect4Board board = new Connect4Board();
        var (player1, player2) = PlayerAdder.AddPlayers();
        board.PrintBoard();

        char currentPlayer = 'O';
        bool gameOver = false;

        while (!gameOver)
        {
            Console.WriteLine($"{(currentPlayer == 'O' ? player1 : player2)} ({(currentPlayer == 'O' ? "O" : "X")}), enter column number to drop your symbol (between 1 and 7): ");
            int column = int.Parse( Console.ReadLine() );

            if (board.DropSymbol(currentPlayer, column) )
            {
                board.PrintBoard();

                currentPlayer = (currentPlayer == 'O') ? 'X' : 'O';
            }
        }
        Console.WriteLine("Game Over");
    }
}