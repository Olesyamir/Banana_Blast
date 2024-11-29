using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicMonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _ship;//instance de Player
    
    List<Creature> _sprites;
    List<Creature> _killcreatures;
    List<Projectile> _projectiles;
    
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
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D shipTexture = Content.Load < Texture2D >("ship2") ;
        _ship = new Player( shipTexture , new Vector2 (250 , 720),80 ) ;
        // TODO: use this.Content to load your game content here
        _sprites = new List<Creature>();
        Texture2D creatureTexture = Content.Load < Texture2D >("virus1") ;
        _sprites.Add(new Creature(TypeCreature.Petit,creatureTexture, new Vector2 (250 , 50),100 ));
        //Texture2D projectileTexture = Content.Load < Texture2D >("missile1") ;
        _projectiles = new List<Projectile>();
        _killcreatures = new List<Creature>();
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
        (x, y) = _ship.getPos();
        
        foreach (var p in _projectiles)
        {
            p.Update(gameTime); 
        }


        
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
        
        foreach (var s in _sprites)
        {
            s.Update(gameTime);
            if (s.getPos().Y >= 600)
            {
                _killcreatures.Add(s);
            }
            else
            {
                foreach (var p in _projectiles)
                {
                    if (p._Rect.Intersects(s._Rect))
                    {
                        _killcreatures.Add(s);
                    }
                }
            }
        }

        foreach (var m in _killcreatures)
        {
            _sprites.Remove(m);
        }
        
        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState : SamplerState.PointClamp); 
        _ship.Draw(_spriteBatch);
        foreach (var s in _sprites)
        {
            s.Draw(_spriteBatch);
        }
        foreach (var p in _projectiles)
        {
            p.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
