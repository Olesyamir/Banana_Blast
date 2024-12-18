using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame;

public class Game1 : Game
{
    public GraphicsDeviceManager _graphics;
    
    
    //private Creature _creature;//instance de creature
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Global._graphics=_graphics;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Global._game = this;
        Global._GameScreen = new MainMenuScreen();
        Global._ScreenManager= new ScreenManager();
        Global._Content = Content;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Global._spriteBatch = new SpriteBatch(GraphicsDevice);
        Global._ScreenManager.ChangeScreen(Global._GameScreen);
    }

    protected override void Update(GameTime gameTime)
    {
        
        Global._ScreenManager.Update(gameTime);
        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //drawing code
        Global._spriteBatch.Begin(samplerState : SamplerState.PointClamp);
        Global._ScreenManager.Draw(gameTime);
        Global._spriteBatch.End();
        base.Draw(gameTime);
    }
}