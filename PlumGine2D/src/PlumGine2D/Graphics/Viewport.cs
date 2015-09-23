using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D.Graphics
{
	public class Viewport
	{
		public Rectangle rectOnScreen { get; private set; }

		public Rectangle rectOnMap { get; private set; }

		public float scale { get; private set; }

		public SpriteBatch spriteBatch { get; private set; }

		public Viewport(Rectangle rectOnScreen, Vector2 posOnMap, float scale, SpriteBatch spriteBatch)
		{
			this.rectOnScreen = rectOnScreen;
			//this.posOnMap = posOnMap;
			this.scale = scale;
			this.spriteBatch = spriteBatch;
		}

		// center of map
		public Vector2 GetPos()
		{
			throw new NotImplementedException();
		}

		public Vector2 GetScale()
		{
			throw new NotImplementedException();
		}

		public Rectangle GetSize()
		{
			throw new NotImplementedException();
		}
	}
}
