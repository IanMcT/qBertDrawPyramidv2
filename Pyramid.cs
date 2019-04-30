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
    class Pyramid
    {
        private int[,] cubes = new int[7, 13];
        private Canvas _canvas;
        private int _verticalOffset;
        private int _horizontalOffset;

        public Pyramid(Canvas c)
        {
            _canvas = c;
            _verticalOffset = 100;
            _horizontalOffset = 100;
            for (int x = 0; x < cubes.GetLength(0); x++)
            {
                for (int y = 0; y < cubes.GetLength(1); y++)
                {
                    cubes[x, y] = 0;
                }
            }
            //MessageBox.Show(cubes.GetLength(0).ToString());
            //loop through each row
            for (int x = 0; x < cubes.GetLength(0); x++)
            {
                int numberOfCubes = x + 1;
                int midPoint = cubes.GetLength(1) / 2;
                int offset = 1;
                if (numberOfCubes % 2 == 1)
                {
                    cubes[x, midPoint] = 1;
                    offset = 2;
                }
                for (int i = 0; i < numberOfCubes / 2; i++)
                {
                    cubes[x, midPoint - offset] = 1;
                    cubes[x, midPoint + offset] = 1;
                    offset += 2;
                }

            }


            string output = "";

            for (int x = 0; x < cubes.GetLength(0); x++)
            {
                for (int y = 0; y < cubes.GetLength(1); y++)
                {
                    Console.Write(cubes[x, y]);
                    output += cubes[x, y].ToString();
                }
                Console.WriteLine();
                output += Environment.NewLine;
            }

            Clipboard.SetText(output);
        }

        /// <summary>
        /// 
        /// </summary>
        public void draw()
        {
            _canvas.Children.RemoveRange(0, _canvas.Children.Count);
            _canvas.Background = Brushes.Black;

            //draw cubes:
            for (int x = 0; x < cubes.GetLength(0); x++)
            {
                for (int y = 0; y < cubes.GetLength(1); y++)
                {
                    if (cubes[x, y] == 1)
                    {
                        //draw base
                        Polygon cubeFront = new Polygon();
                        Polygon cubeSide = new Polygon();
                        cubeFront.Fill = Brushes.White;
                        cubeSide.Fill = Brushes.Gray;

                        //set points from cube front
                        Point cfT = new Point(50 * y - 50+_horizontalOffset, 50 * x + 50+_verticalOffset);
                        Point cfL = new Point(50 * y - 50+_horizontalOffset, 50*x+75+_verticalOffset);
                        Point cfB = new Point(50 * y+_horizontalOffset, 50 * x + 100+_verticalOffset);
                        Point cfR = new Point(50 * y+_horizontalOffset, 50 * x + 75+_verticalOffset);

                        Point csT = cfR;
                        Point csL = cfB;
                        Point csB = new Point(50 * y + 50 + _horizontalOffset, 50 * x + 75 + _verticalOffset); 
                        Point csR=new Point(50 * y + 50 + _horizontalOffset, 50 * x + 50 + _verticalOffset);
                        PointCollection frontPoints = new PointCollection();
                        frontPoints.Add(cfT);
                        frontPoints.Add(cfL);
                        frontPoints.Add(cfB);
                        frontPoints.Add(cfR);
                        PointCollection sidePoints = new PointCollection();
                        sidePoints.Add(csT);
                        sidePoints.Add(csL);
                        sidePoints.Add(csB);
                        sidePoints.Add(csR);
                        cubeFront.Points = frontPoints;
                        cubeSide.Points = sidePoints;
                        _canvas.Children.Add(cubeFront);
                        _canvas.Children.Add(cubeSide);
                        //draw a square

                        //replace later with cube code
                        Rectangle r = new Rectangle();
                        r.Fill = Brushes.Red;
                        r.Width = 50;
                        r.Height = 50;
                    //    _canvas.Children.Add(r);
                    //    Canvas.SetLeft(r, 50 * y+_horizontalOffset);
                    //    Canvas.SetTop(r, 50 * x+_verticalOffset);
                    }


                }
                Console.WriteLine();

            }
        }
    }
}