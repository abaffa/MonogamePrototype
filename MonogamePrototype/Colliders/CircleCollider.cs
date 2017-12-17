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
    public class CircleCollider : Collider
    {
        public int radius { get; set; }


        public CircleCollider(SceneObject parent, int x, int y, int radius)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public override bool CheckCollision(Collider c)
        {
            CircleCollider a = this;
            if (c is CircleCollider)
            {
                CircleCollider b = (CircleCollider)c;
                // local dx = bx - ax
                // local dy = by - ay
                // local dist = math.sqrt(dx * dx + dy * dy)
                // return dist < ar + br

                int dx = (b.parent.x + b.x) - (a.parent.x + a.x);
                int dy = (b.parent.y + b.y) - (a.parent.y + a.y);
                double dist = Math.Sqrt(dx * dx + dy * dy);

                return dist < a.radius + b.radius;
            }
            else if(c is BoxCollider)
            {
                BoxCollider b = (BoxCollider)c;
                //return x1 < x2 + w2 and x2 < x1 + w1 and y1 < y2 + h2 and y2 < y1 + h1
                return (a.parent.x + a.x) - a.radius < (b.parent.x + b.x) + b.width &&
                       (b.parent.x + b.x) < (a.parent.x + a.x) + a.radius &&
                       (a.parent.y + a.y) - a.radius < (b.parent.y + b.y) + b.height &&
                       (b.parent.y + b.y) < (a.parent.y + a.y) + a.radius;
            }

            return false;
        }
    }
}
