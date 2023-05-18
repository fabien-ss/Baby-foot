using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baby_foot
{
    public class Main
    {
        int position { get; set; }
        Brush color { get; set; }
        public void setColor(Brush brush) { color = brush; }
        public Brush getBrush() { return color; }
        public void setPosition(int position) {  this.position = position; }
        public int getPosition() { return this.position; }
    }
}
