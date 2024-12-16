using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace jeu_monstre;

// La classe Global gère les variables globales comme _Temps et _spriteBatch, _Content. Elles seront appelés dans Sprite
public class Global
{
    public static float _Temps { get; set; }
    
    public static bool IsPaused { get; set; }
    public static bool IsGame { get; set; }
    public static bool IsMenu { get; set; }
    
    public static bool IsGameOver { get; set; }
    
    public static SpriteBatch _spriteBatch;
    public static ContentManager _Content { get; set; }
    
    public static Game1 _game { get; set; }
    
    public static GraphicsDeviceManager _graphics { get; set; }
    public static ScreenManager _ScreenManager { get; set; }
    public static Screen _GameScreen { get; set; }
    public static void Update(GameTime gameTime)
    {
        _Temps = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public static float _pressTime { get; set; }
    public static float _pressCooldown = 0.40f;
} 