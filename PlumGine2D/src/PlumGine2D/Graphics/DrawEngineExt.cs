using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlumGine2D.Graphics
{
	public abstract class DrawEngineExt
	{
		protected Engine engine;

		public DrawEngineExt(Engine engine)
		{
			this.engine = engine;
		}

		public abstract void Draw(SpriteBatch spriteBatch, Vector2 scale);

		public abstract void Update(GameTime gameTime);
	}
}

