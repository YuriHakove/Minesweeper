using System.Collections;
using System.Collections.Generic;

public class Grid
{
    // public delegate void InitDelegate();
    // public static event InitDelegate InitEvent;
    public static int w ;
    public static int h ;
    public static Element[,] elements;
    // public static void DoInit()
    // {
    //     InitEvent();
    // }
    public static void UncoverMines()
    {
        foreach (var item in elements)
        {
            if (item.mine)
            { item.LoadTexture(0); }
        }
    }
    public static bool MineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].mine;
        return false;
    }

    public static int AdjacentMines(int x, int y)
    {
        var count = 0;
        if (MineAt(x, y + 1)) ++count; // top
        if (MineAt(x + 1, y + 1)) ++count; // top-right
        if (MineAt(x + 1, y)) ++count; // right
        if (MineAt(x + 1, y - 1)) ++count; // bottom-right
        if (MineAt(x, y - 1)) ++count; // bottom
        if (MineAt(x - 1, y - 1)) ++count; // bottom-left
        if (MineAt(x - 1, y)) ++count; // left
        if (MineAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }

    // Flood Fill empty elements
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        // Coordinates in Range?
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].LoadTexture(AdjacentMines(x, y));

            // close to a mine? then no more work needed here
            if (AdjacentMines(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }
    public static bool isFinished()
    {
        // Try to find a covered element that is no mine
        foreach (Element elem in elements)
            if (elem.isCovered() && !elem.mine)
                return false;
        // There are none => all are mines => game won.
        return true;
    }

}
