namespace GADE6122
{
    public partial class Form1 : Form
    {   
        private GameEngine gameEngine;

        public Form1()
        {
            InitializeComponent();
            
            //initialise the gameEngine with 10 levels
            gameEngine = new GameEngine(10);

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Assigns the GameEngine's ToString() result to the label
            lblDisplay.Text = gameEngine.ToString();
        }


    }
}
