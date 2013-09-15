using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace SlideShow
{
  class Program
  {
    static void Main(string[] args)
    {
      var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var items = Directory.GetFiles(dir, "*.jpg").ToList();
      items.AddRange(Directory.GetFiles(dir, "*.jpeg"));
      items.AddRange(Directory.GetFiles(dir, "*.gif"));
      items.AddRange(Directory.GetFiles(dir, "*.png"));

      using (var sw = new StreamWriter(Path.Combine(dir, "dir.js"), false))
      {
        sw.WriteLine("var IMAGES = [");
        foreach (var item in items)
          sw.WriteLine("  \"{0}\",", Path.GetFileName(item)); //seems we don't have to worry about the extra comma
        sw.WriteLine("];");
      }
    }
  }
}
