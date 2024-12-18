using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame;


public static class MonstreManager
{
  
    public static List<Monstre> _monstres { get; set; } = new List<Monstre>();
    

    private static Texture2D _texture;
    
   
    private static float _spawnCooldown;
    
   
    private static float _spawnTime;
    

    private static int _padding;
    


    public static Monstres GetMonstres()
    {
        return new Monstres { ListeMonstres = _monstres };
    }
    
    public static void SetMonstres(Monstres liste)
    {
        _monstres = liste.ListeMonstres ?? new List<Monstre>();
    }

    public static void Init()
    {
        // Assurez-vous que Globals.Content est correctement défini dans votre projet
        _texture = Global._Content.Load<Texture2D>("enemy2");
        _spawnCooldown = 2f;
        _spawnTime = _spawnCooldown;

        // Calcule le padding en fonction de la taille de la texture
        
    }
    public static int widthdivisor(TypeMonstre creature)
    {
        if (creature == TypeMonstre.Petit)
        {
            return 5;
        }
        else if (creature == TypeMonstre.Bigboss)
        {
            return 12;
        }

        return 1;
    }

    private static Vector2 Position(TypeMonstre type)
    {
        _padding = _texture.Width / (widthdivisor(type) * 2);
        Random random = new Random();
        int x = random.Next(_padding, 500 - _padding); // Exemple de largeur 800
        Vector2 position = new Vector2(x,-40);
        return position;
    }
    
    private static void AddMonstre()
    {
        _monstres.Add(new Monstre(TypeMonstre.Petit,_texture ,Position(TypeMonstre.Petit),60));
        //_Creatures.Add(new Creature(TypeMonstre.Petit,_texture ,new Vector2(0,0),60));
    }

    public static void Update(GameTime gameTime)
    {
        if (Global._screenState==ScreenState.IsGame)
        {
            _spawnTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            _monstres.RemoveAll((c) => c.getHealth() <= 0);
            _monstres.Find((c) => c.getHealth() <= 0)?._animation.Stop();
            while (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                AddMonstre();
            }

            foreach (var c in _monstres)
            {
                c.Update(gameTime);
            }
        }
        if (Global._screenState== ScreenState.IsMenu || Global._screenState == ScreenState.IsGameOver)
        {
            _monstres.Clear();
        }
    }
    
    public static void Draw()
    {
        if (Global._screenState ==ScreenState.IsGame)
        {
            foreach (var c in _monstres)
            {
                c.Draw();
            }
        }
    }
}