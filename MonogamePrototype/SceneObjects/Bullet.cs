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
using MonogamePrototype.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePrototype.SceneObjects
{
    public class Bullet : SceneObject
    {
        Texture2D whiteRectangle;
        double movement_speed = 0.6;
        int maximum_distance = 1000;

        int initial_x = 0;
        int initial_y = 0;
        bool mustDestroy = false;
        Color color;

        public bool MustDestroy { get { return mustDestroy; } }


        public void Destroy()
        {
            mustDestroy = true;
        }
        public Bullet(GraphicsDevice graphicsDevice, int x, int y, double xd, double yd, Color color)
        {
            whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            this.color = color;

            _x = x;
            _y = y;

            this.initial_x = (int)_x;
            this.initial_y = (int)_y;
            this.x = (int)_x;
            this.y = (int)_y;
            this.dir_x = xd;
            this.dir_y = yd;

            this.width = 5;
            this.height = this.width;

            this.Colliders.Add(new BoxCollider(this, 0, 0, width, height));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _y -= dir_y * movement_speed * gameTime.ElapsedGameTime.Milliseconds;
            _x += dir_x * movement_speed * gameTime.ElapsedGameTime.Milliseconds;

            if (Math.Abs(x - initial_x) > maximum_distance || Math.Abs(y - initial_y) > maximum_distance)
                mustDestroy = true;

            x = (int)_x;
            y = (int)_y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // spriteBatch.Begin();

            spriteBatch.Draw(whiteRectangle, new Rectangle((int)x, (int)y, width, height), color);
            // spriteBatch.End();
        }
    }
}
