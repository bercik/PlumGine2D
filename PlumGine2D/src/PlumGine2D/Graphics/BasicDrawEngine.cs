using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;

namespace PlumGine2D.Graphics
{
	public class BasicDrawEngine : DrawEngineExt
	{
		public BasicDrawEngine(Engine engine) : base(engine)
		{
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 scale)
		{
			Vector2 leftTopPos = engine.pos -
				new Vector2(engine.realScreenResolution.X * scale.X * 0.5f, engine.realScreenResolution.Y * scale.Y * 0.5f);
			Rectangle logicScreen = new Rectangle((int)(leftTopPos.X), (int)(leftTopPos.Y),
				(int)(engine.realScreenResolution.X / scale.X), (int)(engine.realScreenResolution.Y / scale.Y));

			List<Chunk> chunks = engine.chunkManager.getChunksInRect(logicScreen);

			for (int i = 0; i < chunks.Count; ++i)
			{
				List<IDrawObject> drawObjects = chunks[i].getObjects<IDrawObject>();
				for (int j = 0; j < drawObjects.Count; ++j)
				{
					drawObjects[j].Draw(spriteBatch, engine.pos, engine.realScreenResolution, scale);
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
		}
	}
}

