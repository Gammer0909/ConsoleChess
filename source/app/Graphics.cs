using System;

namespace ConsoleChess.Graphics;

static class Draw {
    /// <summary>
    /// Draws a pixel with the given foreground and background colors, and a given character.
    /// </summary>
    /// <param name="fg">Foreground color to draw</param>
    /// <param name="bg">background color to draw</param>
    /// <param name="c">Character to put in the pixel</param>
    public static void Pixel(ConsoleColor fg, ConsoleColor bg, char c) {
        Console.ForegroundColor = fg;
        Console.BackgroundColor = bg;
        Console.Write(c);
    }
}