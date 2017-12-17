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
    public class GamePadController : Controller
    {
        public PlayerIndex PlayerIndex { get; set; }

        public GamePadController(PlayerIndex playerIndex)
        {
            this.PlayerIndex = playerIndex;

            // default keys
            up = Buttons.DPadUp;
            down = Buttons.DPadDown;
            right = Buttons.DPadRight;
            left = Buttons.DPadLeft;
            fire = Buttons.A;
        }

        public override void Update(GameTime gameTime, Controls controls)
        {
            
            controls.up = GamePad.GetState(PlayerIndex).IsButtonDown((Buttons)up) ||  GamePad.GetState(PlayerIndex).ThumbSticks.Left.Y > 0;
            controls.down = GamePad.GetState(PlayerIndex).IsButtonDown((Buttons)down) || GamePad.GetState(PlayerIndex).ThumbSticks.Left.Y < 0;
            controls.right = GamePad.GetState(PlayerIndex).IsButtonDown((Buttons)right) || GamePad.GetState(PlayerIndex).ThumbSticks.Left.X > 0;
            controls.left = GamePad.GetState(PlayerIndex).IsButtonDown((Buttons)left) || GamePad.GetState(PlayerIndex).ThumbSticks.Left.X < 0;

            controls.fire = GamePad.GetState(PlayerIndex).IsButtonDown((Buttons)fire);

            controls.x_axis = GamePad.GetState(PlayerIndex).ThumbSticks.Left.X;
            controls.y_axis = GamePad.GetState(PlayerIndex).ThumbSticks.Left.Y;

        }
    }
}
