using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaBlast.Entities;

public class Sprite {
    
    [XmlIgnore]
    protected Texture2D _texture;
  
    [XmlIgnore]
    protected Vector2 _position;
  
    [XmlIgnore]
    private int _size = 100;
  
    [XmlIgnore]
    private static readonly int _sizeMin = 10;
  
    [XmlIgnore]
    private Color _color = Color . White;
    
    [XmlIgnore]
    protected Vector2 _dim;

    [XmlIgnore]
    public Rectangle _Rect
    {
        get => new Rectangle((int)_position.X, (int)_position.Y, (int)_dim.X, (int)_dim.Y);
    }
    
    [XmlIgnore]
    public Texture2D _Texture { get => _texture;set => _texture = value; }
    
    [XmlIgnore]

    public int _Size { get => _size; set => _size = value >= _sizeMin?value:_sizeMin; }



    public Sprite(Texture2D texture, Vector2 dim)
    {
        _texture = texture;
        _dim = dim;
    }
    
    public void Draw ( SpriteBatch spriteBatch) {
            var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
            spriteBatch.Draw(_Texture, // Texture2D ,
                _Rect, // Rectangle destinationRectangle ,
                null, // Nullable < Rectangle > sourceRectangle ,
                _color, // Color ,
                0.0f, // float rotation ,
                origin, // Vector2 origin ,
                SpriteEffects.None, // SpriteEffects effects ,
                0f); // float layerDepth
        
    }
}