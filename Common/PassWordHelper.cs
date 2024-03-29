using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Demo.Common
{
    public class PassWordHelper
    {
            //注册依赖属性 //获取，触发，循环更新
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PassWordHelper),
                                                                                                           new FrameworkPropertyMetadata("", OnPasswordPropertyChanged));
        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PassWordHelper), new PropertyMetadata(false, OnAttachPropertyChanged));
 
        static bool _isUpdate = false;

        //get set 函数
        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }
        public static bool GetAttach(DependencyObject dp)
        {
            return (bool) dp.GetValue(AttachProperty);
        }

        public static string GetPassword(DependencyObject dp)
        {
            return dp.GetValue(PasswordProperty).ToString();
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }


        //事件执行逻辑
        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!_isUpdate)
            {
                passwordBox.Password = e.NewValue?.ToString();
            }
            passwordBox.PasswordChanged += PasswordChanged;
         }

        private static void OnAttachPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged += PasswordChanged;
            
        }

         private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _isUpdate = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdate = false;
        }

    }
}
