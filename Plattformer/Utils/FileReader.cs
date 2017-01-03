using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Plattformer.Models;

namespace Plattformer.Utils
{
    public class FileReader : IReader
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        public string FolderPath { get; set; }
        public string FileName { get; set; }

        public FileReader(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
        }

        public Map GetNewMap()
        {
            var cells = ReadMapFromFile();
            var tileMapping = MapTilesFromFile();
            var tileImage = LoadTileImage();
            return new Map(cells, tileMapping, tileImage);
        }

        private Texture2D LoadTileImage()
        {
            var tileImage = new Texture2D(graphicsDevice, 320, 320);
            tileImage = content.Load<Texture2D>("Maps/Map01Tiles");
            return tileImage;
        }

        private Dictionary<char, int[]> MapTilesFromFile()
        {
            try
            {
                var sr = new StreamReader(content.RootDirectory + "Content/MapData/Map01TileMapping.txt");
                var mapping = new Dictionary<char, int[]>();
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        var tempArray = sr.ReadLine().Split('=');
                        var tempCoordinates = tempArray[1].Split(',');
                        mapping.Add(tempArray[0][0], new []{int.Parse(tempCoordinates[0]), int.Parse(tempCoordinates[1])});
                    }
                }
                return mapping;
            }

            catch
            {
                return null;
            }
        }

        private List<List<char>> ReadMapFromFile()
        {
            try
            {
                var currentDirectory = Environment.CurrentDirectory;
                var sr = new StreamReader("Content/MapData/Map01.txt");
                var cells = new List<List<char>>();
                using (sr)
                {
                    int counter = 0;
                    while (!sr.EndOfStream)
                    {
                        var tempArray = sr.ReadLine().Split(',');
                        var rowList = new List<char>();
                        foreach (var character in tempArray)
                        {
                            rowList.Add(character[0]);
                        }
                        cells.Add(rowList);
                        counter++;
                    }
                }
                return cells;
            }

            catch
            {
                return null;
            }
        }
    }
}
