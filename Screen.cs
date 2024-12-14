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

    public void ChangeScreenSize(GraphicsDeviceManager graphics,int width,int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }
}


