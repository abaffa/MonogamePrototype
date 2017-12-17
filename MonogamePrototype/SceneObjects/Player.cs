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
    public class Player : SceneObject
    {
        GraphicsDevice graphicsDevice;

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

        public Player(GraphicsDevice graphicsDevice, Controller controller, int x, int y, Color color)
        {
            this.graphicsDevice = graphicsDevice;

            controls = new Controls();
            Controller = controller;

            this.color = color;
            _x = x;
            _y = y;
            this.x = (int)_x;
            this.y = (int)_y;
            dir_x = 0;
            dir_y = 0;

            this.width = 30;
            this.height = this.width;

            this.Colliders.Add(new BoxCollider(this, 0, 0, width, height));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Controller.Update(gameTime, controls);

            if (controls.up)
                dir_y = controls.y_axis != 0 ? controls.y_axis : 1;

            if (controls.down)
                dir_y = controls.y_axis != 0 ? controls.y_axis : -1;

            if (controls.right)
                dir_x = controls.y_axis != 0 ? controls.x_axis : 1;

            if (controls.left)
                dir_x = controls.y_axis != 0 ? controls.x_axis : -1;

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

            dot_x = x + dot_size + (int)((width - 3 * dot_size) * (dir_x + 1) / 2);
            dot_y = y + dot_size + (int)((height - 3 * dot_size) * (-dir_y + 1) / 2);

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


            Primitives2D.FillRectangle(spriteBatch, new Rectangle(x, y, width, height), current_color);
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
                    if (col is BoxCollider)
                    {
                        BoxCollider c = (BoxCollider)col;
                        Primitives2D.DrawRectangle(spriteBatch, new Rectangle(x + c.x, y + c.y, c.width, c.height), Color.Black);
                    }
                }
            }

            spriteBatch.End();
        }

    }
}
