using Microsoft.Xna.Framework;

namespace BananaBlast.Screens;

public abstract class Screen
{
    public virtual void Initialize() { }
    public virtual void LoadContent() { }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(GameTime gameTime) { }
    public virtual void UnloadContent() { }

    public void ChangeScreenSize(GraphicsDeviceManager graphics,int width,int height)
    {
        //permet de changer les dimensions de la fenêtre
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }
}


