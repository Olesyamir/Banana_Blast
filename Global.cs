using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame;

public class Global
{
    public static String date=((DateTime.Today).ToShortDateString());
    public static String _dateserialisation=(date.Substring(6,4))+"-"+(date.Substring(3,2))+"-"+(date.Substring(0,2));
    
    public static bool IsEntree = false;

   // [XmlIgnore] 
    public static float _Temps { get; set; }
    
   // [XmlIgnore] 
    public static Joueur _joueur { get; set; }

  //  [XmlIgnore] 
    public static bool IsLoad { get; set; } = false;
    
   // [XmlIgnore] 
    public static ScreenState _screenState { get; set; }
    
   // [XmlIgnore] 
    public static SpriteBatch _spriteBatch;
    
  //  [XmlIgnore] 
    public static ContentManager _Content { get; set; }
    
  //  [XmlIgnore] 
    public static Game1 _game { get; set; }
    
   // [XmlIgnore] 
    public static GraphicsDeviceManager _graphics { get; set; }
    
  //  [XmlIgnore] 
    public static ScreenManager _ScreenManager { get; set; }
    
 //   [XmlIgnore] 
    public static Screen _GameScreen { get; set; }
    public static void Update(GameTime gameTime)
    {
        _Temps = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public static float _pressTime { get; set; }
    public static float _pressCooldown = 0.3f;
} 