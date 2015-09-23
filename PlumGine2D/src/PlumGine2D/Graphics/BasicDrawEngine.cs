using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
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
			List<Chunk> chunks = engine.chunkManager.getChunks(engine.pos);

			for (int i = 0; i < chunks.Count; ++i)
			{
				List<IDrawObject> drawObjects = chunks[i].getObjects<IDrawObject>();
				for (int j = 0; j < drawObjects.Count; ++j)
				{
					drawObjects[j].Draw(spriteBatch, engine.pos, engine.realScreenResolution, scale);
				}
			}
		}

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
		}
	}
}

