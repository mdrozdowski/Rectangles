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

namespace Rectangles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rectangle rect;
        private Point position;
        private Point size;
        private bool isRectDone;
        Random rnd;
        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();
        }

        private void LeftDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
            rect = new Rectangle();
            position = Mouse.GetPosition(canvas);
            rect.SetValue(Canvas.TopProperty, position.Y);
            rect.SetValue(Canvas.LeftProperty, position.X);
            rect.Width = 0;
            rect.Height = 0;
            rect.Stroke = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255)));
            isRectDone = false;
            canvas.Children.Add(rect);
        }
        private void RightDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this);
            rect = new Rectangle();
            position = Mouse.GetPosition(canvas);
            rect.SetValue(Canvas.TopProperty, position.Y);
            rect.SetValue(Canvas.LeftProperty, position.X);
            rect.Width = 0;
            rect.Height = 0;
            rect.Fill = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255)));
            isRectDone = false;
            canvas.Children.Add(rect);
        }

        private void Move(object sender, MouseEventArgs e)
        {
            if (!isRectDone && rect != null)
            {
                Point p = position;
                Point m = e.GetPosition(canvas);
                size = m;
                size.X -= position.X;
                size.Y -= position.Y;
                if (size.X < 0)
                {
                    size.X = Math.Abs(size.X);
                    p.X = m.X;
                }
                if (size.Y < 0)
                {
                    size.Y = Math.Abs(size.Y);
                    p.Y = m.Y;
                }
                rect.SetValue(Canvas.TopProperty, p.Y);
                rect.SetValue(Canvas.LeftProperty, p.X);
                rect.Width = size.X;
                rect.Height = size.Y;
            }
        }
        private void LeftUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            isRectDone = true;
        }
        private void RightUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            isRectDone = true;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (rect == null)
                return;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                switch (e.Key)
                {
                    case Key.Up:
                        if (rect.Height > 1)
                            rect.Height = rect.Height - 3;
                        break;
                    case Key.Down:
                        rect.Height = rect.Height + 3;
                        break;
                    case Key.Left:
                        if (rect.Width > 1)
                            rect.Width = rect.Width - 3;
                        break;
                    case Key.Right:
                        rect.Width = rect.Width + 3;
                        break;
                }
            }
            else
            {
                Point p = new Point();
                p.X = (double)rect.GetValue(Canvas.LeftProperty);
                p.Y = (double)rect.GetValue(Canvas.TopProperty);
                switch (e.Key)
                {
                    case Key.Up:
                        p.Y--;
                        break;
                    case Key.Down:
                        p.Y++;
                        break;
                    case Key.Left:
                        p.X--;
                        break;
                    case Key.Right:
                        p.X++;
                        break;
                }
                rect.SetValue(Canvas.LeftProperty, p.X);
                rect.SetValue(Canvas.TopProperty, p.Y);
            }
        }
    }
}
