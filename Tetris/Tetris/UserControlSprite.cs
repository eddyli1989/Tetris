using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


/*
 * 用户可以控制的精灵 
 */
namespace WindowsPhoneGame2
{
    class UserControlSprite:Sprite
    {

        int MoveSecendPerFrame = 100;
        
        int preStateUp = 0;
        int preStateDown = 0;

        int timesince2 = 0;
        public UserControlSprite(Texture2D textureImage, Vector2 position, Point frameSize,
                                 int collisionOffset, Point currentFrame, Point sheetSize,
                                 Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, frameSize, collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame)
        {
            
        }

        //direction 是一个变量...
        public int direction
        {
            get
            {
               
               
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    return -1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    return 1;
                }
           
                return 0; 
               
            }
        }

        public override void Update(GameTime gametime, Rectangle clientBounds)
        {

            timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
            timesince2 += gametime.ElapsedGameTime.Milliseconds;

          

            if (timesince2 > MoveSecendPerFrame)
            {
                if (direction != 0)
                {
                    position = this.logic.move(position, direction);
                }
                timesince2 -= MoveSecendPerFrame;
            }

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                
                if (this.logic.checkFall( position ))
                {
                    position = this.logic.fall(position);
                }
                else
                {
                    this.logic.dis(position);
                    position = this.logic.createNewBox();
                }
            }

            if ( this.preStateUp==0&&Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.preStateUp = 1;
                position = logic.shiftBox(position);
            }
             if ( this.preStateUp==1&&Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                this.preStateUp = 0;
            }


             if (this.preStateDown == 0 && Keyboard.GetState().IsKeyDown(Keys.Down))
             {
                 this.preStateDown = 1;
                 position = logic.goDown(position);
             }
             if (this.preStateDown == 1 && Keyboard.GetState().IsKeyUp(Keys.Down))
             {
                 this.preStateDown = 0;
             }

            base.Update(gametime,clientBounds);
        }
    }
}
