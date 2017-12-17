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
    public class KeyboardController : Controller<Keys>
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
            controls.up = Keyboard.GetState().IsKeyDown(up);
            controls.down = Keyboard.GetState().IsKeyDown(down);
            controls.right = Keyboard.GetState().IsKeyDown(right);
            controls.left = Keyboard.GetState().IsKeyDown(left);

            controls.fire = Keyboard.GetState().IsKeyDown(fire);
        }
    }
}
