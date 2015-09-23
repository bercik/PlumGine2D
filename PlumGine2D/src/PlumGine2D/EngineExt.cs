using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlumGine2D
{
	public abstract class EngineExt
	{
		public Engine engine { get; private set; }

		public EngineExt(Engine engine)
		{
			this.engine = engine;
		}

		public abstract void Draw(SpriteBatch spriteBatch);

		public abstract void Update(GameTime gameTime);
	}
}

