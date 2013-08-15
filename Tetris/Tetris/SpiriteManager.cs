using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsPhoneGame2
{
    /// <summary>
    /// 這是會實作 IUpdateable 的遊戲元件。
    /// </summary>
    
    public class SpiriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        UserControlSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        public SpiriteManager(Game game)
            : base(game)
        {
            // TODO: 在此建構任何子元件
        }

        /// <summary>
        /// 允許遊戲元件在開始執行前執行所需的任何初始化項目。
        /// 這是元件能夠查詢所需服務，以及載入內容的地方。
        /// </summary>
        public override void Initialize()
        {
            // TODO: 在此新增初始化程式碼
           
            base.Initialize();
        }

        /// <summary>
        /// 允許遊戲元件自我更新。
        /// </summary>
        /// <param name="gameTime">提供時間值的快照。</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: 在此新增更新程式碼
            player.Update(gameTime, Game.Window.ClientBounds);
            if (player.isEnd == true)
            {
                Game.Exit();
            }
            //foreach(Sprite s in spriteList)
            //{
            //    s.Update(gameTime, Game.Window.ClientBounds);
            //}
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            player.Draw(gameTime, spriteBatch);
            foreach (Sprite s in spriteList)
            {
                s.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new UserControlSprite(Game.Content.Load<Texture2D>(@"blocks"), Vector2.Zero, new Point(20, 20),
                                           10, new Point(0, 0), new Point(1,7), new Vector2(0, 0),1000);

            base.LoadContent();
        }

        
    }
}
