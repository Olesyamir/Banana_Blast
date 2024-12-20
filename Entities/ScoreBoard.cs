using BananaBlast.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaBlast.Entities;

public static class Scoreboard
{
    private static Vector2 position;
    private static int score = 0;   // point actuel, init = 0 
    private static SpriteFont font;

    public static void Init()
    {
        position = new Vector2(2, 5);
        //score = 0;
        font = Global._Content.Load<SpriteFont>("Boxybold");
    }

    public static void setScore(int s)
    {
        score = s;
    }
    public static int getScore()
    {
        return score;
    }

    public static void addScore(int tmp)
    { 
        score += tmp;
    }
    
    public static void resetScore() => score = 0;
    
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        string scoreText = $"Score : {score}";
        spriteBatch.DrawString(font, scoreText, position, Color.GhostWhite);
    }

    public static void Update(GameTime gameTime)
    {
        if (Global._screenState==ScreenState.IsMenu || Global._screenState==ScreenState.IsGameOver)
        {
            resetScore();
        }
    }
}