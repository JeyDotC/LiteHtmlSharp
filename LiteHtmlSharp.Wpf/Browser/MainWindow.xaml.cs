﻿using System;
using System.Windows;
using System.Windows.Input;
//using System.Windows.Shapes;
using System.Diagnostics;
using LiteHtmlSharp;
using System.IO;

namespace Browser
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window, IResourceLoader
   {
      string _baseURL;
      Stopwatch _watch = new Stopwatch();
      HTMLVisual _htmlVisual;
      public MainWindow()
      {
         InitializeComponent();

         string css = System.IO.File.ReadAllText("master.css");
         _htmlVisual = new HTMLVisual(canvas, css, this);
      }

      private void button_Click(object sender, RoutedEventArgs e)
      {
         if (_htmlVisual.HTMLLoaded)
         {
            Draw();
         }
         else
         {
            Render(@"<html><head></head><body><div style='width:100px; height:100px; background-color:red'><input type=text value='HELLO WORLD'/></div></body></html>");
         }
      }

      private void btClear_Click(object sender, RoutedEventArgs e)
      {
         _htmlVisual.Clear();
         lbTime.Content = string.Empty;
      }

      private void btLoad_Click(object sender, RoutedEventArgs e)
      {
         Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
         dlg.DefaultExt = "html";
         dlg.Filter = "HTML files |*.html";

         if (dlg.ShowDialog() == true)
         {
            string html = System.IO.File.ReadAllText(dlg.FileName);
            WPFContainer.BaseURL = System.IO.Path.GetDirectoryName(dlg.FileName);
            _baseURL = WPFContainer.BaseURL;
            Render(html);
         }
      }

      private void Draw()
      {
         _watch.Restart();
         _htmlVisual.Draw();
         _watch.Stop();
         lbTime.Content = "Render Time(ms): " + _watch.ElapsedMilliseconds;
      }

      private void Render(string html)
      {
         _watch.Restart();
         _htmlVisual.Render(html);
         _watch.Stop();
         lbTime.Content = "Render Time(ms): " + _watch.ElapsedMilliseconds;
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         _htmlVisual.OnSizeChanged();
      }

      public byte[] GetResourceBytes(string resource)
      {
         var file = GetAbsoluteFile(resource);
         if (File.Exists(file))
         {
            return File.ReadAllBytes(file);
         }

         return new byte[0];
      }

      public string GetResourceString(string resource)
      {
         var file = GetAbsoluteFile(resource);
         if (File.Exists(file))
         {
            return File.ReadAllText(file);
         }

         return string.Empty;
      }

      private string GetAbsoluteFile(string file)
      {
         Uri uri;
         if (!Uri.TryCreate(file, UriKind.Absolute, out uri))
         {
            var fullpath = Path.Combine(_baseURL, file);
            uri = new Uri(fullpath);
         }

         return uri.LocalPath;
      }
   }
}