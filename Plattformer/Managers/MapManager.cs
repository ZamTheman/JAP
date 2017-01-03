using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Plattformer.Models;
using Plattformer.Utils;

namespace Plattformer.Managers
{
    public class MapManager
    {
        private readonly IReader reader;
        private Map currentMap;

        public MapManager(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            reader = new FileReader(graphicsDevice, contentManager);
            ReadNewMap();
        }

        public void ReadNewMap()
        {
            currentMap = reader.GetNewMap();
        }

        public void DrawMap()
        {
            
        }
    }
}
