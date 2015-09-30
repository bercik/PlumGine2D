using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D.Graphics
{
	public class BasicDrawEngine : DrawEngineExt
	{
		public BasicDrawEngine(DrawEngine drawEngine) : base(drawEngine)
		{
		}

		public override void LoadContent(ContentManager content)
		{
		}

		public override void Draw(SpriteBatch spriteBatch, Vector2 scale, 
		                          Vector2 centerScreenPos, Vector2 screenSize)
		{
			Vector2 mapSizeVector = screenSize / scale;
			Vector2 halfMapSizeVector = mapSizeVector * 0.5f;
			Rectangle mapRect = new Rectangle(
				                    (int)(centerScreenPos.X - halfMapSizeVector.X),
				                    (int)(centerScreenPos.Y - halfMapSizeVector.Y), 
				                    (int)mapSizeVector.X, (int)mapSizeVector.Y);

			List<Chunk> chunks = drawEngine.engine.chunkManager.getChunksInRect(mapRect);

			for (int i = 0; i < chunks.Count; ++i)
			{
				List<IDrawObject> drawObjects = chunks[i].getObjects<IDrawObject>();
				for (int j = 0; j < drawObjects.Count; ++j)
				{
					drawObjects[j].Draw(spriteBatch, centerScreenPos,
						screenSize, scale);
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
		}
	}
}

