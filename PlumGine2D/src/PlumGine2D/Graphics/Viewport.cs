using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D.Graphics
{
	public class Viewport
	{
		// względem logicScreenResolution
		public Rectangle rectOnScreen { get; private set; }

		// względem logicScreenResolution
		private Rectangle originalRectOnMap { get; set; }

		public Rectangle rectOnMap { get; private set; }

		// względem rectOnMap
		private float _scale;

		public float scale
		{ 
			get
			{
				return _scale;
			}
			set
			{
				_scale = value;
				rectOnMap = new Rectangle((int)(originalRectOnMap.X / scale), 
					(int)(originalRectOnMap.Y / scale), 
					(int)(originalRectOnMap.Width / scale),
					(int)(originalRectOnMap.Height / scale));
			}
		}

		private Vector2 _centerMapPos;

		public Vector2 centerMapPos
		{
			get
			{
				return _centerMapPos;
			}
			set
			{
				_centerMapPos = value;
				Vector2 halfMapSize = new Vector2(rectOnMap.Width * 0.5f, 
					                      rectOnMap.Height * 0.5f);
				rectOnMap = new Rectangle((int)(_centerMapPos.X - halfMapSize.X), 
					(int)(_centerMapPos.Y - halfMapSize.Y), rectOnMap.Width,
					rectOnMap.Height);
			}
		}

		private RenderTarget2D target = null;

		public Viewport(Rectangle rectOnScreen, Rectangle rectOnMap)
		{
			this.rectOnScreen = rectOnScreen;
			this.originalRectOnMap = rectOnMap;
			this.scale = 1.0f;
		}

		public void AddRenderTarget(GraphicsDevice device, Point screenSize)
		{
			if (target == null)
			{
				target = new RenderTarget2D(device, screenSize.X, screenSize.Y);
			}
		}

		public RenderTarget2D GetTarget()
		{
			return target;
		}
	}
}
