using PointerStateHelper.Extensionss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace PointerStateHelper.Helpers
{
    public class PointerOverHelper
    {
        private const string BackgroundNames = "rootgrid,buttonlayoutgrid,contentpresenter,borderelement,searchboxgrid";
        private const string ForegroundNames = "contentpresenter,contentelement,textblock,text,content";
        private const string BorderNames = "rootgrid,buttonlayoutgrid,contentpresenter,borderelement,searchboxgrid,background";

        public static Brush GetBackground(Control obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(Control obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        public static Brush GetForeground(Control obj)
        {
            return (Brush)obj.GetValue(ForegroundProperty);
        }

        public static void SetForeground(Control obj, Brush value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        public static Brush GetBorderBrush(Control obj)
        {
            return (Brush)obj.GetValue(BorderBrushProperty);
        }

        public static void SetBorderBrush(Control obj, Brush value)
        {
            obj.SetValue(BorderBrushProperty, value);
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(PointerOverHelper), new PropertyMetadata(null, async (s, a) =>
             {
                 if (a.NewValue != a.OldValue)
                 {
                     if (s is FrameworkElement ele)
                     {
                         var commonStates = await ele.GetCommonStates();

                         if (commonStates != null)
                         {
                             var storyboard = commonStates.States.FirstOrDefault(x => x.Name == "PointerOver")?.Storyboard;
                             if (storyboard != null)
                             {
                                 var list = storyboard.Children.Where(x => BackgroundNames.Contains(Storyboard.GetTargetName(x).ToLowerInvariant()) && Storyboard.GetTargetProperty(x) == "Background");

                                 foreach (var item in list)
                                 {
                                     item.SetAnimationValue(a.NewValue);
                                 }
                             }
                         }
                     }
                 }
             }));

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(PointerOverHelper), new PropertyMetadata(null, async (s, a) =>
            {
                if (a.NewValue != a.OldValue)
                {
                    if (s is FrameworkElement ele)
                    {
                        var commonStates = await ele.GetCommonStates();

                        if (commonStates != null)
                        {
                            var storyboard = commonStates.States.FirstOrDefault(x => x.Name == "PointerOver")?.Storyboard;
                            if (storyboard != null)
                            {
                                var list = storyboard.Children.Where(x => ForegroundNames.Contains(Storyboard.GetTargetName(x).ToLowerInvariant()) && Storyboard.GetTargetProperty(x) == "Foreground");

                                foreach (var item in list)
                                {
                                    item.SetAnimationValue(a.NewValue);
                                }
                            }
                        }
                    }
                }
            }));

        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(PointerOverHelper), new PropertyMetadata(null, async (s, a) =>
            {
                if (a.NewValue != a.OldValue)
                {
                    if (s is FrameworkElement ele)
                    {
                        var commonStates = await ele.GetCommonStates();

                        if (commonStates != null)
                        {
                            var storyboard = commonStates.States.FirstOrDefault(x => x.Name == "PointerOver")?.Storyboard;
                            if (storyboard != null)
                            {
                                var list = storyboard.Children.Where(x => BorderNames.Contains(Storyboard.GetTargetName(x).ToLowerInvariant()) && Storyboard.GetTargetProperty(x) == "BorderBrush");

                                foreach (var item in list)
                                {
                                    item.SetAnimationValue(a.NewValue);
                                }
                            }
                        }
                    }
                }
            }));

    }
}
