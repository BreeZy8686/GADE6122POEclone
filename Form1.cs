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
            // refresh the map text
            lblDisplay.Text = gameEngine.ToString();

            // OPTIONAL: if a label named "lblHeroStats" exists on the form, show HP there
            var statsLabel = this.Controls.Find("lblHeroStats", true).FirstOrDefault() as Label;
            if (statsLabel != null)
                statsLabel.Text = gameEngine.HeroStats;
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
            // Testing shortcut — press H to set Hero HP = 1
            else if (keyData == Keys.H)
            {
                gameEngine.CurrentLevel.Hero.SetHitPoints(1);

                UpdateDisplay();
            }

            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // refresh UI after any action (movement or attack)
            UpdateDisplay();
            return true; // handled
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // show the current level from the game engine in the existing label
            UpdateDisplay();

        }

        private void lblWasdMovement_Click(object sender, EventArgs e)
        {

        }
    }
}