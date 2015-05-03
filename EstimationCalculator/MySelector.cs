using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Controls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using EstimationCalculator;


namespace LoopingSelector
{
    public class MySelector : Button
    {
        public MySelector(String DefaultValue, Char[] Suitss) : base ()
        {
            base.Tap += MySelector_Tap;
            base.Content = DefaultValue;
            base.Tag = new ScrollViewer();
            ScrollViewer BidSuiter = base.Tag as ScrollViewer;
            BidSuiter.Content = new StackPanel();
            BidSuiter.Height = 65;
            BidSuiter.Width = 90;
            BidSuiter.HorizontalAlignment = HorizontalAlignment.Stretch;
            BidSuiter.VerticalAlignment = VerticalAlignment.Stretch;
            BidSuiter.Visibility = Visibility.Collapsed;
            foreach (var item in Suitss)
            {
                Button x = new Button { Content = item, HorizontalAlignment = HorizontalAlignment.Stretch, MinWidth = 20 };            
                x.Tap += x_Tap;
                ((base.Tag as ScrollViewer).Content as StackPanel).Children.Add(x);
            }           
        }

        private void x_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            base.Content = (sender as Button).Content;
            (base.Tag as UIElement).Visibility = Visibility.Collapsed;
            base.Visibility = Visibility.Visible;
        }

        private void MySelector_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            base.Visibility = Visibility.Collapsed;
            (base.Tag as UIElement).Visibility = Visibility.Visible;
        }
      
        public  void ParentSelector(Panel Parent)
        {          
           foreach (Button x in ((base.Tag as ScrollViewer).Content as StackPanel).Children)
           {
               x.Background = base.Background;
               x.Foreground = base.Foreground;
               x.BorderBrush = base.BorderBrush;
           }
           ((base.Tag as ScrollViewer).Content as StackPanel).Background = new SolidColorBrush(Colors.White);
           ((base.Tag as ScrollViewer).Content as StackPanel).HorizontalAlignment = base.HorizontalAlignment;
           Grid.SetColumn(base.Tag as ScrollViewer, Grid.GetColumn(this as Button));
           Grid.SetRow(base.Tag as ScrollViewer, Grid.GetRow(this as Button));
           (base.Tag as ScrollViewer).Margin = new Thickness(0, -75, 0, -75);
           Parent.Children.Add(base.Tag as UIElement);           
       }
    }
}
