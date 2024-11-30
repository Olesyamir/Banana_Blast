using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicMonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    
    private Player _ship;//instance de Player
    
    List<Projectile> _projectiles;
    List<Projectile> _killprojectiles;
    
    Timer time = new Timer();
    
    //private Creature _creature;//instance de creature
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
   
        Global._Content = Content;
        CreatureManager.Init();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Global._spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D shipTexture = Content.Load < Texture2D >("ship2") ;
        _ship = new Player( shipTexture , new Vector2 (250 , 720),80 ) ;
        // TODO: use this.Content to load your game content here
        //Texture2D projectileTexture = Content.Load < Texture2D >("missile1") ;
        _projectiles = new List<Projectile>();
        _killprojectiles = new List<Projectile>();
    }

    protected override void Update(GameTime gameTime)
    {
        _graphics.PreferredBackBufferWidth = 500;
        _graphics.PreferredBackBufferHeight = 780;
        _graphics.ApplyChanges();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        
        float x;
        float y;

        
        _ship.Update(gameTime,_projectiles, (Content.Load<Texture2D>("missile1")));
        
        foreach (var p in _projectiles)
        {
            p.Update(gameTime); 
        }

        
        (x, y) = _ship.getPos();
        

        if (x >= Window.ClientBounds.Width - (_ship._Rect.Width / 2) )
        {
            x = Window.ClientBounds.Width - (_ship._Rect.Width / 2);
            _ship.setPos(x, y);
        }

        if (x < (_ship._Rect.Width/2))
        {
            x = (_ship._Rect.Width/2);
            _ship.setPos(x,y);
        }
        CreatureManager.Update(gameTime);
        
        foreach (var s in CreatureManager._Creatures)
        {
            s.Update(gameTime);
            if (s.getPos().Y >= 600)
            {
                s._Health = 0;
            }
            else
            {
                foreach (var p in _projectiles)
                {
                    if (p._Rect.Intersects(s._Rect))
                    {
                        _killprojectiles.Add(p);
                        s._Health = 0;
                    }

                    if (p._Rect.Top < 0)
                    {
                        _killprojectiles.Add(p);
                    }
                }
            }
        }
        
        
        foreach (var p in _killprojectiles)
        {
            _projectiles.Remove(p);
        }
        
        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Global._spriteBatch.Begin(samplerState : SamplerState.PointClamp); 
        _ship.Draw(Global._spriteBatch);
        
        CreatureManager.Draw();
        
        foreach (var p in _projectiles)
        {
            p.Draw(Global._spriteBatch);
        }
        Global._spriteBatch.End();
        base.Draw(gameTime);
    }
}
