using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace jeu_monstre;

// La classe Global gère les variables globales comme _Temps et _spriteBatch, _Content. Elles seront appelés dans Sprite
public class Global
{
    public static float _Temps { get; set; }
    public static SpriteBatch _spriteBatch;
    public static ContentManager _Content { get; set; }


    public static void Update(GameTime gameTime)
    {
        _Temps = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}