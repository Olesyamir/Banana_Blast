using Microsoft.Xna.Framework;

namespace BasicMonoGame.Entities;

public class Moves
{
    public static Vector2 Up(Vector2 pos, float move, Vector2 speed)
    {
        return new Vector2(pos.X, pos.Y - (move-speed.Y));
    }

    public static Vector2 Down(Vector2 pos, float move, Vector2 speed)
    {
        return new Vector2(pos.X, pos.Y + (move+speed.Y));
    }

    public static Vector2 Left(Vector2 pos, float move, Vector2 speed)
    {
        return new Vector2(pos.X - (move-speed.X), pos.Y);
    }

    public static Vector2 Right(Vector2 pos, float move, Vector2 speed)
    {
        return new Vector2(pos.X + (move+speed.X), pos.Y);
    }
    
    public static Vector2 ZigZag(Vector2 position, float move, Vector2 speed)
    {
        float offsetX = (position.Y % 100 < 15) ? speed.X : -speed.X; // Alterne gauche-droite
        return new Vector2(position.X + offsetX, position.Y + move);
    }
    
    public static Vector2 ZigZagReverse(Vector2 position, float move, Vector2 speed)
    {
        float offsetX = (position.Y % 100 < 15) ? -speed.X : +speed.X; // Alterne gauche-droite
        return new Vector2(position.X + offsetX, position.Y + move);
    }
}