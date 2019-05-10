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
    enum PlayerState { alive, moving, dying, dead };
    public class Player
    {
        private Point position;
        private Point moveToPosition;
        private int health;
        private PlayerState state;
        private Pyramid pyramid;
        private Canvas canvas;
        private Rectangle player;
        private int _verticalOffset;
        private int _horizontalOffset;
        private double framesToMove = 1280/60;
        //when testing my time was 1.28 seconds to move
        private int counter = 0;

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
            player.Fill = Brushes.Yellow;
            player.Width = 50;
            player.Height = 50;
            canvas.Children.Add(player);
            //position = new Point(position.X + 3, position.Y + 3);
            Canvas.SetLeft(player, 50 * position.Y + 25 + _horizontalOffset);
            Canvas.SetTop(player, 50 * position.X + 50 + _verticalOffset);

        }//end Player

        /// <summary>
        /// Oliver
        /// Get keyboard input, move player, check collisions
        /// return true if alive, return false when dead
        /// </summary>
        /// <returns>true for alive, false when dead</returns>
        public bool update()
        {
            if (state == PlayerState.alive)
            {
                counter = 0;//reset
                if (Keyboard.IsKeyDown(Key.Left))
                {

                    moveToPosition = new Point(position.X + 1, position.Y - 1);
                    drawIfValid();
                }
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    moveToPosition = new Point(position.X + 1, position.Y + 1);
                    drawIfValid();
                }
                if (Keyboard.IsKeyDown(Key.Up))
                {
                    moveToPosition = new Point(position.X - 1, position.Y - 1);
                    drawIfValid();
                }
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    moveToPosition = new Point(position.X - 1, position.Y + 1);
                    drawIfValid();
                }
            }
            else if (state == PlayerState.moving)
            {
                position = new Point(position.X + (moveToPosition.X - position.X) * counter / framesToMove, position.Y + (moveToPosition.Y - position.Y) * counter / framesToMove);
                draw();
                counter++;
                if (counter > framesToMove)
                {
                    position = moveToPosition;
                    pyramid.cubeLandedOn(position);
                    state = PlayerState.alive;
                }
            }
            else if (state == PlayerState.dying)
            {
                position = new Point(position.X + 1, position.Y);
                // MessageBox.Show(Canvas.GetTop(player).ToString() + "\n" + canvas.ActualHeight);
                if (Canvas.GetTop(player) > canvas.ActualHeight)
                {
                    MessageBoxResult mbr = MessageBox.Show("Game Over, click OK to close");
                    if (mbr == MessageBoxResult.OK)
                    {
                        state = PlayerState.dead;
                    }
                    else
                    {
                        state = PlayerState.dead;
                    }
                }
                draw();
            }
            else if (state == PlayerState.dead)
            {
                canvas.Children.RemoveRange(0, canvas.Children.Count - 1);
            }
            return true;//make sure to change so you can get the logic in there
        }//end update

        private void drawIfValid()
        {
            if (pyramid.isValidMove(moveToPosition))
            {
                state = PlayerState.moving;
                draw();
            }
            else
            {
                state = PlayerState.dying;
                draw();
            }
        }

        public void draw()
        {
            Canvas.SetLeft(player, 50 * position.Y + 25 + _horizontalOffset);
            Canvas.SetTop(player, 50 * position.X + 50 + _verticalOffset);
        }//end draw
        
    }//end class
}//end namespace
