using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace qbert
{

    public enum GameState { Menu, ScoreScreen, GameOn, GameOver, movingToGame }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global Variables
        public Pyramid pyramid;
        public GameState gameState;
        public System.Windows.Threading.DispatcherTimer gameTimer;
        public Player player;
        public HighScores highScores;

        /// <summary>
        /// Constructor - set all initial values
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            highScores = new HighScores();
            highScores.getHighScores();
            highScores.showHighScores();

            highScores.addHighScore(1000, "David");

            gameState = GameState.movingToGame;
            gameTimer = new System.Windows.Threading.DispatcherTimer(); 
            //start Timer
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();



        }//end MainWindow

        /// <summary>
        /// Runs for every frame (i.e. fps), controls everything.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameState == GameState.movingToGame)
            {
                pyramid = new Pyramid(canvas);
                pyramid.draw();
                gameState = GameState.GameOn;
                player = new Player(pyramid, canvas);
            }//done movingtogame
            else if (gameState == GameState.GameOn)
            {
                player.update();//add logic for death
            }//done GameOn
        }//end GameTimer_Tick
    }//end Class
}//end Namespace
