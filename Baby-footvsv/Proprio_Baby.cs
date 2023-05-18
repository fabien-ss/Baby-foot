using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Baby_foot
{
    public class Proprio_Baby
    {
        int idProprio {get; set;}
        string nom {get; set;}

        public void setNom(string nom){this.nom = nom;}
        public void setIdProprio(int idProprio){this.idProprio = idProprio;}
        public int getIdProprio(){return this.idProprio;}
        public string getNom(){return this.nom;}

        public static Proprio_Baby[] GetAll() {
        using (NpgsqlConnection connection = Connection.GetConnection()) {
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Proprio_Baby", connection)) {
                using (NpgsqlDataReader reader = command.ExecuteReader()) {
                    List<Proprio_Baby> players = new List<Proprio_Baby>();
                    while (reader.Read()) {
                        Proprio_Baby player = new Proprio_Baby();

                        player.setIdProprio(reader.GetInt32(0));
                        player.setNom(reader.GetString(1));
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
