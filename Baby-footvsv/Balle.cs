using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baby_foot
{
    public class Balle
    {
        int x { get; set; }
        int y { get; set; }
        int size { get; set; }
        string color { get; set; }
        bool isMoving = false;
        int deplacement = 1;
        public void setDeplacement(int de) { deplacement = de; }
        public int getDeplacement() { return deplacement; }
        public void changeY(int change)
        {
            
            if(this.isMoving == false) this.y += change;
        }
        public void possession(Joueur joueur){
            List<Barre> list = joueur.getBarres();
            if (this.isMoving == true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < list[i].getPions().Count; j++)
                    {
                        float x2 = this.x - list[i].getPions()[j].getX();
                        float y2 = this.y - list[i].getPions()[j].getY();
                        float distance = (float)Math.Sqrt(x2 * x2 + y2 * y2);
                        if (distance <= 10/* & list[i].getPions()[j].siPorteur() == false*/)
                        {
                            int decal = joueur.getIdentite();
                            decal = 10 * decal;
                            this.setX(list[i].getPions()[j].getX() + decal);
                            this.setY(list[i].getPions()[j].getY());
                            this.setMove(false);
                            //list[i].getPions()[j].estPorteur(true);
                        }
                    }
                }
            }
        }
        public void dona(int width,Joueur j1, Joueur j2)
        {
            this.possession(j1);
            this.possession(j2);
        }
        public void Move(int width, Joueur j1, Joueur j2) {
            if (this.isMoving == true)
            {
                if(this.x == 0) this.deplacement = this.deplacement * -1;
                if (this.x == width) this.deplacement = this.deplacement * -1;
                this.x += deplacement;
            }
        }
        public void setMove(bool move) { this.isMoving = move; }
        public bool getMove() { return this.isMoving ; }
        public void setSize(int size) { this.size = size; }
        public int getX() { return this.x; }
        public void setX(int x) { this.x = x;}
        public int getY() { return this.y; }
        public void setY(int y) { this.y = y;}
        public string getColor() { return this.color; }
        public void setColor(string color) { this.color = color;}
       
    }
}
