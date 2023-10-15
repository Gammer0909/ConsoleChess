using System;
using ConsoleChess.Abstraction;

class Game {

    private Board board;
    private bool useDebug;
    private bool logMoves;

    public static void Main(string[] args) {
        bool useColor = false;
        bool shouldLog = false;
        // Assume we're not running in debug if it's not specified
        if (args.Length == 0) {
            useColor = false;
        } else if (args[0] == "--no-color" || args[0] == "-nc") {
            useColor = true;
        } else if (args[0] == "--help" || args[0] == "-h") {
            Console.WriteLine("Usage: dotnet run [ -nc --no-color | --help | -cn --chess-notation | -l --log] [-l --log]");
            Environment.Exit(0);
        } else if (args[0] == "--chess-notation" || args[0] == "-cn") {
            // Print the contents of `Chess-Notation.txt
            Console.WriteLine(System.IO.File.ReadAllText("Chess-Notation.txt"));
        } else if (args[0] != null && args.Length == 2) {
            if (args[1] == "--log" || args[1] == "-l") {
                shouldLog = true;
                System.IO.File.Create("source/app/game.pgn");
            }
            else {
                Console.WriteLine("Invalid argument. Use -h or --help for help.");
                Environment.Exit(1);
            }
        } else if (args[0] == "--log" || args[0] == "-l") {
            shouldLog = true;
        }
        else {
            Console.WriteLine("Invalid argument. Use -h or --help for help.");
            Environment.Exit(1);
        }

        // Create a new board
        Board board = new Board();

        // Create a new game
        Game game = new Game(board, useColor, shouldLog);
        game.Start();


    }

    public Game(Board b, bool useDebug, bool logMoves) {
        this.board = b;
        this.useDebug = useDebug;
        this.logMoves = logMoves;
    }

    public void Start() {

        while (!this.board.gameOver) {

            board.DrawBoard(useDebug);
            board.Move(this.logMoves);

        }

    }

}