using Microsoft.Xna.Framework;

namespace Plattformer.Utils.ImageEffects
{
    public class SpriteSheetEffect : ImageEffect
    {
        private int frameCounter;
        private int switchFrame;
        private Vector2 currentFrame;
        private int currentFrameNumber;

        public SpriteSheetEffect()
        {
            currentFrame = new Vector2(0, 0);
            switchFrame = 100;
            frameCounter = 0;
        }
        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.IsActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameCounter >= image.AnimationSpeed)
                {
                    frameCounter = 0;
                    currentFrame.X++;
                    currentFrameNumber++;

                    if ((int)currentFrame.X == image.NrColumns)
                    {
                        currentFrame.X = 0;
                        currentFrame.Y++;
                    }   
                }

                if (currentFrameNumber == image.NrFrames)
                {
                    frameCounter = 0;
                    currentFrame.X = 0;
                    currentFrame.Y = 0;
                    currentFrameNumber = 0;
                }
            }

            image.SourceRectangle = new Rectangle((int)currentFrame.X * image.Texture.Width / image.NrColumns, 
                                                  (int)currentFrame.Y * image.Texture.Height / image.NrRows, 
                                                  image.Texture.Width / image.NrColumns,
                                                  image.Texture.Height / image.NrRows);
        }

    }
}
