using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace PointerStateHelper.Extensionss
{
    public static class VisualExtension
    {
        public static Task<VisualStateGroup> GetCommonStates(this FrameworkElement element)
        {
            var vGroup = VisualStateManager.GetVisualStateGroups(element)?.FirstOrDefault(x => x.Name == "CommonStates");
            var resultSource = new TaskCompletionSource<VisualStateGroup>();

            if (vGroup == null)
            {
                if (element.GetFirstChild() is FrameworkElement ele)
                {
                    element = ele;
                    vGroup = VisualStateManager.GetVisualStateGroups(element)?.FirstOrDefault(x => x.Name == "CommonStates");
                }
                else if (!element.IsLoaded())
                {
                    void Element_Loaded(object sender, RoutedEventArgs e)
                    {
                        element.Loaded -= Element_Loaded;
                        vGroup = VisualStateManager.GetVisualStateGroups(element)?.FirstOrDefault(x => x.Name == "CommonStates");
                        if (vGroup == null)
                        {
                            if (element.GetFirstChild() is FrameworkElement ele2)
                            {
                                element = ele2;
                                vGroup = VisualStateManager.GetVisualStateGroups(element)?.FirstOrDefault(x => x.Name == "CommonStates");
                            }
                        }
                        resultSource.SetResult(vGroup);
                    }
                    element.Loaded += Element_Loaded;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                resultSource.SetResult(vGroup);
            }
            return resultSource.Task;
        }



        public static FrameworkElement GetFirstChild(this FrameworkElement element)
        {
            if (VisualTreeHelper.GetChildrenCount(element) > 0 && VisualTreeHelper.GetChild(element, 0) is FrameworkElement ele)
            {
                return ele;
            }
            return null;
        }

        public static bool IsLoaded(this FrameworkElement element)
        {
            return element == Window.Current.Content || element.Parent != null || VisualTreeHelper.GetParent(element) != null;
        }

        public static void SetAnimationValue(this Timeline timeline, object value)
        {
            if (value is double doubleValue)
            {
                if (timeline is DoubleAnimation doubleAnimation)
                {
                    doubleAnimation.To = doubleValue;
                }
                if (timeline is DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames)
                {
                    if (doubleAnimationUsingKeyFrames.KeyFrames.LastOrDefault() is DoubleKeyFrame doubleKeyFrame)
                    {
                        doubleKeyFrame.Value = doubleValue;
                    }
                }
            }

            else if (value is Color colorValue)
            {
                if (timeline is ColorAnimation colorAnimation)
                {
                    colorAnimation.To = colorValue;
                }
                if (timeline is ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames)
                {
                    if (colorAnimationUsingKeyFrames.KeyFrames.LastOrDefault() is ColorKeyFrame colorKeyFrame)
                    {
                        colorKeyFrame.Value = colorValue;
                    }
                }
            }

            else if (value is Point pointValue)
            {
                if (timeline is PointAnimation pointAnimation)
                {
                    pointAnimation.To = pointValue;
                }
                if (timeline is PointAnimationUsingKeyFrames pointAnimationUsingKeyFrames)
                {
                    if (pointAnimationUsingKeyFrames.KeyFrames.LastOrDefault() is PointKeyFrame pointKeyFrame)
                    {
                        pointKeyFrame.Value = pointValue;
                    }
                }
            }

            else
            {
                if (timeline is ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames)
                {
                    if (objectAnimationUsingKeyFrames.KeyFrames.LastOrDefault() is ObjectKeyFrame objectKeyFrame)
                    {
                        objectKeyFrame.Value = value;
                    }
                }
            }
        }
    }
}
