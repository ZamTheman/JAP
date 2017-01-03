using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Plattformer.Screens;
using Plattformer.Utils.ImageEffects;

namespace Plattformer.Utils
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path, Name;
        public Vector2 Position, Scale;
        public Rectangle SourceRectangle;
        public bool IsActive;
        
        [XmlIgnore]
        public Texture2D Texture;
        private Vector2 origin;
        private ContentManager content;
        private RenderTarget2D renderTarget;
        private SpriteFont font;
        private Dictionary<string, ImageEffect> effectList;
        public string Effects;

        public int AnimationSpeed;
        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;

        public int NrColumns, NrRows, NrFrames;

        public Image()
        {
            Path = Text = Effects = Name = string.Empty;
            FontName = "SpriteFonts/Arial";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRectangle = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void StoreEffects()
        {
            Effects = string.Empty;
            foreach (var imageEffect in effectList)
            {
                if (imageEffect.Value.IsActive)
                    Effects += imageEffect.Key + ":";
            }
            if(Effects != string.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach (var imageEffect in effectList)
            {
                if (imageEffect.Value.IsActive)
                    DeactivateEffect(imageEffect.Key);
            }

            string[] split = Effects.Split(':');
            foreach (var s in split)
                ActiveEffect(s);
        }

        private void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T) Activator.CreateInstance(typeof(T));

            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            var type = effect.GetType().ToString().Split('.');
            effectList.Add(type.Last(), (effect as ImageEffect));
        }

        public void ActiveEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            if (Path != string.Empty)
                Texture = content.Load<Texture2D>(Path);

            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            }
            else
                dimensions.Y = font.MeasureString(Text).Y;

            dimensions.X += font.MeasureString(Text).X;
            
            if (SourceRectangle == Rectangle.Empty)
                SourceRectangle = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if(Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);


            if (Effects != string.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (var item in split)
                {
                    ActiveEffect(item);
                }
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
            {
                DeactivateEffect(effect.Key);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                {
                    effect.Value.Update(gameTime);
                }
                    
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            spriteBatch.Draw(Texture, Position + origin, SourceRectangle, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }

    }
}
