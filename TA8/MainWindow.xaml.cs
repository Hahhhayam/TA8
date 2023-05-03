using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TA8.API;
using TA8.API.Interfaces;
using TA8.API.Trees;

namespace TA8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Controller controller;

        private void generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int capacity = Int32.Parse(input_length.Text);
                List<int> data = new List<int>();

                Random rand = new Random();
                for (int i = 0; i < capacity; i++)
                {
                    int randNum = rand.Next(1, 150);
                    if (data.Contains(randNum)) { continue; }
                    data.Add(randNum);
                }
                        
                        
                if (Simple.IsChecked == true) 
                {
                    controller = new Controller(new SimpleTree(data));
                }
                if (Balanced.IsChecked == true)
                {
                    controller = new Controller(new BalancedTree(data));
                }
                if (RedBlack.IsChecked == true)
                {
                    controller = new Controller(new RedAndBlackTree(data));
                }
            }
            catch (Exception ex)
            {
                status.Content = ex.Message;
                status.Background = Brushes.Red;
                return;
            }
            status.Content = "Created";
            status.Background = Brushes.Green;
            listcontrols.IsEnabled = false;
            controls.IsEnabled = true;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int capacity = Int32.Parse(input_length.Text);
                List<int> data = new List<int>();
                foreach (string item in input.Text.Split('\n'))
                {
                    data.Add(int.Parse(item));
                }

                if (Simple.IsChecked == true)
                {
                    controller = new Controller(new SimpleTree(data));
                }
                if (Balanced.IsChecked == true)
                {
                    controller = new Controller(new BalancedTree(data));
                }
                if (RedBlack.IsChecked == true)
                {
                    controller = new Controller(new RedAndBlackTree(data));
                }
            }
            catch (Exception ex)
            {
                status.Content = ex.Message;
                status.Background = Brushes.Red;
                return;
            }
            status.Content = "Created";
            status.Background = Brushes.Green;
            listcontrols.IsEnabled = false;
            controls.IsEnabled = true;
        }

        private void use_reservated_Click(object sender, RoutedEventArgs e)
        {
            if (Simple.IsChecked == true)
            {
                controller = new Controller(new SimpleTree(new List<int> { 9, 14, 12, 17, 23, 19, 50, 72, 54, 67, 76 }));
            }
            if (Balanced.IsChecked == true)
            {
                controller = new Controller(new BalancedTree(new List<int> { 9, 14, 12, 17, 23, 19, 50, 72, 54, 67, 76 }));
            }
            if (RedBlack.IsChecked == true)
            {
                controller = new Controller(new RedAndBlackTree(new List<int> { 9, 14, 12, 17, 23, 19, 50, 72, 54, 67, 76 }));
            }
            status.Content = "Created";
            status.Background = Brushes.Green;
            listcontrols.IsEnabled = false;
            controls.IsEnabled = true;
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                output.Text += controller.FindElement(element.Text) + "\n-------------------------------------------------------\n";
                sw.Stop();
            }
            catch (Exception ex)
            {
                sw.Stop();
                status.Content = ex.Message + sw.Elapsed.TotalMilliseconds + " ms.";
                status.Background = Brushes.Red;
                return;
            }
            status.Content = "Done: " + sw.Elapsed.TotalMilliseconds + " ms.";
            status.Background = Brushes.Green;
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                controller.RemoveElement(element.Text);
                sw.Stop();
            }
            catch (Exception ex)
            {
                sw.Stop();
                status.Content = ex.Message + sw.Elapsed.TotalMilliseconds + " ms.";
                status.Background = Brushes.Red;
                return;
            }
            status.Content = "Done: " + sw.Elapsed.TotalMilliseconds + " ms.";
            status.Background = Brushes.Green;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                controller.AddElement(element.Text);
                sw.Stop();
            }
            catch (Exception ex)
            {
                sw.Stop();
                status.Content = ex.Message + sw.Elapsed.TotalMilliseconds + " ms.";
                status.Background = Brushes.Red;
                return;
            }
            status.Content = "Done: " + sw.Elapsed.TotalMilliseconds + " ms.";
            status.Background = Brushes.Green;
        }

        private void direction_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                controller.GetDirection(element.Text).ForEach(x => output.Text += x + "-");
                sw.Stop();
                output.Text += "\n-------------------------------------------------------\n";
            }
            catch (Exception ex)
            {
                sw.Stop();
                status.Content = ex.Message + sw.Elapsed.TotalMilliseconds + " ms.";
                status.Background = Brushes.Red;
                return;
            }
            status.Content = "Done: " + sw.Elapsed.TotalMilliseconds + " ms.";
            status.Background = Brushes.Green;
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            controller.GetList().ForEach(x => output.Text += x + "\n");
            output.Text += "\n-------------------------------------------------------\n";
        }
    }
}
