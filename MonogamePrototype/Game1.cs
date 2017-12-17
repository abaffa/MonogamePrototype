/*
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Author: Augusto Baffa(abaffa@inf.puc-rio.br)
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogamePrototype.Colliders;
using MonogamePrototype.Controllers;
using MonogamePrototype.SceneObjects;

namespace MonogamePrototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player1;
        //Player player2;
        CirclePlayer player2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            //graphics.ApplyChanges();
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

            //KeyboardController c1 = new KeyboardController();
            GamePadController c1 = new GamePadController(PlayerIndex.One);
            player1 = new Player(graphics.GraphicsDevice, c1, 20,
                                                           graphics.GraphicsDevice.Viewport.Height / 2, Color.Red);

            KeyboardController c2 = new KeyboardController();
            c2.up = Keys.W;
            c2.down = Keys.S;
            c2.right = Keys.D;
            c2.left = Keys.A;
            c2.fire = Keys.Space;
            //player2 = new Player(graphics.GraphicsDevice, k2, graphics.GraphicsDevice.Viewport.Width - 60,
            //                                                            graphics.GraphicsDevice.Viewport.Height / 2, Color.Blue);
            player2 = new CirclePlayer(graphics.GraphicsDevice, c2, graphics.GraphicsDevice.Viewport.Width - 60,
                                                                        graphics.GraphicsDevice.Viewport.Height / 2, Color.Blue);

            // TODO: use this.Content to load your game content here
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
            player1.Update(gameTime);
            foreach (Collider a in player1.Colliders)
            {
                foreach (Collider b in player2.Colliders)
                {
                    if (b.CheckCollision(a))
                    {
                        player1.RevertUpdate();
                    }

                }

                foreach (Bullet bullet in player2.Bullets)
                    foreach (Collider b in bullet.Colliders)
                    {
                        if (b.CheckCollision(a))
                        {
                            player1.Damage();
                            bullet.Destroy();
                        }
                    }
            }


            player2.Update(gameTime);
            foreach (Collider a in player2.Colliders)
            {
                foreach (Collider b in player1.Colliders)
                {
                    if (b.CheckCollision(a))
                    {
                        player2.RevertUpdate();
                    }

                }

                foreach (Bullet bullet in player1.Bullets)
                    foreach (Collider b in bullet.Colliders)
                    {
                        if (b.CheckCollision(a))
                        {
                            player2.Damage();
                            bullet.Destroy();
                        }
                    }
            }

            if (player1.x < 0 || player1.y < 0 ||
                 player1.x + player1.width > graphics.GraphicsDevice.Viewport.Width ||
                 player1.y + player1.height > graphics.GraphicsDevice.Viewport.Height)
                player1.RevertUpdate();

            if (player2.x - player2.radius < 0 || player2.y - player2.radius < 0 ||
                 player2.x + player2.radius > graphics.GraphicsDevice.Viewport.Width ||
                 player2.y + player2.radius > graphics.GraphicsDevice.Viewport.Height)
                player2.RevertUpdate();


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
