using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using jeu_monstre;
using Microsoft.Xna.Framework.Content;


namespace jeu_monstre;

public class Scoreboard: GameObject
{
    private int score;   // point actuel
    private SpriteFont font;
    
    public Scoreboard(Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        score = 0;
    }

    public void addScore(int score)
    {
        this.score += score;
    }
    
    public int getScore() => score;
    public void resetScore() => score = 0;

    public void LoadContent(ContentManager content, string fontpath)
    {
        font = content.Load<SpriteFont>(fontpath);
    } 
    
    public void Draw(SpriteBatch spriteBatch)
    {
        string scoreText = $"Score : {score}";
        spriteBatch.DrawString(font, scoreText, _position, Color.GhostWhite);
    }
    
}