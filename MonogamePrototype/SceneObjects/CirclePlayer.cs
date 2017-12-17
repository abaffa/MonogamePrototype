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

using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogamePrototype.Colliders;
using MonogamePrototype.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePrototype.SceneObjects
{
    public class CirclePlayer : SceneObject
    {
        GraphicsDevice graphicsDevice;

        public int radius { get; set; }
        int dot_size = 5;
        double movement_speed = 500;
        int dot_x;
        int dot_y;

        private Controls controls;
        public Controller Controller { get; set; }

        public Color color { get; set; }

        List<Bullet> bullets = new List<Bullet>();
        public List<Bullet> Bullets { get { return bullets; } }
        int resetFire = 100; // ms
        int resetFireCounter = 0;


        int damageBlink = 10; // ms
        int damageAnim = 100; // ms
        int damageAnimCounter = 0;


        public void Damage()
        {
            damageAnimCounter = damageAnim;
        }

        public CirclePlayer(GraphicsDevice graphicsDevice, Controller controller, int x, int y, Color color)
        {
            this.graphicsDevice = graphicsDevice;

            controls = new Controls();
            Controller = controller;

            this.color = color;

            radius = 15;
            _x = x + radius;
            _y = y + radius;
            this.x = (int)_x;
            this.y = (int)_y;
            dir_x = 0;
            dir_y = 0;

            this.width = radius * 2;
            this.height = this.width;

            this.Colliders.Add(new CircleCollider(this, 0, 0, radius));
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Controller.Update(gameTime, controls);

            if (controls.up)
                dir_y = 1;
            if (controls.down)
                dir_y = -1;

            if (controls.right)
                dir_x = 1;

            if (controls.left)
                dir_x = -1;

            if ((controls.left || controls.right) && !controls.up && !controls.down)
                dir_y = 0;

            if ((controls.up || controls.down) && !controls.left && !controls.right)
                dir_x = 0;

            if (controls.left || controls.right)
                _x += dir_x * movement_speed * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            if (controls.up || controls.down)
                _y -= dir_y * movement_speed * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            x = (int)_x;
            y = (int)_y;

            dot_x = x - radius + dot_size + (int)((width - 3 * dot_size) * (dir_x + 1) / 2);
            dot_y = y - radius + dot_size + (int)((height - 3 * dot_size) * (-dir_y + 1) / 2);

            if (controls.fire && resetFireCounter <= 0 && (dir_x != 0 || dir_y != 0))
            {
                bullets.Add(new Bullet(graphicsDevice, dot_x, dot_y, dir_x, dir_y, color));
                resetFireCounter = resetFire;
            }

            foreach (Bullet b in bullets.ToArray())
            {
                b.Update(gameTime);
                if (b.MustDestroy)
                    bullets.Remove(b);
            }

            if (resetFireCounter > 0)
                resetFireCounter -= gameTime.ElapsedGameTime.Milliseconds;

            if (damageAnimCounter > 0)
                damageAnimCounter -= gameTime.ElapsedGameTime.Milliseconds;

        }



        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            Color current_color = color;
            if ((damageAnimCounter / damageBlink) % 2 == 1)
                current_color = Color.White;

            Primitives2D.DrawCircle(spriteBatch, new Vector2(x, y), radius, 100, current_color);
            Primitives2D.FillRectangle(spriteBatch, new Rectangle(dot_x, dot_y, dot_size, dot_size), Color.Black);

            foreach (Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }


            //Debug Colliders
            if (Program.DEBUG)
            {
                foreach (Collider col in Colliders)
                {
                    if (col is CircleCollider)
                    {
                        CircleCollider c = (CircleCollider)col;
                        Primitives2D.DrawCircle(spriteBatch, new Vector2(x + c.x, y + c.y), c.radius, 100, Color.Black);
                    }
                }
            }
            spriteBatch.End();
        }

    }
}
