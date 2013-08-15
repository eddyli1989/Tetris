using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame2
{
    abstract class Sprite
    {
        protected BoxLogic logic;
        Texture2D textureImage;
        Point currentFrame;
        Point sheetSize;
        protected Point frameSize;

        public bool isEnd = false;

        int collisionOffset;
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame;

        const int defaultMillisecondsPerFrame = 500;
        protected Vector2 speed;
        protected List<Vector2> position;

        public Sprite(Texture2D textureImage,
                        Point framsize,
                        int collisionOffset,
                        Point currentFrame,
                        Point sheetSize,
                        Vector2 speed,
                        int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            //this.position = position;
            this.frameSize = framsize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = defaultMillisecondsPerFrame;
            logic = new BoxLogic(32,24);

            this.position = logic.createNewBox();
        }
        public virtual void Update(GameTime gametime, Rectangle clientBounds)
        {
            //timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
            //++currentFrame.X;
            //if (currentFrame.X >= sheetSize.X)
            //{
            //    currentFrame.X = 0;
            //    ++currentFrame.Y;
            //    if (currentFrame.Y > sheetSize.Y)
            //    {
            //        currentFrame.Y = 0;
            //    }
            //}
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
             
                for (int i = 0; i < logic.xMax; i++)
                    for (int j = 0; j < logic.yMax; j++)
                    {
                        int v = logic.getData(i, j);
                        if (v != -1)
                        {
                            Vector2 tmp = new Vector2(j * frameSize.X, i * frameSize.Y);
                            spriteBatch.Draw(textureImage, tmp, new Rectangle( (currentFrame.X+v) * frameSize.X,
                                                                                   currentFrame.Y * frameSize.Y,
                                                                                   frameSize.X, frameSize.Y),
                                            Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        }
                    }
            
        }
       
        //public Rectangle collisionRect
        //{
        //    get
        //    {
                
        //       Rectangle v =  new Rectangle((int)position.X + collisionOffset,
        //                                    (int)position.Y + collisionOffset,
        //                                    frameSize.X - (collisionOffset * 2),
        //                                    frameSize.Y - (collisionOffset * 2));
                
        //    }
        //}
    }
}

