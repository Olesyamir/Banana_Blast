using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
using Microsoft. Xna. Framework. Input ;

namespace BasicMonoGame.Entities;

public class GameObject : Sprite
{
    protected Vector2 _speed;

    public GameObject(Texture2D texture, Vector2 position,Vector2 dim) :base(texture,dim)
    {
        _position = position;
    }
   
    public Vector2 getPos()
    {
        return _position;
    }
    
    public void setPos(float x, float y)
    {
        _position.X = x;
        _position.Y = y;
    }
    
    public void setSpeedX(float x)
    {
        _speed.X = x;

    }

    public void setSpeedY(float y){
        _speed.Y = y;
    }
}