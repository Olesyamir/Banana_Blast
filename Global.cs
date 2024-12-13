using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame;

public class Global
{
    public static float _Temps { get; set; }
    
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
}