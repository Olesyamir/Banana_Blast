using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jeu_monstre;

public class BackgroundManager
{
    private static List<Background> Backgrounds { get; set; } = new();
    private Texture2D bgTexture;
    private int bgsize=1560;
    public void Initialize(Texture2D texture)
    {
        bgTexture = texture;
        Backgrounds.Add(new Background(bgTexture,Vector2.Zero,bgsize));
        Backgrounds.Add(new Background(bgTexture,new Vector2(0,-bgsize),bgsize));
    }

    public void Update(GameTime gameTime)
    {
        foreach (var bg in Backgrounds)
        {
            bg.Update(gameTime);
        }
        
        if (Backgrounds.Count > 0 && Backgrounds[^1]._Rect.Top >= 0)
        {
            var lastBackground = Backgrounds[^1];
            Backgrounds.Add(new Background(bgTexture,
                new Vector2(0, lastBackground._Rect.Top-bgsize), bgsize));
            Backgrounds.RemoveAll(bg => (bg._Rect.Top) >= 780);
            
        }
    }

    public void Draw(GameTime gameTime)
    {
        foreach (var bg in Backgrounds)
        {
            bg.Draw(gameTime);
        }
    }
}