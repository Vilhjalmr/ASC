using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASC.Models
{
    public class SpriteManager : DrawableGameComponent
    {
        public Boolean gameOver { get; private set; }

        SpriteBatch spriteBatch;
        UserControlledSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        public SpriteManager(Microsoft.Xna.Framework.Game game) : base(game)
        { }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>("Img/threerings"), 
                Vector2.Zero, new Point(75, 75), 10, 
                new Point(0, 0), new Point(6, 8), 
                new Vector2(6, 6));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>("Img/skullball"),
                new Vector2(150, 150), new Point(75, 75), 10,
                new Point(0, 0), new Point(6, 8), Vector2.Zero));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>("Img/skullball"),
                new Vector2(150, 300), new Point(75, 75), 10,
                new Point(0, 0), new Point(6, 8), Vector2.Zero));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>("Img/skullball"),
                new Vector2(300, 150), new Point(75, 75), 10,
                new Point(0, 0), new Point(6, 8), new Vector2(3,1)));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>("Img/skullball"),
                new Vector2(600, 300), new Point(75, 75), 10,
                new Point(0, 0), new Point(6, 8), Vector2.One));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {
                // Update player
                player.Update(gameTime, Game.Window.ClientBounds);

                // Update sprite list
                foreach (Sprite s in spriteList)
                {
                    s.Update(gameTime, Game.Window.ClientBounds);

                    // Check for collisions. Exit if found
                    if (s.collisionRect.Intersects(player.collisionRect))
                    {
                        gameOver = true;
                    }
                }
            }
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            // Draw Player
            player.Draw(gameTime, spriteBatch);

            // Draw sprite list
            foreach (Sprite s in spriteList)
            {
                s.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
