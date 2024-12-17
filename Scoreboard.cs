using System.Net.Mime;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using jeu_monstre;
using Microsoft.Xna.Framework.Content;


namespace jeu_monstre;

public static class Scoreboard
{
    private static Vector2 position;
    private static int score = 0;   // point actuel, init = 0 
    private static SpriteFont font;

    public static void Init()
    {
        position = new Vector2(0, 0);
        //score = 0;
        font = Global._Content.Load<SpriteFont>("Roboto");
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
        if (Global.IsMenu || Global.IsGameOver)
        {
            resetScore();
        }
        else
        {
            
        }
    }
}