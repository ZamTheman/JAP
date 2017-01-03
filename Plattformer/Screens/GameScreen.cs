using System;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Plattformer.Managers;

namespace Plattformer.Screens
{
    public class GameScreen
    {
        protected ContentManager content;
        [XmlIgnore]
        public Type Type;

        public string XmlPath;
        public GameScreen()
        {
            Type = this.GetType();
            XmlPath = "Content/" + Type.ToString().Split('.').Last() + ".xml";
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }
        
        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
