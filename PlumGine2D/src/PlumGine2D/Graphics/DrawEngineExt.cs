using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D.Graphics
{
	public abstract class DrawEngineExt
	{
		protected DrawEngine drawEngine;

		public DrawEngineExt(DrawEngine drawEngine)
		{
			this.drawEngine = drawEngine;
		}

		public abstract void LoadContent(ContentManager content);

		public abstract void Draw(SpriteBatch spriteBatch, Vector2 scale, 
		                          Vector2 centerScreenPos, Vector2 screenSize);

		public abstract void Update(GameTime gameTime);
	}
}

