# ConsoleChess

ConsoleChess is a console-based version of chess, written in C#.

## Installation

To install ConsoleChess, first clone the repo

```bash
git clone https://github.com/gammer0909/ConsoleChess.git
```

Next, move to the folder containing the repo

```bash
cd ConsoleChess
```

Finally, run the program

```bash
dotnet run
```

and when you want to exit the game, type `exit` when prompted:
```
  A B C D E F G H
8 ♖   ♗ ♕ ♔ ♗ ♘ ♖ 8
7 ♙ ♙   ♙ ♙ ♙ ♙ ♙ 7
6     ♘           6
5     ♙           5
4         ♟       4
3           ♞     3
2 ♟ ♟ ♟ ♟   ♟ ♟ ♟ 2
1 ♜ ♞ ♝ ♛ ♚ ♝   ♜ 1
  A B C D E F G H
5. White > exit
```
## Usage

To use ConsoleChess, simply follow the on-screen instructions.
If you need help, run the program with the --help or -h flag.

```bash
dotnet run --help
```

There are also other flags, those being:

```bash
--help, -h
--no-color, -nc
--no-unicode, -nu
--chess-notation, -cn
--log --l # Logs the game in a .pgn, can be added after --no-color or --no-unicode
```

NOTE:
Currently there is only support for a max of 2 arguments, where the first is --no-color or --no-unicode and the second is --log. I am working on adding support for multiple arguments.

*What's Chess Notation?*
See here: [Chess-Notation.txt](https://github.com/Gammer0909/ConsoleChess/blob/main/source/Chess-Notation.txt)

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Feature Want List

- [ ] Checking if a move is legal
- [ ] Checking if a move puts the player in check
- [ ] Checking if a move puts the player in checkmate
- [ ] Checking if a move puts the player in stalemate
- [ ] Running certain commands for help in-game
- [ ] Castling checking and En passant checking

## License

[MIT](https://choosealicense.com/licenses/mit/)