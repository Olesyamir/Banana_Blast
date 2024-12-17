using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jeu_monstre ;

public class ExplosionAnimation
{
    private Texture2D _texture;
    private List<Rectangle> _explosions = new List<Rectangle>();
    private readonly int _frames;
    private int _frame;
    private readonly float _frameTime;
    private float _frameTimeLeft;
    public bool _active = true;

    public ExplosionAnimation(Texture2D texture, int frames, float frameTime)
    {
        _frames = frames;
        _texture = texture;
        _frameTime = frameTime;
        _frameTimeLeft = _frameTime;
        var framewidth = texture.Width / frames;
        var frameheight = texture.Height;
        for (int i = 0; i < _frames; i++)
        {
            _explosions.Add(new Rectangle((i * framewidth), 0, framewidth, frameheight));
        }
        Start();
    }

    public void Stop()
    {
        _active = false;
    }

    public void Start()
    {
        _frame = 0;
        _active = true;
    }

    public void Reset()
    {
        _frame = 0;
        _frameTimeLeft = _frameTime; 
    }

    public void Update(GameTime gameTime)
    {
        if (!_active) return;

        _frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_frameTimeLeft <= 0)
        {
            _frameTimeLeft += _frameTime;
            _frame = (_frame + 1);
        }

        if (_frame >= 6)
        {
            Stop();
        }
    }

    public void Draw(Vector2 pos)
    {
        if (_active)
        {
            Global._spriteBatch.Draw(_texture, pos, _explosions[_frame], Color.White, 0, Vector2.Zero, new Vector2(0.2f,0.2f) ,
                SpriteEffects.None, 0f);
        }
    }
}