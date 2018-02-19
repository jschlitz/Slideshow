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
      var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar;
      var items = Directory.GetFiles(dir, "*.jpg", SearchOption.AllDirectories).ToList();
      items.AddRange(Directory.GetFiles(dir, "*.jpeg", SearchOption.AllDirectories));
      items.AddRange(Directory.GetFiles(dir, "*.gif", SearchOption.AllDirectories));
      items.AddRange(Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories));

      //rename all the filesthat are samples
      const string SAM = "sample-";
      foreach (var item in items.Where(x => Path.GetFileName(x).StartsWith(SAM, StringComparison.CurrentCultureIgnoreCase)))
      {
        var fp = Path.GetFileName(item);
        var d = Path.GetDirectoryName(item);
        var newName = Path.Combine(d, fp.Substring(SAM.Length, fp.Length-SAM.Length));
        var oldName = item;
        if (!items.Contains(newName))
          File.Copy(oldName, newName);
        File.Delete(oldName);
      }

      using (var sw = new StreamWriter(Path.Combine(dir, "dir.js"), false))
      {
        sw.WriteLine("var IMAGES = [");
        foreach (var item in items)
          sw.WriteLine("  \"{0}\",", item.Substring(dir.Length).Replace(@"\", @"\\")); //seems we don't have to worry about the extra comma
        sw.WriteLine("];");
      }
    }
  }
}
