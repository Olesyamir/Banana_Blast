using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BasicMonoGame.Screens;
using BasicMonoGame.Entities;

namespace BasicMonoGame.Managers;


public static class MonstreManager
{
  
    public static List<Monstre> _monstres { get; set; } = new List<Monstre>();
    

    
    
   
    private static float _spawnCooldown;
    
   
    private static float _spawnTime;
    

    private static int _padding;

    private static Random random;
    


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
        if (creature == TypeMonstre.Bigboss)
        {
            return 4;
        }

        return 1;
    }

    private static Vector2 Position(TypeMonstre type,Texture2D texture)
    {
        _padding = texture.Width / (widthdivisor(type) * 2);

        random = new Random();
        int x = random.Next(_padding, 500 - _padding); // Exemple de largeur 800
        Vector2 position = new Vector2(x,-30);
        return position;
    }
    
    private static void AddMonstre()
    {
        Texture2D texture;
        List<int> liste = [0, 0, 0, 0, 0, 0, 0, 0, 0, 1];
        List<int> posit = [5, 700];
        random = new Random();
        int i = random.Next(0, liste.Count);
        switch (liste[i])
        {
            case 0:
                texture = Global._Content.Load<Texture2D>("enemy2");
                _monstres.Add(new Monstre(TypeMonstre.Petit,texture ,Position((TypeMonstre)liste[i],texture),new Vector2(80,74)));
                break;
            case 1:
                texture = Global._Content.Load<Texture2D>("NpcWeed");
                _monstres.Add(new Monstre(TypeMonstre.Bigboss,texture ,new Vector2(posit[random.Next(0,1)],-10),new Vector2(118,66)));
                break;

        }
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