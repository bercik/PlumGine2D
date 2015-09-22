using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PlumGine2D
{
	public class DrawEngine : EngineExt
	{
		private Vector2 scale;

		private List<Rectangle> viewports = new List<Rectangle>();

		public DrawEngine(Engine engine) : base(engine)
		{
			this.scale = new Vector2((float)engine.realScreenResolution.X / (float)engine.logicScreenResolution.X,
				(float)engine.realScreenResolution.Y / (float)engine.logicScreenResolution.Y);
		}

		public void AddViewport(Rectangle viewport)
		{
			viewports.Add(viewport);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			List<Chunk> chunks = engine.chunkManager.getChunks(engine.pos);

			spriteBatch.Begin();

			for (int i = 0; i < chunks.Count; ++i)
			{
				List<IDrawObject> drawObjects = chunks[i].getObjects<IDrawObject>();
				for (int j = 0; j < drawObjects.Count; ++j)
				{
					Vector2 halfRealScreenResolution = 
						new Vector2(engine.realScreenResolution.X * 0.5f, engine.realScreenResolution.Y * 0.5f);
					Vector2 pos = drawObjects[j].getPosLeftTop() - engine.pos;
					pos *= scale * engine.scale;
					pos += halfRealScreenResolution;
					spriteBatch.Draw(drawObjects[j].getTexture(), pos, null,
						Color.White, 0, Vector2.Zero, scale * engine.scale, SpriteEffects.None, 0);
				}
			}

			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
		}
	}
}

