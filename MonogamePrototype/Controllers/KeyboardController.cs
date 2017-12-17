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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MonogamePrototype.Controllers
{
    public class KeyboardController : Controller
    {

        public KeyboardController()
        {
            // default keys
            up = Keys.Up;
            down = Keys.Down;
            right = Keys.Right;
            left = Keys.Left;
            fire = Keys.Enter;
        }

        public override void Update(GameTime gameTime, Controls controls)
        {
            controls.up = Keyboard.GetState().IsKeyDown((Keys)up);
            controls.down = Keyboard.GetState().IsKeyDown((Keys)down);
            controls.right = Keyboard.GetState().IsKeyDown((Keys)right);
            controls.left = Keyboard.GetState().IsKeyDown((Keys)left);

            controls.fire = Keyboard.GetState().IsKeyDown((Keys)fire);
            controls.x_axis = 0;
            controls.y_axis = 0;
        }
    }
}
