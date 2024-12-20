using System.Collections.Generic;
using BananaBlast.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaBlast.Animations;

public class Animation
{
    private Texture2D _texture;
    private List<Rectangle> _rect = new List<Rectangle>();
    protected readonly int _frames;
    protected int _frame;
    protected readonly float _frameTime;
    protected float _frameTimeLeft;
    private int _ligne;
    private int _nbligne;
    public bool _active = true;
    private bool _loop;
    private int framewidth;
    private int frameheight;

    public Animation(Texture2D texture,int line ,int frames, float frameTime,bool loop)
    {
        _loop = loop;
        _frames = frames;
        _texture = texture;
        _frameTime = frameTime;
        _frameTimeLeft = _frameTime;
        framewidth = texture.Width / frames;
        frameheight = texture.Height/line;
        _nbligne = line;
        for (int j = 0; j < line; j++)
        {
            for (int i = 0; i < _frames; i++)
            {
                //ici un rectangle est une vue de dimension framewidth x frameheight a partir du bord superieur gauche sur la texture (spritesheet) ((colonne_courant * framewidth),(frameheight * ligne_courant))
                _rect.Add(new Rectangle((i * framewidth), (frameheight * j), framewidth, frameheight));
            }
        }
        //commence l'animation
        Start();
    }

    public void Stop()
    {
        _active = false;
    }

    public void Start()
    {
        _ligne = 0;
        _frame = 0;
        _active = true;
    }

    public void Reset()
    {
        _ligne = 0;
        _frame = 0;
        _frameTimeLeft = _frameTime; 
    }

    public void Update(GameTime gameTime)
    {
        if (!_active) return;
        //si _active alors on met a jour les frame courant et la ligne courante 
        
        //soustrait le temps restant pour la frame avec le temps écoulés depuis l'affichage de la frame courante
        _frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        //_frameTimeLeft<=0 ssi le temps d'affichage de la frame depasse ou egale a celle qui a ete donnee
        if (_frameTimeLeft <= 0)
        {
            //on rajoute alors du temps pour pouvoir laisser du temps a la nouvelle frame sur l'ecran
            _frameTimeLeft += _frameTime;
            if (!_loop)
            {
                if (_ligne==_nbligne && _frame == _frames-1)//stop quand on est a la dernière ligne et a la derniere frame
                {
                    Stop();
                }
                _frame = (_frame + 1)%_frames;
                if (_frame == _frames-1) _ligne = (_ligne + 1);
                
            }
            else
            {
                //si boucle alors on ne s'arrete pas on revients à _frame = 0 et ligne =0 tant que Stop() n'est pas appelé
                _frame = (_frame+1) % _frames;
                if (_frame == _frames-1) _ligne = (_ligne + 1) % _nbligne;
            }
        }
    }
    
    public void Draw(Vector2 pos,TypeMonstre monstre)
    {
        if (_active)
        {
            float a;
            float b;
            if (monstre == TypeMonstre.Petit)
            {
                a = 0.4f;
                b = 0.5f;
            }
            else
            {
                a=2.11f;
                b=2.46f;
            }
            var scale = new Vector2(a,b);
            //on affiche les rectangles dans l'ordre d'indice pour faire l'animation
            Global._spriteBatch.Draw(_texture, pos, _rect[_frame+ (_frames * _ligne)], Color.White, 0, Vector2.Zero, scale ,
                SpriteEffects.None, 0f);
        }
    }
}