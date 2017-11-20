using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	

	public class KochFractalAction : IUiAction
	{
		public KochFractalAction(KochPainter kochPainter)
		{
			this.kochPainter = kochPainter;
//			this.imageHolder = imageHolder;
//			this.palette = palette;
		}

//		private IImageHolder imageHolder;
//		private Palette palette;
		private KochPainter kochPainter;

		public string Category => "Фракталы";
		public string Name => "Кривая Коха";
		public string Description => "Кривая Коха";

		public void Perform()
		{
//			var container = new StandardKernel();
//			container.Bind<IImageHolder>().ToConstant(imageHolder);
//			container.Bind<Palette>().ToConstant(palette);
			kochPainter.Paint();
//			container.Get<KochPainter>().Paint();
		}
	}
}