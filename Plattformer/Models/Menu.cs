using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Plattformer.Managers;
using Plattformer.Screens;

namespace Plattformer.Models
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;

        private int itemNumber;
        public int ItemNumber
        {
            get { return itemNumber; }
        }
        
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        public Menu()
        {
            id = string.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void Transition(float alpha)
        {
            foreach (var menuItem in Items)
            {
                menuItem.Image.IsActive = true;
                menuItem.Image.Alpha = alpha;
                if (alpha == 0.0f)
                    menuItem.Image.FadeEffect.Increase = true;
                else
                    menuItem.Image.FadeEffect.Increase = false;
            }
        }

        private void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (var item in Items)
                dimensions += new Vector2(item.Image.SourceRectangle.Width, item.Image.SourceRectangle.Height);
            
            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2, (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);

            foreach (var item in Items)
            {
                if(Axis == "X")
                    item.Image.Position = new Vector2(dimensions.X, (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRectangle.Height) / 2);

                else if(Axis == "Y")
                    item.Image.Position = new Vector2((ScreenManager.Instance.Dimensions.X - item.Image.SourceRectangle.Width) / 2, dimensions.Y);

                dimensions += new Vector2(item.Image.SourceRectangle.Width, item.Image.SourceRectangle.Height);
            }

        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach (var menuItem in Items)
            {
                menuItem.Image.LoadContent();
                foreach (var s in split)
                    menuItem.Image.ActiveEffect(s);
            }
            AlignMenuItems();
        }

        public void UnloadContent()
        {
            foreach (var menuItem in Items)
            {
                menuItem.Image.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    itemNumber--;
            }

            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Up))
                    itemNumber--;
                else if (InputManager.Instance.KeyPressed(Keys.Down))
                    itemNumber++;
            }

            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > Items.Count - 1)
                itemNumber = Items.Count - 1;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                    Items[i].Image.IsActive = true;
                else
                    Items[i].Image.IsActive = false;
                
                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var menuItem in Items)
            {
                menuItem.Image.Draw(spriteBatch);
            }
        }

    }
}
