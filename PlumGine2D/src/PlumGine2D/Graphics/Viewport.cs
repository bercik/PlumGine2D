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
		public Point originalRectOnMapSize { get; set; }

		public Rectangle rectOnMap { get; private set; }

		private RenderTarget2D target = null;

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
				Vector2 newSize = new Vector2(originalRectOnMapSize.X / scale,
					                  originalRectOnMapSize.Y / scale);
				Vector2 halfNewSize = newSize * 0.5f;
				Vector2 centerPos = 
					new Vector2(_centerMapPos.X, _centerMapPos.Y);
				centerPos -= halfNewSize;

				rectOnMap = new Rectangle((int)centerPos.X, (int)centerPos.Y,
					(int)newSize.X, (int)newSize.Y);
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

		public Vector2 drawScale { get; private set; }

		public Viewport(Rectangle rectOnScreen, Point rectOnMapSize, 
		                Vector2 centerMapPos = default(Vector2))
		{
			this.rectOnScreen = rectOnScreen;
			this.originalRectOnMapSize = new Point(rectOnMapSize.X, rectOnMapSize.Y);
			this.centerMapPos = centerMapPos;
			this.scale = 1.0f;

			this.drawScale = new Vector2(
				(float)rectOnScreen.Width / (float)rectOnMapSize.X,
				(float)rectOnScreen.Height / (float)rectOnMapSize.Y);				
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
