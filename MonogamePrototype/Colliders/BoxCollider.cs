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

using MonogamePrototype.SceneObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePrototype.Colliders
{
    public class BoxCollider : Collider
    {
        public int width { get; set; }
        public int height { get; set; }

        public BoxCollider(SceneObject parent, int x, int y, int width, int height)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public override bool CheckCollision(Collider c)
        {
            BoxCollider a = this;
            if (c is BoxCollider)
            {
                BoxCollider b = (BoxCollider)c;
                //return x1 < x2 + w2 and x2 < x1 + w1 and y1 < y2 + h2 and y2 < y1 + h1
                return (a.parent.x + a.x) < (b.parent.x + b.x) + b.width && 
                       (b.parent.x + b.x) < (a.parent.x + a.x) + a.width &&
                       (a.parent.y + a.y) < (b.parent.y + b.y) + b.height &&
                       (b.parent.y + b.y) < (a.parent.y + a.y) + a.height;
            }
            else if (c is CircleCollider)
            {
                CircleCollider b = (CircleCollider)c;
                //return x1 < x2 + w2 and x2 < x1 + w1 and y1 < y2 + h2 and y2 < y1 + h1
                return (a.parent.x + a.x) < (b.parent.x + b.x) + b.radius &&
                       (b.parent.x + b.x) - b.radius < (a.parent.x + a.x) + a.width &&
                       (a.parent.y + a.y) < (b.parent.y + b.y) + b.radius &&
                       (b.parent.y + b.y) - b.radius < (a.parent.y + a.y) + a.height;
            }

            return false;
        }
    }
}
