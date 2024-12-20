using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using BasicMonoGame.Screens;
using BasicMonoGame.Entities;
using BasicMonoGame.Managers;

namespace BasicMonoGame;

public static class Global
{
    public static String date=((DateTime.Today).ToShortDateString());
    public static String _dateserialisation=(date.Substring(6,4))+"-"+(date.Substring(3,2))+"-"+(date.Substring(0,2));
    
    public static bool IsEntree = false;

    public static bool IsSaved = false;
    
    

    public static Joueur _joueur { get; set; }


    public static bool IsLoad { get; set; } = false;
    

    public static ScreenState _screenState { get; set; }
    
 
    public static SpriteBatch _spriteBatch;
    

    public static ContentManager _Content { get; set; }
    

    public static Game1 _game { get; set; }
    

    public static GraphicsDeviceManager _graphics { get; set; }
    

    public static ScreenManager _ScreenManager { get; set; }
    

    public static Screen _GameScreen { get; set; }


    public static float _pressTime { get; set; }
    public static float _pressCooldown = 0.3f;
} 