using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Baby_foot
{
    public class Joueur
    {
        int idJoueur {get; set;}
        //barres du joueur
        private List<Barre> barres { get; set; }
        //identité du joueur
        int identite { get;set;}

        string nom { get; set; }
        int point { get; set; }
        decimal argent { get; set; }
        Main main { get; set; }
        
        public Joueur(int id){
            int width = 300;
            if(id == 1){
                Brush br = new SolidBrush(Color.Orange);
                Barre b1 = new Barre(0, width, br);
                Pion p = new Pion(1, 710, 150);
                b1.setPions(new List<Pion> { p });
                b1.setX(710);
                Barre b2 = new Barre(0, width, br);
                b2.setPions(new List<Pion> { new Pion(2, 210, 35), new Pion(3, 210, 110), new Pion(4, 210, 185), new Pion(5, 210, 260) }); ;
                b2.setX(210);
                Barre b3 = new Barre(0, width, br);
                b3.setPions(new List<Pion> { new Pion(7, 410, 60), new Pion(6, 410, 150), new Pion(8, 410, 240) });
                b3.setX(410);
                Barre b4 = new Barre(0, width, br);
                b4.setPions(new List<Pion> { new Pion(11, 610, 35), new Pion(10, 610, 110), new Pion(9, 610, 185), new Pion(12, 610, 260) });
                b4.setX(610);

                List<Barre> list1 = new List<Barre>();
                list1.Add(b1);
                list1.Add(b2);
                list1.Add(b3);
                list1.Add(b4);

                this.setPoint(0);
                this.setBarres(list1);
                Main main = new Main();
                main.setColor(br);
                main.setPosition(10);
                this.setMain(main);
                this.setIdentite(-1);
            }
            else if(id == 2){
                Brush br2 = new SolidBrush(Color.Black);
                Barre b12 = new Barre(0, width, br2);
                b12.setPions(new List<Pion> { new Pion(12, 110, 35), new Pion(9, 110, 110), new Pion(10, 110, 185), new Pion(11, 110, 260) });
                b12.setX(110);
                Barre b22 = new Barre(0, width, br2);
                b22.setPions(new List<Pion> { new Pion(7, 310, 240), new Pion(6, 310, 150), new Pion(8, 310, 60) }) ;
                b22.setX(310);
                Barre b32 = new Barre(0, width, br2);
                b32.setPions(new List<Pion> { new Pion(2, 510, 35), new Pion(3, 510, 110), new Pion(4, 510, 185), new Pion(2, 510, 260)});
                b32.setX(510);
                Barre b42 = new Barre(0, width, br2);
                b42.setPions(new List<Pion> { new Pion(1, 10, 150) });
                b42.getPions()[0].estPorteur(true);
                b42.setX(10);

                List<Barre> list2 = new List<Barre>();
                list2.Add(b12);
                list2.Add(b22);
                list2.Add(b32);
                list2.Add(b42);

                this.setPoint(0);
                this.setBarres(list2);
                Main main2 = new Main();
                main2.setPosition(710);
                main2.setColor(br2);
                this.setMain(main2);
                this.setIdentite(1);
            }
        }
        public void passe(int selectedJ1, int babyplayer, Balle balle){
            try{
            balle.setX(this.getBarres()[selectedJ1].getPions()[babyplayer].getX());
            balle.setY(this.getBarres()[selectedJ1].getPions()[babyplayer].getY());
            }
            catch(Exception e){
                throw new Exception("Cannot passe outside ");
            }
        }
        public int indicePionMisyBalle(Balle balle, Joueur j1, Joueur j2)
        {
            for(int i = 0; i < j1.getBarres().Count; i++)
            {
                for(int j = 0; j < j1.getBarres()[i].getPions().Count; j++)
                {
                    int x1 = balle.getX() - j1.getBarres()[i].getPions()[j].getX();
                    int y1 = balle.getY() - j1.getBarres()[i].getPions()[j].getY();
                    int distance = (int)Math.Sqrt(x1 * x1 + y1 * y1);
                    if (distance < 15)
                    {
                        return j;
                    }
                }
            }
            for (int i = 0; i < j2.getBarres().Count; i++)
            {
                for (int j = 0; j < j2.getBarres()[i].getPions().Count; j++)
                {
                    int x1 = balle.getX() - j2.getBarres()[i].getPions()[j].getX();
                    int y1 = balle.getY() - j2.getBarres()[i].getPions()[j].getY();
                    int distance = (int) Math.Sqrt(x1 * x1 + y1 * y1);
                    if (distance < 15)
                    {
                        return j;
                    }
                }
            }
            return 0;
        }
        public bool checkRange(int selectedJ1){
            foreach(Pion p in this.getBarres()[selectedJ1].getPions()){
                if (p.getY() < 0) return false;
                
                if (p.getY() > 300) return false;
            }
            return true;
        }
        public void mikisaka(int fikisahana, Balle balle, int selectedJ1){
                this.getBarres()[selectedJ1].setY(this.getBarres()[selectedJ1].getY() - fikisahana);
                this.getBarres()[selectedJ1].manage_pions(fikisahana);
                this.identifierPorteur(balle);
        }
        public void checkScore(But but_adverse, Balle balle)
        {
            int x1 = balle.getX() - but_adverse.getX(); 
            int y1 = balle.getY() - but_adverse.getY();
            int distance = (int) Math.Sqrt(x1 * x1 + y1 * y1);
            if (distance <= 5)
            {
                this.setPoint(this.getPoint() + 1);
                balle.setDeplacement(balle.getDeplacement() * -1);
            }
        }
        
        public void win(System.Windows.Forms.Timer timer, Match match, Joueur j1, Joueur j2){
         //   MessageBox.Show(match.getIdMatch()+"");
            decimal argent_player = match.getMise() * (decimal) 0.9;
            decimal argent_proprio = match.getMise() * (decimal) 0.1;

            if(j1.getPoint() == match.getPointGagnant()){
                timer.Stop();
 
                Porte_feuille pf1 = new Porte_feuille("J", j1.getIdJoueur(), match.getIdMatch(), argent_player);
                Porte_feuille pf2 = new Porte_feuille("J", j2.getIdJoueur(), match.getIdMatch(), (decimal) 0);
                Porte_feuille pf3 = new Porte_feuille("P", match.getProprio().getIdProprio(), match.getIdMatch(), argent_proprio);
                pf1.Insert();
                pf2.Insert();
                pf3.Insert();
                MessageBox.Show("Victoire du joueur "+j1.nom);
            }
            else if(j2.getPoint() == match.getPointGagnant()){
                timer.Stop();

                Porte_feuille pf1 = new Porte_feuille("J", j2.getIdJoueur(), match.getIdMatch(), argent_player);
                Porte_feuille pf2 = new Porte_feuille("J", j1.getIdJoueur(), match.getIdMatch(), (decimal) 0);
                Porte_feuille pf3 = new Porte_feuille("P", match.getProprio().getIdProprio(), match.getIdMatch(), argent_proprio);
                pf1.Insert();
                pf2.Insert();
                pf3.Insert();
                MessageBox.Show("Victoire du joueur "+j2.nom);
            }
        }
        public void ajouterGardien(int gardien)
        {
            List<Pion> pions = this.getBarres()[gardien].getPions();
            int taille = pions.Count();
            Pion p = new Pion();
            p.setX(pions[pions.Count-1].getX());
            p.setY(pions[pions.Count-1].getY()+30);
            pions.Add(p);
            this.getBarres()[gardien].setPions(pions);
        }
        public void identifierPorteur(Balle balle)
        {
            for (int i = 0; i < barres.Count; i++)
            {
                for(int j = 0; j < barres[i].getPions().Count; j++)
                {
                    int x1 = balle.getX() - barres[i].getPions()[j].getX();
                    int y1 = balle.getY() - barres[i].getPions()[j].getY();

                    float distance = (float)Math.Sqrt(x1 * x1 + y1 * y1);

                    if (distance < 15 && !balle.getMove())
                    {
                        
                        balle.setY(barres[i].getPions()[j].getY());
                        balle.setX(barres[i].getPions()[j].getX());
                        //balle.setY(barres[i].getPions()[j].getY());
                    }
                }
            }
            //return balle;
        }
        public void qui_a_la_balle(Balle balle, Joueur j1, Joueur j2)
        {
            int index = 0;
            Pion enQuestion = new Pion();
            Joueur joueur = new Joueur();
            for(int i = 0; i < j1.getBarres().Count; i++)
            {
                for(int j = 0; j < j1.getBarres()[i].getPions().Count; j++)
                {
                    int x1 = balle.getX() - j1.getBarres()[i].getPions()[j].getX();
                    int y1 = balle.getY() - j1.getBarres()[i].getPions()[j].getY();
                    int distance = (int)Math.Sqrt(x1 * x1 + y1 * y1);
                    if (distance < 15)
                    {
                        joueur = j1;
                        enQuestion = j1.getBarres()[i].getPions()[j];
                        index = i;
                    }
                }
            }
            for (int i = 0; i < j2.getBarres().Count; i++)
            {
                for (int j = 0; j < j2.getBarres()[i].getPions().Count; j++)
                {
                    int x1 = balle.getX() - j2.getBarres()[i].getPions()[j].getX();
                    int y1 = balle.getY() - j2.getBarres()[i].getPions()[j].getY();
                    int distance = (int) Math.Sqrt(x1 * x1 + y1 * y1);
                    if (distance < 15)
                    {
                        joueur = j2;
                        enQuestion = j2.getBarres()[i].getPions()[j];
                        index = i;
                    }
                }
            }
            balle.setDeplacement(joueur.getIdentite());
            balle.setMove(true);
           //enQuestion.estPorteur(false);
        }
        public Joueur(string nom, int point, decimal argent)
        {
            this.nom = nom;
            this.point = point;
            this.argent = argent;
        }

        public Joueur() { }

        public void setMain(Main main) {
            this.main = main; 
        }
        public Main GetMain() {  return this.main; }
        public List<Barre> getBarres() { return barres; }
        public void setBarres(List<Barre> barres) {
            if(barres.Count == 0) throw new ArgumentNullException("Barres non trouvées");
            this.barres = barres; 
        }
        public string getNom() { return this.nom; }
        public void setNom(string nom) { 
            if(this.nom == "") throw new Exception("Player didn't initialize his name");
            this.nom = nom; 
        }
        public int getPoint() { return this.point; }
        public void setPoint(int point) { 
            if(point < 0) throw new Exception("Point negatif");
            this.point = point; 
        } 
        public decimal getArgent() { return this.argent; }
        public void setArgent(decimal argent) {
            if(argent < 0) throw new Exception("Argent negatif");
            this.argent = argent; 
        }
        public void setIdentite(int ident) { 
            this.identite = ident; 
        }
        public int getIdentite() { return this.identite; }

        public void setIdJoueur(object id) { this.idJoueur = (int)id; }
        public int getIdJoueur() { return this.idJoueur; }

        public static Joueur[] GetAll() {
            using (NpgsqlConnection connection = Connection.GetConnection()) {
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Joueur", connection)) {
                    using (NpgsqlDataReader reader = command.ExecuteReader()) {
                        List<Joueur> players = new List<Joueur>();
                        while (reader.Read()) {
                            Joueur player = new Joueur();

                            player.setIdJoueur(reader.GetInt32(0));
                            player.setNom(reader.GetString(1));
                            player.setArgent(0);
                            players.Add(player);
                        }
                        return players.ToArray();
                    }
                }

                connection.Close();
            }
        }
    }
}
