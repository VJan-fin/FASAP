using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmetkaZaNaracka
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // plink odnadvor@bazi.finki.ukim.mk -ssh -pw B2eN66Rq3GXE7T8R -C -N -2 -L 1620:bazi.finki.ukim.mk:1521
            Application.Run(new FasapPocetenEkran());
          
            //Application.Run(new PregledMeni());

            //Application.Run(new Izv1PridonesVoPromet());
            //Application.Run(new FasapPoceten());


            //Application.Run(new PregledKvartalnaSostojba());
            //Application.Run(new Izv1PridonesVoPromet());

        }
    }
}