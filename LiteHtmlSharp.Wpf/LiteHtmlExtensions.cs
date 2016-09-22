﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LiteHtmlSharp.Wpf
{
   public static class LiteHtmlExtensions
   {
      static Dictionary<string, Brush> _brushes = new Dictionary<string, Brush>();
      static Dictionary<string, Pen> _pens = new Dictionary<string, Pen>();

      public static Brush GetBrush(this web_color color)
      {
         string key = color.red.ToString() + color.green.ToString() + color.blue.ToString() + color.alpha.ToString();

         Brush result = null;
         if (!_brushes.TryGetValue(key, out result))
         {
            result = new SolidColorBrush(Color.FromArgb(color.alpha, color.red, color.green, color.blue));
            _brushes.Add(key, result);
         }

         return result;
      }

      public static Pen GetPen(this web_color color, double thickness)
      {
         string key = color.red.ToString() + color.green.ToString() + color.blue.ToString() + thickness;

         Pen result = null;
         if (!_pens.TryGetValue(key, out result))
         {
            Brush brush = color.GetBrush();
            result = new Pen(brush, thickness);
            _pens.Add(key, result);
         }

         return result;
      }

   }
}
