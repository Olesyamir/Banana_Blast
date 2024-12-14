using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicMonoGame;

public class GameOverScreen : Screen
{
    private string[] GameOverItems = { "Restart", "Quit" };
    private int selectedIndex = 0;
    
    public override void Initialize()
    {
        
    }

    public override void LoadContent()
    {
        
    }

    public override void UnloadContent()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        // Gérer les entrées utilisateur
        Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Global._pressTime>=Global._pressCooldown)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                selectedIndex = (selectedIndex - 1 + GameOverItems.Length) % GameOverItems.Length;

                Global._pressTime = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % GameOverItems.Length;
                Global._pressTime = 0;
            }
            
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            HandleGameOverSelection(gameTime);
        }
        base.Update(gameTime);
    }

    private void HandleGameOverSelection(GameTime gameTime)
    {
        switch (selectedIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    public override void Draw(GameTime gameTime)
    {
        
    }
}