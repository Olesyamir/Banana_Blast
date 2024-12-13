using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;

public abstract class Screen
{
    public virtual void Initialize() { }
    public virtual void LoadContent() { }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(GameTime gameTime) { }
    public virtual void UnloadContent() { }

    public void ChangeScreenSize(GraphicsDeviceManager graphics)
    {
        graphics.PreferredBackBufferWidth = 500;
        graphics.PreferredBackBufferHeight = 780;
        graphics.ApplyChanges();
    }
}


