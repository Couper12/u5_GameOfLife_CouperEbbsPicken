/* Couper Ebbs-Picken
 * 6/4/2018
 * Program a game thingy
 */ 
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
using System.Windows.Threading;

namespace u5_GameOfLife_Couper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] cellArray = new int[20, 20];
        Rectangle cell;
        int xCoordinate;
        int yCoordinate;
        DispatcherTimer simulationTimer = new DispatcherTimer();
        int liveCounter;

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 21; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Black;
                rectangle.Height = 5;
                rectangle.Width = 800;
                canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, i * 40);
            }

            for (int i = 0; i < 21; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Black;
                rectangle.Height = 800;
                rectangle.Width = 5;
                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, i * 40);
            }

            string temp = "";
            for (int x = 0; x < cellArray.GetLength(0); x++)
            {
                for (int y = 0; y < cellArray.GetLength(1); y++)
                {
                    temp += cellArray[x, y].ToString() + "\t";
                } 
                temp += "\n";
            }
            //messagebox.show(temp);
        }

        private void btn_addCell_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txt_xCoordinate.Text, out xCoordinate);
            int.TryParse(txt_yCoordinate.Text, out yCoordinate);
            cell = new Rectangle();
            cell.Fill = Brushes.Green;
            cell.Width = 35;
            cell.Height = 35;
            canvas2.Children.Add(cell);
            Canvas.SetLeft(cell, ((40 * xCoordinate) - 40) + 5);
            Canvas.SetTop(cell, ((40 * yCoordinate) - 40) + 5);
            cellArray[xCoordinate, yCoordinate] = 1;
            txt_xCoordinate.Text = "Xcoordinate";
            txt_yCoordinate.Text = "Ycoordinate";
        }

        private void redraw(Canvas c, int[,] cells)
        {


            int counter = c.Children.Count;
            ////messagebox.show(counter.ToString());

            for (int i = counter - 1; i >= 0; i--)
            {
                c.Children.RemoveAt(i);
            }

            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (cells[i, j] == 1
                        || cells[i, j] == 2)
                    {
                        cell = new Rectangle();
                        cell.Fill = Brushes.Green;
                        cell.Width = 35;
                        cell.Height = 35;
                        canvas2.Children.Add(cell);
                        Canvas.SetLeft(cell, ((40 * i) - 40) + 5);
                        Canvas.SetTop(cell, ((40 * j) - 40) + 5);
                    }
                    if (cells[i, j] == 3)
                    {
                        cells[i, j] = 0;
                    }
                    else if (cells[i, j] == 2)
                    {
                        cells[i, j] = 1;
                    }
                }
            }
        }

        private void btn_runSimulation_Click(object sender, RoutedEventArgs e)
        {
            simulationTimer.Tick += gameTimer_Tick;
            simulationTimer.Interval = new TimeSpan(0, 0, 0, 1);//fps
            simulationTimer.Start();

            
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    liveCounter = 0;

                    // check cell above and left
                    if (i != 0
                    && j != 0
                    && (cellArray[i - 1, j - 1] == 1
                    || cellArray[i - 1, j - 1] == 3))
                    {
                        //messagebox.show("adding cell above left");
                        liveCounter++;
                    }

                    // check cell above
                    if (j != 0
                        && (cellArray[i, j - 1] == 1
                        || cellArray[i, j - 1] == 3))
                    {
                        //messagebox.show("adding cell from above");
                        liveCounter++;
                    }

                    // check cell above and right
                    if (j != 0
                        && i != 19
                        && (cellArray[i + 1, j - 1] == 1
                        || cellArray[i + 1, j - 1] == 3))
                    {
                        //messagebox.show("adding cell from above right");
                        liveCounter++;
                    }

                    // check cell left
                    if (i != 0
                        && (cellArray[i - 1, j] == 1
                        || cellArray[i - 1, j] == 3))
                    {
                        //messagebox.show("adding cell from left");
                        liveCounter++;
                    }

                    // check cell right
                    if (i != 19
                        && (cellArray[i + 1, j] == 1
                        || cellArray[i + 1, j] == 3))
                    {
                        //messagebox.show("adding cell from right");
                        liveCounter++;
                    }

                    // check cell below left
                    if (i != 0
                        && j != 19
                        && (cellArray[i - 1, j + 1] == 1
                        || cellArray[i - 1, j + 1] == 3))
                    {
                        //messagebox.show("adding cell from below left");
                        liveCounter++;
                    }

                    // check below
                    if (j != 19
                        && (cellArray[i, j + 1] == 1
                        || cellArray[i, j + 1] == 3))
                    {
                        //messagebox.show("adding cell from below");
                        liveCounter++;
                    }

                    // check below right
                    if (i != 19
                        && j != 19
                        && (cellArray[i + 1, j + 1] == 1
                        || cellArray[i + 1, j + 1] == 3))
                    {
                        //messagebox.show("adding cell for below right");
                        liveCounter++;
                    }

                    // check to see if cell becomes live
                    ////messagebox.show("Live counter is: " + liveCounter.ToString());
                    if (liveCounter == 3
                        && cellArray[i, j] == 0)
                    {
                        //messagebox.show("Living up cell: (" + i.ToString() + ", " + j.ToString() + ")");
                        cellArray[i, j] = 2;
                    }

                    // check to see if cell is still live
                    if ((liveCounter < 2
                        || liveCounter > 3)
                        && cellArray[i, j] == 1)
                    {
                        //messagebox.show("Killing cell: (" + i.ToString() + ", " + j.ToString() + ")");
                        cellArray[i, j] = 3;
                    }

                }
            }

            redraw(canvas2, cellArray);
        }

        private void btn_stopSimulation_Click(object sender, RoutedEventArgs e)
        {
            simulationTimer.Stop();
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            int counter = canvas2.Children.Count;

            for (int i = counter - 1; i >= 0; i--)
            {
                canvas2 .Children.RemoveAt(i);
            }
        }
    }
}


