using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baby_foot
{
    public class Pion
    {
        int numero { get; set; }
        int x { get; set; }
        int y { get; set; }

        bool porteur = false;
        
        public void devant()
        {
            porteur = false;
            
        }
        public void marquer()
        {

        }
        public void tirer()
        {

        }
        public Pion(int numero, int x, int y)
        {
            this.numero = numero;
            this.x = x;
            this.y = y;
        }

        public Pion()
        {
        }

        public void PionMandendeBalle(Balle balle){
            float x1 = balle.getX() - this.getX();
            float y1 = balle.getY() - this.getY();
            float distance = (float) Math.Sqrt(x1*x1 + y1*y1);
            if(distance < 15){
                balle.setX(this.getX());
                balle.setY(this.getY());
            }    
        }
        public void estPorteur(bool es) { this.porteur = es; }
        public bool siPorteur() { return this.porteur; }
        public void setX(int x) { this.x = x; }
        public int getX() { return this.x;}
        public void setY(int y) { this.y = y; }
        public int getY() { return this.y;}
        public void setNumero(int numero) { this.numero = numero; }
        public int getNumero() { return this.numero;}
    }
}
