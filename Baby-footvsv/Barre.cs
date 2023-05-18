using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baby_foot
{
    public class Barre
    {
        int y { get; set; }
        int x { get; set; }

        int width { get; set; }
        Brush color { get; set; }

        List<Pion> pions { get; set; }
        public void setAllNonePorter()
        {
            foreach(Pion p in this.pions){
                p.estPorteur(false);
            }
        }
        
        public void assistance(Balle balle, int passina)
        {
            if(passina >= 0)
            {
                Pion pion = this.pions[passina];
                balle.setX(pion.getX());
                balle.setY(pion.getY());
            }
        }
        
        public Barre(int y, int width, Brush color)
        {
            this.y = y;
            this.width = width;
            this.color = color;
        }
        public void manage_pions(int value)
        {
            
            for(int i = 0; i < pions.Count; i++)
            {
                float y2 = this.x - pions[i].getX();
                if(y2 < 0) y2 = y2 * -1;
                if(y2 < 20) pions[i].setY(pions[i].getY() + value);             
            }
        }
        public Barre() { }
        public void setX(int x) { this.x = x; }
        public int getX() { return this.x; }
        public void setPions(List<Pion> pions) { this.pions = pions; }
        public List<Pion> getPions() { return this.pions; }
        public int getY() { return y; } 
        public void setY(int y) { this.y = y; }
        public int getWidth() { return width; }
        public void setWidth(int width) { this.width = width; } 
        public Brush getColor() { return color; }
        public void setColor(Brush color) { this.color = color; }
    }
}
