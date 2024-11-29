using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
using Microsoft. Xna. Framework. Input ;

namespace BasicMonoGame;

public class GameObject : Sprite
{
    protected Vector2 _speed;
    public GameObject(Texture2D texture, Vector2 position, int size) : base(texture,size )
    {
        _position = position;
    }
   
    public Vector2 getPos()
    {
        return this._position;
    }
    
    public void setPos(float x, float y)
    {
        this._position.X = x;
        this._position.Y = y;
    }
    
    public void setSpeedX(float x)
    {
        _speed.X = x;

    }

    public void setSpeedY(float y){
        _speed.Y = y;
    }
}