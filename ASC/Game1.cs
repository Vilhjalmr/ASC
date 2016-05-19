using ASC.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ASC
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;

        Texture2D ringsTexture;
        Texture2D skullTexture;
        Texture2D plusTexture;

        Point ringsFrameSize = new Point(75, 75);
        Point ringsCurrentFrame = new Point(0, 0);
        Point ringsSheetSize = new Point(6, 8);

        Point skullFrameSize = new Point(75, 75);
        Point skullCurrentFrame = new Point(0, 0);
        Point skullSheetSize = new Point(6, 8);

        Point plusFrameSize = new Point(75, 75);
        Point plusCurrentFrame = new Point(0, 0);
        Point plusSheetSize = new Point(6, 4);

        int ringsTimeSinceLastFrame = 0;
        int ringsMillisecondsPerFrame = 50;

        int skullTimeSinceLastFrame = 0;
        const int skullMillisecondsPerFrame = 30;

        int plusTimeSinceLastFrame = 0;
        int plusMillisecondsPerFrame = 50;

        Vector2 ringsPosition = Vector2.Zero;
        const float ringsSpeed = 6;
        Vector2 skullPosition = new Vector2(100, 100);
        Vector2 plusPosition = new Vector2(300, 300);

        MouseState prevMouseState;

        int ringsCollisionRectOffset = 10;
        int skullCollisionRectOffset = 10;
        int plusCollisionRectOffset = 10;

        protected bool Collide()
        {
            Rectangle ringsRect = new Rectangle((int)ringsPosition.X + ringsCollisionRectOffset, 
                (int)ringsPosition.Y + ringsCollisionRectOffset, 
                ringsFrameSize.X - (ringsCollisionRectOffset * 2), 
                ringsFrameSize.Y - (ringsCollisionRectOffset * 2));
            Rectangle skullRect = new Rectangle((int)skullPosition.X + skullCollisionRectOffset,
                (int)skullPosition.Y + skullCollisionRectOffset, 
                skullFrameSize.X - (skullCollisionRectOffset * 2), 
                skullFrameSize.Y - (skullCollisionRectOffset * 2));
            Rectangle plusRect = new Rectangle((int)plusPosition.X + plusCollisionRectOffset,
                (int)plusPosition.Y + plusCollisionRectOffset,
                plusFrameSize.X - (plusCollisionRectOffset * 2),
                plusFrameSize.Y - (plusCollisionRectOffset * 2));
            return ringsRect.Intersects(skullRect);

        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ringsTexture = Content.Load<Texture2D>("threerings");
            skullTexture = Content.Load<Texture2D>("skullball");
            plusTexture = Content.Load<Texture2D>("plus");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ringsTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (ringsTimeSinceLastFrame > ringsMillisecondsPerFrame)
            {
                ringsTimeSinceLastFrame -= ringsMillisecondsPerFrame;
                ++ringsCurrentFrame.X;
                if (ringsCurrentFrame.X >= ringsSheetSize.X)
                {
                    ringsCurrentFrame.X = 0;
                    ++ringsCurrentFrame.Y;
                    if (ringsCurrentFrame.Y >= ringsSheetSize.Y)
                        ringsCurrentFrame.Y = 0;
                }
            }

            skullTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (skullTimeSinceLastFrame > skullMillisecondsPerFrame)
            {
                skullTimeSinceLastFrame -= skullMillisecondsPerFrame;
                ++skullCurrentFrame.X;
                if (skullCurrentFrame.X >= skullSheetSize.X)
                {
                    skullCurrentFrame.X = 0;
                    ++skullCurrentFrame.Y;
                    if (skullCurrentFrame.Y >= skullSheetSize.Y)
                        skullCurrentFrame.Y = 0;
                }
            }

            plusTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if ( plusTimeSinceLastFrame > plusMillisecondsPerFrame)
            {
                plusTimeSinceLastFrame -= plusMillisecondsPerFrame;
                ++plusCurrentFrame.X;
                if (plusCurrentFrame.X >= plusSheetSize.X)
                {
                    plusCurrentFrame.X = 0;
                    ++plusCurrentFrame.Y;
                    if (plusCurrentFrame.Y >= plusSheetSize.Y)
                    {
                        plusCurrentFrame.Y = 0;
                    }
                }

            }

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                ringsPosition.X -= ringsSpeed;
            if (keyboardState.IsKeyDown(Keys.Right))
                ringsPosition.X += ringsSpeed;
            if (keyboardState.IsKeyDown(Keys.Up))
                ringsPosition.Y -= ringsSpeed;
            if (keyboardState.IsKeyDown(Keys.Down))
                ringsPosition.Y += ringsSpeed;

            MouseState mouseState = Mouse.GetState();
            if (mouseState.X != prevMouseState.X || 
                mouseState.Y != prevMouseState.Y)
                ringsPosition = new Vector2(mouseState.X, mouseState.Y);
            prevMouseState = mouseState;

            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            ringsPosition.X += ringsSpeed * gamepadState.ThumbSticks.Left.X;
            ringsPosition.Y -= ringsSpeed * gamepadState.ThumbSticks.Left.Y;

            if (ringsPosition.X < 0) ringsPosition.X = 0;
            if (ringsPosition.Y < 0) ringsPosition.Y = 0;
            if (ringsPosition.X > Window.ClientBounds.Width - ringsFrameSize.X)
                ringsPosition.X = Window.ClientBounds.Width - ringsFrameSize.X;
            if (ringsPosition.Y > Window.ClientBounds.Height - ringsFrameSize.Y)
                ringsPosition.Y = Window.ClientBounds.Height - ringsFrameSize.Y;

            if (Collide()) Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            spriteBatch.Draw(
                ringsTexture, 
                ringsPosition,
                new Rectangle(
                    ringsCurrentFrame.X * ringsFrameSize.X,
                    ringsCurrentFrame.Y * ringsFrameSize.Y,
                    ringsFrameSize.X,
                    ringsFrameSize.Y),
                Color.White, 
                0, 
                Vector2.Zero,
                1, 
                SpriteEffects.None,
                0);

            spriteBatch.Draw(skullTexture, skullPosition,
                new Rectangle(skullCurrentFrame.X * skullFrameSize.X,
                skullCurrentFrame.Y * skullFrameSize.Y,
                skullFrameSize.X,
                skullFrameSize.Y),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);

            spriteBatch.Draw(
                plusTexture,
                plusPosition,
                new Rectangle(
                    plusCurrentFrame.X * plusFrameSize.X,
                    plusCurrentFrame.Y * plusFrameSize.Y,
                    plusFrameSize.X,
                    plusFrameSize.Y),
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
