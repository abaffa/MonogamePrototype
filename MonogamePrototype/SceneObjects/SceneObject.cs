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
using MonogamePrototype.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePrototype.SceneObjects
{
    public abstract class SceneObject
    {
        protected double _x;
        protected double _y;

        public int x { get; set; }
        public int y { get; set; }

        public double dir_x { get; set; }
        public double dir_y { get; set; }

        public int old_x { get; set; }
        public int old_y { get; set; }

        public double old_dir_x { get; set; }
        public double old_dir_y { get; set; }


        public int width { get; set; }
        public int height { get; set; }

        List<Collider> colliders = new List<Collider>();
        public List<Collider> Colliders { get { return colliders; } }

        public virtual void Update(GameTime gameTime)
        {
            x = (int)_x;
            y = (int)_y;
            old_x = x;
            old_y = y;
            old_dir_x = dir_x;
            old_dir_y = dir_y;
        }

        public virtual void RevertUpdate()
        {
            _x = old_x;
            _y = old_y;

            x = (int)_x;
            y = (int)_y;

            dir_x = old_dir_x;
            dir_y = old_dir_y;
        }
    }
}
