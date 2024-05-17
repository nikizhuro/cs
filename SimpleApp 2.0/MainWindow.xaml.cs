using OpenHardwareMonitor.Collections;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using SimpleApp_2._0.src;
using System.Threading.Tasks;

namespace SimpleApp_2._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 0;
            Animation.To = 160;
            Animation.Duration = TimeSpan.FromSeconds(0.5);

            network_button.BeginAnimation(WidthProperty, Animation);
            chipset_button.BeginAnimation(WidthProperty, Animation);
            program_button.BeginAnimation(WidthProperty, Animation);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void network_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 160;
            Animation.To = 180;
            Animation.Duration = TimeSpan.FromSeconds(0.2);

            network_button.BeginAnimation(WidthProperty, Animation);
            network_button.BeginAnimation(HeightProperty, Animation);

            network_label.Visibility = Visibility.Visible;

            Animation.From = 0;
            Animation.To = 35;

            network_label.BeginAnimation(HeightProperty, Animation);
        }
        private void network_button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            int i = 0;
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 180;
            Animation.To = 160;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            network_button.BeginAnimation(WidthProperty, Animation);
            network_button.BeginAnimation(HeightProperty, Animation);

            Animation.From = 35;
            Animation.To = 1;

            Animation.Completed += Animation_Completed;
            network_label.BeginAnimation(HeightProperty, Animation);
            void Animation_Completed(object Sender, EventArgs args)
            {
                if (i == 0)
                {
                    network_label.Visibility = Visibility.Hidden;
                }
            }

            
            
        }

        private void chipset_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 160;
            Animation.To = 180;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            chipset_button.BeginAnimation(WidthProperty, Animation);
            chipset_button.BeginAnimation(HeightProperty, Animation);

            chipset_label.Visibility = Visibility.Visible;

            Animation.From = 0;
            Animation.To = 35;

            chipset_label.BeginAnimation(HeightProperty, Animation);
        }
        private void chipset_button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            int i = 0;
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 180;
            Animation.To = 160;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            chipset_button.BeginAnimation(WidthProperty, Animation);
            chipset_button.BeginAnimation(HeightProperty, Animation);

            Animation.From = 35;
            Animation.To = 1;

            Animation.Completed += Animation_Completed;
            chipset_label.BeginAnimation(HeightProperty, Animation);
            void Animation_Completed(object Sender, EventArgs args)
            {
                if (i == 0)
                {
                    chipset_label.Visibility = Visibility.Hidden;
                }
            }
        }

        private void program_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 160;
            Animation.To = 180;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            program_button.BeginAnimation(WidthProperty, Animation);
            program_button.BeginAnimation(HeightProperty, Animation);

            program_label.Visibility = Visibility.Visible;

            Animation.From = 0;
            Animation.To = 35;

            program_label.BeginAnimation(HeightProperty, Animation);
        }
        private void program_button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            int i = 0;
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 180;
            Animation.To = 160;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            program_button.BeginAnimation(WidthProperty, Animation);
            program_button.BeginAnimation(HeightProperty, Animation);

            Animation.From = 35;
            Animation.To = 1;

            Animation.Completed += Animation_Completed;
            program_label.BeginAnimation(HeightProperty, Animation);
            void Animation_Completed(object Sender, EventArgs args)
            {
                if (i == 0)
                {
                    program_label.Visibility = Visibility.Hidden;
                }
            }
        }

        private void close_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 130;
            Animation.To = 160;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            close_button.BeginAnimation(WidthProperty, Animation);
            close_button.Background = Brushes.DarkGreen;
            close_button.Foreground = Brushes.White;
        }
        private void close_button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 160;
            Animation.To = 133;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            close_button.BeginAnimation(WidthProperty, Animation);
            close_button.Background = Brushes.White;
            close_button.Foreground = Brushes.Black;
        }

        private void color_button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 130;
            Animation.To = 160;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            
        }
        private void color_button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.From = 160;
            Animation.To = 133;
            Animation.Duration = TimeSpan.FromSeconds(0.2);
            
        }

        private void color_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 30;
            animation.To = i;
            animation.Duration = TimeSpan.FromSeconds(0.2);


            animation.Completed += Animation_Completed;

            void Animation_Completed(object Sender, EventArgs args)
            {
                if (i == 0)
                {
                    this.Close();
                }
            }

            close_button.BeginAnimation(WidthProperty, animation);

            animation.From = 160;
            animation.To = 0;

            network_button.BeginAnimation(HeightProperty, animation);
            program_button.BeginAnimation(HeightProperty, animation);
            chipset_button.BeginAnimation(HeightProperty, animation);
        }

        private async void loadFormWithLoadingScreen<Type>() 
            where Type : Window, new()
        {
            //first step:
            LoadingScreen loadingScreen = new LoadingScreen
            {
                Owner = this,
                Topmost = true
            };

            this.Hide();
            loadingScreen.Activate();
            loadingScreen.Show();
            loadingScreen.toCenter(this);

            //second step:
            Type win = new Type
            {
                Owner = this,
                Topmost = true
            };
            win.Unloaded += (_s, _e) =>
            {
                this.Show();
                this.Activate();
            };
            win.Loaded += (_s, _e) =>
            {
                loadingScreen.Close();
            };

            win.toCenter(this);
            win.Show();
        }

        private void network_button_Click(object sender, RoutedEventArgs e)
        {
            loadFormWithLoadingScreen<Window3>();
        }

        private void chipset_button_Click(object sender, RoutedEventArgs e)
        {
            loadFormWithLoadingScreen<Window1>();
        }

        private void program_button_Click(object sender, RoutedEventArgs e)
        {
            loadFormWithLoadingScreen<Window2>();
        }
    }
}
