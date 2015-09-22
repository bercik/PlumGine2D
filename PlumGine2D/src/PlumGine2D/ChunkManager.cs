using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PlumGine2D
{
	public class ChunkManager
	{
		private Engine engine;

		private Chunk[,] chunks;

		public ChunkManager(Engine engine)
		{
			this.engine = engine;

			this.chunks = new Chunk[engine.mapSize.X, engine.mapSize.Y];
			for (int x = 0; x < engine.mapSize.X; ++x)
			{
				for (int y = 0; y < engine.mapSize.Y; ++y)
				{
					chunks[x, y] = new Chunk();
				}
			}
		}

		public void addGameObject(IGameObject obj)
		{
			if (obj.getSize().X > engine.logicScreenResolution.X
				|| obj.getSize().Y > engine.logicScreenResolution.Y)
			{
				throw new ArgumentException(
					"Object width and height must be less than logicScreenResolution");
			}

			int x = (int)obj.getPosLeftTop().X / engine.logicScreenResolution.X;
			int y = (int)obj.getPosLeftTop().Y / engine.logicScreenResolution.Y;

			if (x < 0 || x > engine.mapSize.X ||
				y < 0 || y > engine.mapSize.Y)
			{
				throw new ArgumentException("Object position exceed map size");
			}

			chunks[x, y].addGameObject(obj);
		}

		public List<Chunk> getChunks(Vector2 pos)
		{
			List<Chunk> result = new List<Chunk>();

			int x = (int)pos.X / engine.logicScreenResolution.X;
			int y = (int)pos.Y / engine.logicScreenResolution.Y;

			int radius = 1;
			if (engine.scale < 1.0f)
			{
				radius = 2;
			}

			for (int i = -radius; i <= radius; ++i)
			{
				for (int j = -radius; j <= radius; ++j)
				{
					int tmpX = x + i;
					int tmpY = y + j;
					if (tmpX >= 0 && tmpX < engine.mapSize.X
						&& tmpY >= 0 && tmpY < engine.mapSize.Y)
					{
						result.Add(chunks[tmpX, tmpY]);
					}
				}
			}

			return result;
		}
	}
}

