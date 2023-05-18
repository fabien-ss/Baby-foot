using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baby_foot
{
    public partial class SelectionJoueur : Form
    {
        public SelectionJoueur()
        {
            InitializeComponent();

            Joueur[] players = Joueur.GetAll();
            string[] players_name = new string[players.Length];
            int i = 0;
            foreach(Joueur j in players){
                players_name[i] = j.getIdJoueur()+""+j.getNom();
                i += 1;
            }

            Proprio_Baby[] proprios = Proprio_Baby.GetAll();
            comboBoxProprio.AccessibleRole = AccessibleRole.None;
            comboBoxProprio.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProprio.FormattingEnabled = true;
            comboBoxProprio.DisplayMember = "Nom";
            comboBoxProprio.DataSource = proprios;

            comboBoxJoueur1.AccessibleRole = AccessibleRole.None;
            comboBoxJoueur1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxJoueur1.FormattingEnabled = true;
         //   comboBoxJoueur1.DisplayMember = "name";
            comboBoxJoueur1.DataSource = players_name;

            comboBoxJoueur2.BindingContext = new BindingContext();
            comboBoxJoueur2.AccessibleRole = AccessibleRole.None;
            comboBoxJoueur2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxJoueur2.FormattingEnabled = true;
           // comboBoxJoueur2.DisplayMember = "name";
            comboBoxJoueur2.DataSource = players_name;
        }

        private void AddMatch(object sender, EventArgs e)
        {
            try {

                Proprio_Baby pb = (Proprio_Baby) comboBoxProprio.SelectedItem;

                string[] Argents = { textBoxJoueur1.Text, textBoxJoueur2.Text };
                Joueur j1 = new Joueur(1);
                string arg1 = Argents[0];
                j1.setArgent(decimal.Parse(arg1));
                string selectedItem1 = (string)comboBoxJoueur1.SelectedItem;
                char t = (char) selectedItem1[0];
                
                j1.setIdJoueur(((int) (t)) - 48);
                MessageBox.Show("Player 1 "+j1.getIdJoueur()+" money "+arg1);
                j1.setNom(selectedItem1);

                Joueur j2 = new Joueur(2);
                string arg2 = Argents[1];
                j2.setArgent(decimal.Parse(arg2));
                string selectedItem2 = (string)comboBoxJoueur2.SelectedItem;
                char t2 = (char) selectedItem2[0];
                MessageBox.Show("Player 2 "+selectedItem2+" money "+arg2);
                
                j2.setIdJoueur(((int) (t2)) - 48);
                j2.setNom(selectedItem2);

                Form1 form = new Form1();
                
                But but1 = new But();
                But but2 = new But();

                but1.setX(0);
                but1.setY(150);
                but2.setX(740);
                but2.setY(150);

                Balle balle = new Balle();
        
                balle.setX(10); balle.setY(150);
                balle.setColor("White");
                balle.setSize(10);

                form.setJ1(j1);
                form.setJ2(j2);
                form.setBalle(balle);
                balle.setMove(false);
                form.setBut(but1);
                form.setBut2(but2);

                Match match = new Match();
                match.setProprio(pb);
                match.setJoueur1(j1);
                match.setJoueur2(j2);
                decimal arg1D = decimal.Parse(arg1);
                decimal arg2D = decimal.Parse(arg2);
                decimal mise = arg1D + arg2D;
                match.setMise(mise);

                match.Insert();
                
                Match lastMatch = match.getLast();

                match.setPointGagnant(1);
                match.setIdMatch(lastMatch.getIdMatch());

                form.setMatch(match);
                form.Show();
                // Player[] players = {(Player) comboBoxJoueur1.SelectedItem,(Player) comboBoxJoueur2.SelectedItem};
                // Match match = new Match(SelectionJoueurs, players);
                // BabyFoot baby = new BabyFoot(match);
                // baby.Show();

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
