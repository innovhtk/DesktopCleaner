using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace DesktopCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            string desktop = @"C:\Users\Eleazar\Desktop";
            string workshop = @"C:\Users\Eleazar\Desktop\Workshop";
            //Leer arvhicos en el escritroio
            //Generar lista de archivos
            List<string> files = Directory.EnumerateFiles(desktop).ToList();
            List<string> WSfiles = Directory.EnumerateFiles(workshop).ToList();
            //preguntar si existe carpeta de trabajo
            List<string> directories = Directory.EnumerateDirectories(desktop).ToList();
            List<string> WSdirectories = Directory.EnumerateDirectories(workshop).ToList();
            string workshopFind;
            if ((workshopFind = directories.Find(dir => dir == workshop)) != null)
            {
                directories.Remove(workshopFind);
            }
            else
            {
                //si no existe, crearlo
                Directory.CreateDirectory(workshop);
            }
            //Mandar todos los activos a carperta de trabajo
            foreach (string file in files)
            {
                string[] fileparts = file.Split('\\');
                try
                {
                    if (WSfiles.Find(fl => fl == workshop+"\\"+fileparts[fileparts.Count()-1])!=null)
                    {
                        File.Move(file, workshopFind +"\\" + fileparts[fileparts.Count() - 1]+"(New)");
                    }
                    else
                    {
                        File.Move(file, workshopFind +"\\" + fileparts[fileparts.Count() - 1]);
                    }
                }
                catch (Exception) { }
                
            }
            foreach (string dir in directories)
            {
                string[] dirparts = dir.Split('\\');
                try
                {
                    if (WSdirectories.Find(dr => dr == @"C:\Users\Eleazar\Desktop\Workshop\" +  dirparts[dirparts.Count() - 1])!=null)
                    {
                        Directory.Move(@"C:\Users\Eleazar\Desktop\" + dirparts[dirparts.Count() - 1], @"C:\Users\Eleazar\Desktop\Workshop\" + dirparts[dirparts.Count() - 1] + "(New)");
                    }
                    else
                    {
                        Directory.Move(@"C:\Users\Eleazar\Desktop\"+ dirparts[dirparts.Count() - 1], @"C:\Users\Eleazar\Desktop\Workshop\" + dirparts[dirparts.Count() - 1]);
                    }
                    
                }
                catch (Exception) { }
                
            }
           
        }
    }
}
