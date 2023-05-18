using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Baby_foot
{
    public class Porte_feuille
    {   
        string identification {get; set; }
        int id {get; set; }
        int idMatch {get; set; }
        decimal argent {get; set; } 

        public Porte_feuille(string identification, int id, int idMatch, decimal argent){
            this. identification = identification;
            this. id = id;  
            this. idMatch = idMatch;
            this. argent = argent;
        }
        public void Insert() {
            using (NpgsqlConnection connection = Connection.GetConnection()) {
                // Create a new match
                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Porte_feuille (identification, id, idMatch, argent) VALUES (@identification, @id, @idMatch, @argent)", connection)) {
                    command.Parameters.AddWithValue("identification", this.identification);
                    command.Parameters.AddWithValue("id", this.id);
                    command.Parameters.AddWithValue("idMatch", this.idMatch);
                    command.Parameters.AddWithValue("argent", this.argent);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
