﻿using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var container = new Ninject.StandardKernel();
//	        container.Bind<MainForm>().To<MainForm>();
	        container.Bind<IUiAction>().To<SaveImageAction>();
	        container.Bind<IUiAction>().To<DragonFractalAction>();
	        container.Bind<IUiAction>().To<KochFractalAction>();
	        container.Bind<IUiAction>().To<ImageSettingsAction>();
	        container.Bind<IUiAction>().To<PaletteSettingsAction>();
	        container.Bind<Palette>().ToSelf().InSingletonScope();
	        container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
	        container.Bind<KochPainter>().ToSelf().InSingletonScope();

			try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
	            Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}