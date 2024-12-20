using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BasicMonoGame.Managers;

using BasicMonoGame.Entities;
namespace BasicMonoGame.Screens;

public class InitializeScreen : Screen
{
   // 
    private string _text = "";
    private string _text2 = "";
    private Rectangle _textBoxRectangle;
    private Rectangle _textBoxRectangle2;
    private bool _isActive = false;
    private bool _isActive2 = false;
    private SpriteFont font;
    private Color _textColor = Color.Black;
    private Texture2D _backgroundTexture;
    private Color _backgroundColor ;
    private float _textInput = 0.4f;//temps entre chaque entrée
    private int longueurmax = 15;//length max du nom 
    private Joueur _joueur;
    

    public override void Initialize()
    {
        //Initialisation de l'ecran et des deux textbox
        Global._screenState = ScreenState.IsInitialize;
        _textBoxRectangle = new Rectangle(100, 100, 300, 50);
        _textBoxRectangle2 = new Rectangle(100, 200, 300, 50);
    }

    public override void LoadContent()
    {
        //load le joueur qui va être passé en parametre au prochain ecran pour que le score soit enregistré pour le joueur actuel
        Texture2D shipTexture = Global._game.Content.Load < Texture2D >("ship2") ;
        _joueur = new Joueur( shipTexture , new Vector2 (250 , 720),new Vector2(100,100) ) ;
        font = Global._Content.Load<SpriteFont>("outerspace");
        _backgroundTexture = new Texture2D(Global._game.GraphicsDevice, 1, 1);
        _backgroundTexture.SetData(new[] { Color.White });
    }

    public override void UnloadContent()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        if (Global._screenState==ScreenState.IsInitialize)
        {
            var keyboardState = Keyboard.GetState();

            // Si l'utilisateur clique dans une textbox, active l'édition
            if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                _textBoxRectangle2.Contains(Mouse.GetState().Position))
            {
                _isActive2 = true;
                _isActive = false;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                _textBoxRectangle.Contains(Mouse.GetState().Position))
            {
                _isActive = true;
                _isActive2 = false;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _isActive = false;
                _isActive2 = false;
            }
            //ajoute a pressTime le temps passé, si superieur au temps necessaire entre deux entrees alors ecrit
            Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Global._pressTime >= _textInput)
            {
                foreach (var key in keyboardState.GetPressedKeys())
                {
                    if (key == Keys.Back && _text.Length > 0)
                    {
                        if (_isActive)
                        {
                            _text = _text[..^1]; // Supprime le dernier caractère
                        }

                        if (_isActive2)
                        {
                            _text2 = _text2[..^1];
                        }

                        Global._pressTime = 0;
                    }
                    else if ( key == Keys.Space)
                    {
                        if (_isActive && _text.Length < 14){
                            _text += " "; //ajoute espace sauf quand on depasse la longueur de nom possible
                        }

                        if (_isActive2 && _text2.Length < 2)
                        {
                            _text2 += " ";
                        }
                        Global._pressTime = 0;
                    }
                    else if (_text.Length < 13 && key.ToString().Length == 1 && _isActive) // pour le nom si entree est une lettre
                    {
                        _text += key.ToString();
                        Global._pressTime = 0;
                    }
                    else if (_isActive2 && _text2.Length < 2 && (key.ToString().Length==7 && (char.IsDigit(key.ToString(), 6))
                                                                 ||(key >= Keys.D0 && key <= Keys.D9)))//pour age si entree est un chiffre
                    {
                        _text2 += key.ToString()[^1];//dernier char de "Numpad[X]" avec X entre 0 et 9
                        Global._pressTime = 0;
                    }
                    Global._pressTime = 0;//pressTime a pour empecher le spam 
                }
            }
            else{//Sinon quand on appuie sur Entree
                Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds; 
                if (Global._pressTime >= 0.1f)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) & _text.Length > 0 && _text2.Length >0)
                    {
                        //On nomme du joueur et on lance la partie 
                        _joueur.setName(_text);
                        _joueur.setAge(int.Parse(_text2));
                        Global._joueur = _joueur;
                        XMLManager<InGameScreen> GameDeserializer = new XMLManager<InGameScreen>();
                        Global._ScreenManager.ChangeScreen(new InGameScreen(_joueur));
                        //Code pour initialiser joueur et pour mettre à jour le scoreboard et profil si score actuelle > meilleur score
                        //a la fin on met presstime = 0
                        Global._pressTime = 0;
                    }
                }
            }
        }

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        // Dessiner le fond des TextBox
        Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
        _backgroundColor = (_isActive) ? Color.White : Color.Gray ;
        Global._spriteBatch.Draw(
            _backgroundTexture,
            _textBoxRectangle,
            _backgroundColor
        );
        Color _backgroundColor2 = _isActive2 ? Color.White : Color.Gray ;
        Global._spriteBatch.Draw(
            _backgroundTexture,
            _textBoxRectangle2,
            _backgroundColor2
        );
        
        // Dessiner les textes
        var textPosition = new Vector2(
            _textBoxRectangle.X + 5,
            _textBoxRectangle.Y + (_textBoxRectangle.Height - font.LineSpacing) / 2
        );
        var textPosition2 = new Vector2(
            _textBoxRectangle2.X + 5,
            _textBoxRectangle2.Y + (_textBoxRectangle2.Height - font.LineSpacing) / 2
        );
        Global._spriteBatch.DrawString(font, _text, textPosition, _textColor);
        Global._spriteBatch.DrawString(font, "Nom du joueur",new Vector2(textPosition.X,textPosition.Y+40), Color.Gold);
        Global._spriteBatch.DrawString(font, _text2, textPosition2, _textColor);
        Global._spriteBatch.DrawString(font, "Age",new Vector2(textPosition2.X,textPosition2.Y+40), Color.Gold);

        base.Draw(gameTime);
    }
}