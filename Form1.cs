namespace Baby_foot
{
    public partial class Form1 : Form
    {
        Match match {get; set; }
        Joueur j1 { get; set; }
        Joueur j2 { get; set; }
        Balle balle { get; set; }

        But but { get; set; }
        But but2 { get; set; }

        int selectedJ1 = 0;
        int selectedJ2 = 3;
        int fikisahana = 10;
        int indice = 0;
        System.Windows.Forms.Timer timer;

        Pion selectedPion {get; set;}

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.DoubleBuffered = true;
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 5;
            this.timer.Tick += Timer1_Tick; 
            this.MouseClick += Form1_MouseClick;
            this.timer.Start();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.balle.Move(750, this.j1, this.j2);
            try{
                j1.win(this.timer, this.match, this.j1, this.j2);
            }
            catch(Exception ee){
                timer.Stop();
                MessageBox.Show(ee.Message);
            }
            // j2.win(this.timer);
            this.balle.dona(750, this.j1, this.j2);
            this.j1.checkScore(but, balle);
            this.j2.checkScore(but2, balle);
            Refresh();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            Graphics g = e.Graphics;
            string hex = "#FF0000";
            ColorConverter cv = new ColorConverter();
            Color color = (Color) cv.ConvertFromString(hex);
            Brush remplissage = new SolidBrush(color);
            //Dessiner le terrain
            g.FillRectangle(remplissage, 0, 0, 750, 300);
            
            //Dessiner les buts
            g.FillRectangle(new SolidBrush(Color.White), but.getX(), but.getY() , 5, 10);
            g.FillRectangle(new SolidBrush(Color.White), but2.getX(), but.getY(), 5, 10);
            

            this.drawPlayer(g);


            g.FillRectangle(this.j1.GetMain().getBrush(), 0, 320, 800, 10);
            g.FillRectangle(this.j2.GetMain().getBrush(), 0, 310, 800, 10);
            //tracage de la main
            g.FillRectangle(this.j1.GetMain().getBrush(), this.j1.GetMain().getPosition(), 310, 20, 10);
            g.FillRectangle(this.j2.GetMain().getBrush(), this.j2.GetMain().getPosition(), 320, 20, 10);

            //   g.FillRectangle(remplissage, 10, this.getJ1().getBarres()[0].getY(), this.getJ1().getBarres()[0].getWidth(), 10);
            //ito le balle
            g.FillEllipse(Brushes.White, this.balle.getX(), this.balle.getY(), 10, 10);
            this.drawScore(g);
        }

        private void drawScore(Graphics g){
            g.DrawString(this.j1.getNom() + "", new Font("Arial", 12), Brushes.Black, 350, 70);
            g.DrawString(this.j1.getPoint() + "", new Font("Arial", 24), Brushes.Black, 350, 100);
            g.DrawString(this.j2.getPoint() + "", new Font("Arial", 24), Brushes.Black, 350, 200);
            g.DrawString(this.j2.getNom() + "", new Font("Arial", 12), Brushes.Black, 350, 230);
        }

        private void drawPlayer(Graphics g){
            int distance = 10;
            for (int i = 0; i < this.j1.getBarres().Count; i++)
            {
                for(int j = 0; j < this.j1.getBarres()[i].getPions().Count; j++)
                {
                    int x = this.j1.getBarres()[i].getPions()[j].getX();
                    int y = this.j1.getBarres()[i].getPions()[j].getY();
                    g.FillEllipse(this.j1.getBarres()[i].getColor(), x, y, 10, 10);
                }   
             //   this.j1.getBarres()[i].setX(distance);
                distance += 200;
            }
            int distance2 = 720;
            for (int i = 0; i < this.j2.getBarres().Count; i++)
            {
                //g.FillRectangle(this.j2.getBarres()[i].getColor(), distance2, this.getJ2().getBarres()[i].getY(), 10, this.getJ2().getBarres()[i].getWidth());
                for (int j = 0; j < this.j2.getBarres()[i].getPions().Count; j++)
                {
                    int x = this.j2.getBarres()[i].getPions()[j].getX();
                    int y = this.j2.getBarres()[i].getPions()[j].getY();
                    g.FillRectangle(this.j2.getBarres()[i].getColor(), x, y, 10, 10);
                }
               // this.j2.getBarres()[i].setX(distance2);
                distance2 += -200;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e){
          //  MessageBox.Show("Bonjour "+e.X+" "+e.Y);
            Pion selectionnee = this.match.getSelectedPion((int) e.X, (int) e.Y);
            this.selectedPion = selectionnee;
        }   
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            // D�placer la fl�che en fonction de la touche appuy�e
            switch (e.KeyCode)
            {
                case Keys.D6:
                    if(this.selectedPion != null){
                        this.selectedPion.setX(this.selectedPion.getX() + 1);
                        this.selectedPion.PionMandendeBalle(this.balle);
                    }
                    break;
                case Keys.D4:
                    if(this.selectedPion != null){
                        this.selectedPion.setX(this.selectedPion.getX() - 1);
                        this.selectedPion.PionMandendeBalle(this.balle);
                    }
                    break;
                case Keys.D5:
                    if(this.selectedPion != null){
                        this.selectedPion.setY(this.selectedPion.getY() + 1);
                        this.selectedPion.PionMandendeBalle(this.balle);
                    }
                    break;
                case Keys.D8:
                    if(this.selectedPion != null){
                        this.selectedPion.setY(this.selectedPion.getY() - 1);
                        this.selectedPion.PionMandendeBalle(this.balle);
                    }
                    break;
                case Keys.Left:
                    if (this.j1.GetMain().getPosition() > 200)
                    {
                        this.j1.GetMain().setPosition(this.j1.GetMain().getPosition() - 200);
                        selectedJ1 = selectedJ1 - 1;
                    }
                    break;
                case Keys.Up:
                
                    this.j1.mikisaka(-fikisahana, this.balle, selectedJ1);
                    break;
                case Keys.Down:
                    this.j1.mikisaka(fikisahana, this.balle, selectedJ1);
                    break;

                case Keys.Z:
                    this.j2.mikisaka(-fikisahana, this.balle, selectedJ2);
                    break;
                case Keys.S:
                    this.j2.mikisaka(fikisahana, this.balle, selectedJ2);
                    break;

                case Keys.P:
                    //Joueur 1 Passer

                    break;
                case Keys.T:

                    this.j1.qui_a_la_balle(this.balle, this.j1, this.j2);
                    
                    //((Pion) tab[1]).estPorteur(false);
                    break;

                case Keys.N:
                    this.j1.ajouterGardien(0);
                    break;

                case Keys.B:
                    this.j2.ajouterGardien(3);
                    break;

                case Keys.L:
                    //Joueur 2 Passer
                    try{
                        indice = this.j1.indicePionMisyBalle(this.balle, this.j1, this.j2);
                        Console.WriteLine("index: " + indice);
                        this.j1.passe(selectedJ1, indice - 1, this.balle);
                    }
                    catch(Exception exx){
                        MessageBox.Show(exx.Message);
                    }
                    break;
                case Keys.M:
                    //Joueur 2 Tirer
                    try{
                        indice = this.j1.indicePionMisyBalle(this.balle, this.j1, this.j2);
                        Console.WriteLine("index: " + indice);
                        this.j1.passe(selectedJ1, indice + 1, this.balle);
                    }
                    catch(Exception exx){
                        MessageBox.Show(exx.Message);
                    }
                    break;

                case Keys.W:
                    //Joueur 2 Passer
                    try{
                        indice = this.j1.indicePionMisyBalle(this.balle, this.j1, this.j2);
                        Console.WriteLine("index: " + indice);
                        this.j2.passe(selectedJ2, indice - 1, this.balle);
                    }
                    catch(Exception exx){
                        MessageBox.Show(exx.Message);
                    }
                    break;
                case Keys.X:
                    //Joueur 2 Tirer
                    try{
                        indice = this.j1.indicePionMisyBalle(this.balle, this.j1, this.j2);
                        Console.WriteLine("index: " + indice);
                        this.j2.passe(selectedJ2, indice + 1, this.balle);
                    }
                    catch(Exception exx){
                        MessageBox.Show(exx.Message);
                    }
                    break;

                case Keys.Right:
                    if (this.j1.GetMain().getPosition() <= 750 )
                    {
                        this.j1.GetMain().setPosition(this.j1.GetMain().getPosition() + 200);
                        selectedJ1 = selectedJ1 + 1;
                    }
                    break;

                case Keys.Q:
                    if (this.j2.GetMain().getPosition() > 0)
                    {
                        this.j2.GetMain().setPosition(this.j2.GetMain().getPosition() - 200);
                        selectedJ2 = selectedJ2 - 1;
                    }
                    break;
                case Keys.D:
                    if (this.j2.GetMain().getPosition() < 750 - 200)
                    {
                        this.j2.GetMain().setPosition(this.j2.GetMain().getPosition() + 200);
                        selectedJ2 = selectedJ2 + 1;
                    }
                    break;
            }

            // Forcer le formulaire � se redessiner pour afficher la nouvelle position de la fl�che
            this.Invalidate();
        }
        public void setSelectedPion(Pion p){ this.selectedPion = p; }
        public Pion getSelectedPion(){ return this.selectedPion; }
        public void setJ1(Joueur j1) { this.j1 = j1; }
        public Joueur getJ1() {  return this.j1; }
        public void setJ2(Joueur j2) { this.j2 = j2; }
        public Joueur getJ2() { return this.j2; }
        public void setBalle(Balle balle) { this.balle = balle; }
        public Balle getBalle() { return this.balle; }
        public void setBut(But but) { this.but = but; }
        public void setBut2(But but) { this.but2 = but; }
        public But getBut() { return this.but; }
        public void setMatch(Match match) { this.match = match; }
        public Match getMatch() { return this.match; }
        public But getBut2() { return this.but2; }
    }
}