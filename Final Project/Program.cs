using System;

class Connect4Board
{
    public const int Rows = 6;
    private const int Cols = 7;

    private char[,] board;
    public int[] columnCount;

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
            for (int j = 0; j < Cols; j++)
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
        if (column < 1 || column > Cols)
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

        return true;
    }


    public bool CheckWin(char symbol, int row, int col)
    {
        // Check horizontal win
        int count = 0;
        for (int j = 0; j < Cols; j++)
        {
            if (board[row, j] == symbol)
            {
                count++;
                if (count == 4) return true;
            }
            else
            {
                count = 0;
            }
        }

        // Check vertical win
        count = 0;
        for (int i = 0; i < Rows; i++)
        {
            if (board[i, col] == symbol)
            {
                count++;
                if (count == 4) return true;
            }
            else
            {
                count = 0;
            }
        }

        // Check diagonal win (top-left to bottom-right)
        for (int i = 0; i <= Rows - 4; i++)
        {
            for (int j = 0; j <= Cols - 4; j++)
            {
                if (board[i, j] == symbol && board[i + 1, j + 1] == symbol && board[i + 2, j + 2] == symbol && board[i + 3, j + 3] == symbol)
                {
                    return true;
                }
            }
        }

        // Check diagonal win (top-right to bottom-left)
        for (int i = 0; i <= Rows - 4; i++)
        {
            for (int j = Cols - 1; j >= 3; j--)
            {
                if (board[i, j] == symbol && board[i + 1, j - 1] == symbol && board[i + 2, j - 2] == symbol && board[i + 3, j - 3] == symbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsBoardFull()
    {
        for (int i = 0; i < Cols; i++)
        {
            if (columnCount[i] < Rows)
            {
                return false;
            }
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

class Game
{
    public void Start()
    {
        bool restart = true;
        while (restart)
        {
            restart = PlayConnect4();
        }
        Console.WriteLine("Game Over");
    }

    static bool PlayConnect4()
    {
        Connect4Board board = new Connect4Board();
        var (player1, player2) = PlayerAdder.AddPlayers();
        board.PrintBoard();

        char currentPlayer = 'O';

        while (true)
        {
            Console.WriteLine($"{(currentPlayer == 'O' ? player1 : player2)} ({(currentPlayer == 'O' ? "O" : "X")}), enter column number to drop your symbol (between 1 and 7): ");
            int column;
            while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
            {
                Console.WriteLine("Invalid column number. Please enter a number between 1 and 7.");
            }

            if (board.DropSymbol(currentPlayer, column))
            {
                board.PrintBoard();

                // Calculate the row index based on the number of symbols in the column
                int rowIndex = Connect4Board.Rows - board.columnCount[column - 1];

                if (board.CheckWin(currentPlayer, rowIndex, column - 1))
                {
                    Console.WriteLine($"Player {(currentPlayer == 'O' ? player1 : player2)} wins! Restart? Yes(1) No(2): ");
                    string restart = Console.ReadLine();
                    return restart == "1";
                }
                else
                {
                    if (board.IsBoardFull())
                    {
                        Console.WriteLine("The board is full! It's a draw.");
                        return false;
                    }
                    currentPlayer = (currentPlayer == 'O') ? 'X' : 'O';
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}