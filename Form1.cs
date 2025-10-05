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
            Direction dir = Direction.None;

            // Arrow keys
            if (keyData == Keys.Up) dir = Direction.Up;
            else if (keyData == Keys.Right) dir = Direction.Right;
            else if (keyData == Keys.Down) dir = Direction.Down;
            else if (keyData == Keys.Left) dir = Direction.Left;

            // Optional: WASD
            else if (keyData == Keys.W) dir = Direction.Up;
            else if (keyData == Keys.D) dir = Direction.Right;
            else if (keyData == Keys.S) dir = Direction.Down;
            else if (keyData == Keys.A) dir = Direction.Left;

            if (dir != Direction.None)
            {
                // ask the engine to move, then redraw the map
                gameEngine.TriggerMovement(dir);
                UpdateDisplay();
                return true; // key handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
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
