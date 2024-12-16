using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jeu_monstre.Virus;

public static class CreatureManager
{
    public static List<Creature> _Creatures { get; } = new List<Creature>();   // liste des virus aléatoires
    private static Texture2D _texture;
    private static float _spawnCooldown;
    private static float _spawnTime;
    private static int _padding;

    public static void Init()
    {
        // Assurez-vous que Globals.Content est correctement défini dans votre projet
        _texture = Global._Content.Load<Texture2D>("virus1");
        _padding = _texture.Width;
        _spawnCooldown = 2f;
        _spawnTime = _spawnCooldown;

        // Calcule le padding en fonction de la taille de la texture
        _padding = _texture.Width / 2;
    }

    public static Vector2 Position()
    {
        Random random = new Random();
        int x = random.Next(_padding, 500 - _padding); // Exemple de largeur 800
        Vector2 position = new Vector2(x,50);
        return position;
    }
    
    public static void AddCreature()
    {
        _Creatures.Add(new Creature(TypeCreature.Petit,_texture ,Position(),60));
    }

    public static void Update(GameTime gameTime)
    {
        _spawnTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        while(_spawnTime <= 0)
        {
            _spawnTime += _spawnCooldown;
            AddCreature();
        }

        foreach (var c in _Creatures)
        {
            c.Update(gameTime);
        }
        _Creatures.RemoveAll((c) => c._Health <= 0);
    }
    
    public static void Draw()
    {
        foreach (var c in _Creatures)
        {
            c.Draw(Global._spriteBatch);
        }
    }
}