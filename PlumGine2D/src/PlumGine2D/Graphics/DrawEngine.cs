using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PlumGine2D.Graphics
{
	public class DrawEngine : EngineExt
	{
		private Vector2 scale;

		private List<DrawEngineExt> extensions = new List<DrawEngineExt>();

		public DrawEngine(Engine engine) : base(engine)
		{
			this.scale = new Vector2((float)engine.realScreenResolution.X / (float)engine.logicScreenResolution.X,
				(float)engine.realScreenResolution.Y / (float)engine.logicScreenResolution.Y);
		}

		public void AddExtension(DrawEngineExt ext)
		{
			extensions.Add(ext);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			foreach (DrawEngineExt dee in extensions)
			{
				dee.Draw(spriteBatch, engine.scale * scale);
			}

			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (DrawEngineExt dee in extensions)
			{
				dee.Update(gameTime);
			}
		}
	}
}

