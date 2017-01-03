using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Plattformer.Models
{
    public class Map : IMap
    {
        private List<List<char>> cells;
        public List<List<char>> Cells {
            get { return cells; }
            set { cells = value; }
        }

        private Dictionary<char, int[]> tileMapping;
        public Dictionary<char, int[]> TileMapping
        {
            get { return tileMapping;}
            private set { tileMapping = value; }
        }
        private Texture2D mapTiles;
        public Texture2D MapTiles
        {
            get { return mapTiles;}
            set { mapTiles = value; }
        }

        public Map(List<List<char>> cells, Dictionary<char, int[]> tileMapping, Texture2D mapTiles)
        {
            Cells = cells;
            TileMapping = tileMapping;
            MapTiles = mapTiles;
        }
    }
}
