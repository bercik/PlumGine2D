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

		private int[] Range(int min, int max)
		{
			int[] result = new int[max - min + 1];

			for (int i = min, j = 0; i <= max; ++i, ++j)
			{
				result[j] = i;
			}

			return result;
		}

		public List<Chunk> getChunksInRect(Rectangle rect)
		{
			List<Chunk> result = new List<Chunk>();

			int xMin = rect.Left / engine.logicScreenResolution.X - 1;
			int xMax = rect.Right / engine.logicScreenResolution.X + 1;
			int[] xs = Range(xMin, xMax);

			int yMin = rect.Top / engine.logicScreenResolution.Y - 1;
			int yMax = rect.Bottom / engine.logicScreenResolution.Y + 1;
			int[] ys = Range(yMin, yMax);

			for (int i = 0; i < xs.Length; ++i)
			{
				for (int j = 0; j < ys.Length; ++j)
				{
					int x = xs[i];
					int y = ys[j];

					if (x >= 0 && x < engine.mapSize.X
						&& y >= 0 && y < engine.mapSize.Y)
					{
						result.Add(chunks[x, y]);
					}
				}
			}

			return result;
		}
	}
}

