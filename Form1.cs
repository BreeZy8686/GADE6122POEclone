using System.Windows.Forms;

namespace GADE6122
{
    public partial class Form1 : Form
    {
        private GameEngine gameEngine;

        public Form1()
        {
            InitializeComponent();

            // initialise the gameEngine with 10 levels
            gameEngine = new GameEngine(10);

            // allow the form to receive key events
            this.KeyPreview = true;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Assigns the GameEngine's ToString() result to the label
            lblDisplay.Text = gameEngine.ToString();
        }

        // Handles keyboard input to move the hero.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Movement with WASD
            if (keyData == Keys.W)
            {
                gameEngine.TriggerMovement(Direction.Up);
            }
            else if (keyData == Keys.D)
            {
                gameEngine.TriggerMovement(Direction.Right);
            }
            else if (keyData == Keys.S)
            {
                gameEngine.TriggerMovement(Direction.Down);
            }
            else if (keyData == Keys.A)
            {
                gameEngine.TriggerMovement(Direction.Left);
            }
            // Attacks with Arrow Keys
            else if (keyData == Keys.Up)
            {
                gameEngine.TriggerAttack(Direction.Up);
            }
            else if (keyData == Keys.Right)
            {
                gameEngine.TriggerAttack(Direction.Right);
            }
            else if (keyData == Keys.Down)
            {
                gameEngine.TriggerAttack(Direction.Down);
            }
            else if (keyData == Keys.Left)
            {
                gameEngine.TriggerAttack(Direction.Left);
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // refresh UI after any action (movement or attack)
            lblDisplay.Text = gameEngine.ToString();
            return true; // handled
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // show the current level from the game engine in the existing label
            lblDisplay.Text = gameEngine.ToString();
        }

        private void lblWasdMovement_Click(object sender, EventArgs e)
        {

        }
    }
}