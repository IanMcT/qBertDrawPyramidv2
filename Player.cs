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
    enum PlayerState {alive, dying, dead };
    public class Player
    {
        private Point position;
        private int health;
        private PlayerState state;
        private Pyramid pyramid;
        private Canvas canvas;
        private Rectangle player;
        private int _verticalOffset;
        private int _horizontalOffset;

        /// <summary>
        /// default constructor
        /// </summary>
        public Player(Pyramid p, Canvas c)
        {
            health = 100;
            state = PlayerState.alive;
            player = new Rectangle();
            pyramid = p;
            canvas = c;
            position = pyramid.initializePlayer();
            _horizontalOffset = 50;
            _verticalOffset = 50;
            player = new Rectangle();
            player.Fill = Brushes.Blue;
            player.Width = 50;
            player.Height = 50;
            canvas.Children.Add(player);
            //position = new Point(position.X + 1, position.Y + 1);
            Canvas.SetLeft(player, 50 * position.Y + 25 + _horizontalOffset);
            Canvas.SetTop(player, 50 * position.X + 50 + _verticalOffset);

        }//end Player

    }
}
