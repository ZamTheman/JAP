using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Plattformer.Managers;
using Plattformer.Utils;
using System.Xml.Serialization;

namespace Plattformer.Models
{
    public class Player
    {
        public Image CurrentImage;
        private Image oldImage;
        public Vector2 Velocity;
        public float MoveSpeed;
        private Vector2 position;

        [XmlArray("SpriteSheets")]
        [XmlArrayItem("SpriteSheet")]
        public List<Image> Images { get; set; }
        
        public Player()
        {
            Velocity = Vector2.Zero;
            Images = new List<Image>();
        }
        public void LoadContent()
        {
            foreach (var s in Images)
            {
                s.LoadContent();
            }
            CurrentImage = Images[0];
            CurrentImage.IsActive = true;
        }

        public void UnloadContent()
        {
            foreach (var s in Images)
            {
                s.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyDown(Keys.Down))
                Velocity.Y = MoveSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.Up))
                Velocity.Y = -MoveSpeed*(float) gameTime.ElapsedGameTime.TotalSeconds;
            else
                Velocity.Y = 0;

            if (InputManager.Instance.KeyDown(Keys.Right))
            {
                Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                CurrentImage = ChangeCurrentImage(Images[1]);
            }
                
            else if (InputManager.Instance.KeyDown(Keys.Left))
                Velocity.X = -MoveSpeed*(float) gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                if (CurrentImage != Images[0])
                {
                    CurrentImage = ChangeCurrentImage(Images[0]);
                }
                CurrentImage = Images[0];
                Velocity.X = 0;
            }

            oldImage = CurrentImage;
            position += Velocity;
            CurrentImage.Position = position;
        }

        private Image ChangeCurrentImage(Image newImage)
        {
            oldImage.IsActive = false;
            newImage.IsActive = true;
            return newImage;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentImage.Draw(spriteBatch);
        }
    }
}
