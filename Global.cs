using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame;

public class Global
{
    public static float _Temps { get; set; }
    
    public static Joueur _joueur { get; set; }

    public static bool IsLoad { get; set; } = false;
    public static ScreenState _screenState { get; set; }
    
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
    public static float _pressCooldown = 0.3f;
}