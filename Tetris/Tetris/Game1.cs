using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace WindowsPhoneGame2
{
    /// <summary>
    /// 這是您遊戲的主要型別
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpiriteManager spriteManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Windows Phone 預設的畫面播放速率為 30 fps。
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // 鎖定以延長電池壽命。
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// 允許遊戲先執行所需的初始化程序，再開始執行。
        /// 這是遊戲可查詢必要服務和載入任何非圖形相關內容
        /// 的地方。呼叫 base.Initialize 會列舉所有元件
        /// 並予以初始化。
        /// </summary>
        protected override void Initialize()
        {
            // TODO: 在此新增初始化邏輯

            spriteManager = new SpiriteManager(this);
            Components.Add(spriteManager);
            graphics.PreferredBackBufferFormat = SurfaceFormat.Bgr565;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// 每次遊戲都會呼叫 LoadContent 一次，這是載入所有內容
        /// 的地方。
        /// </summary>
        protected override void LoadContent()
        {
            // 建立可用來繪製紋理的新 SpriteBatch。
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
           
            // TODO: 在此使用 this.Content 來載入遊戲內容
        }

        /// <summary>
        /// 每次遊戲都會呼叫 UnloadContent 一次，這是解除載入
        /// 所有內容的地方。
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: 在此解除載入任何非 ContentManager 內容
        }

        /// <summary>
        /// 允許遊戲執行如更新世界、
        /// 檢查衝突、收集輸入和播放音訊的邏輯。
        /// </summary>
        /// <param name="gameTime">提供時間值的快照。</param>
        protected override void Update(GameTime gameTime)
        {
            // 允許遊戲結束
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: 在此新增您的更新邏輯

            base.Update(gameTime);
        }

        /// <summary>
        /// 當遊戲應該自我繪製時會呼叫此項目。
        /// </summary>
        /// <param name="gameTime">提供時間值的快照。</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: 在此新增您的繪圖程式碼
          
            base.Draw(gameTime);
        }
    }
}
