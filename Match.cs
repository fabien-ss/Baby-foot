using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Baby_foot
{
    public class Match
    {
        int idMatch { get; set; }
        Joueur Joueur1 { get; set; }
        Joueur Joueur2 { get; set; }
        Proprio_Baby Proprio { get; set; }
        decimal mise {get; set; }
        int pointGagnant {get; set; }

        public void setPointGagnant(int pointGagnant){this.pointGagnant = pointGagnant;}
        public int getPointGagnant(){return this.pointGagnant; }


        public Pion getSelectedPion(int X, int Y){
            foreach(Barre b in this.Joueur1.getBarres()){
                foreach(Pion p in b.getPions()){
                    float x1 = X - p.getX();
                    float y1 = Y - p.getY();
                    float distance = (float) Math.Sqrt(x1*x1 + y1*y1);
                    if(distance < 15){
                        return p;
                    }
                }
            }
            foreach(Barre b in this.Joueur2.getBarres()){
                foreach(Pion p in b.getPions()){
                    float x1 = X - p.getX();
                    float y1 = Y - p.getY();
                    float distance = (float) Math.Sqrt(x1*x1 + y1*y1);
                    if(distance < 15){
                        return p;
                    }
                }
            }
            return null;
        }
        
        public void setIdMatch(int idMatch){ this.idMatch = idMatch; } 
        public void setJoueur1(Joueur Joueur1){ this.Joueur1 = Joueur1; }
        public void setJoueur2(Joueur Joueur2){ this.Joueur2 = Joueur2; }
        public void setProprio(Proprio_Baby Proprio){ this.Proprio = Proprio; }
        
        public void setMise(decimal mise){ this.mise = mise; }
        public int getIdMatch(){ return this.idMatch; }
        public Joueur getJoueur1(){ return this.Joueur1; }
        public Joueur getJoueur2(){ return this.Joueur2; }
        public Proprio_Baby getProprio(){ return this.Proprio; }
        public decimal getMise(){ return this.mise; }
        public void Insert() {
            using (NpgsqlConnection connection = Connection.GetConnection()) {
                // Create a new match
                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO match (idJoueur1, idJoueur2, idProprio, mise) VALUES (@idJoueur1, @idJoueur2, @idProprio, @mise)", connection)) {
                    command.Parameters.AddWithValue("idJoueur1", this.Joueur1.getIdJoueur());
                    command.Parameters.AddWithValue("idJoueur2", this.Joueur2.getIdJoueur());
                    command.Parameters.AddWithValue("idProprio", this.Proprio.getIdProprio());
                    command.Parameters.AddWithValue("mise", this.mise);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public Match getLast(){
             using (NpgsqlConnection connection = Connection.GetConnection()) {
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM match order by idmatch desc limit 1", connection)) {
                    using (NpgsqlDataReader reader = command.ExecuteReader()) {
                        Match match = new Match();
                        if (reader.Read()) {
                            match.setIdMatch(reader.GetInt32(0));
                        }
                        return match;
                    }
                }
                connection.Close();
            }
        }
    }
}
