using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PlumGine2D
{
	public class ChunkManager
	{
		private Point logicScreenResolution;
		private Point mapSize;

		private Chunk[,] chunks;

		public ChunkManager(Point logicScreenResolution, Point mapSize)
		{
			this.logicScreenResolution = logicScreenResolution;
			this.mapSize = mapSize;

			this.chunks = new Chunk[mapSize.X, mapSize.Y];
			for (int x = 0; x < mapSize.X; ++x)
			{
				for (int y = 0; y < mapSize.Y; ++y)
				{
					chunks[x, y] = new Chunk();
				}
			}
		}

		public void addGameObject(IGameObject obj)
		{
			if (obj.getWidth() > logicScreenResolution.X
			    || obj.getHeight() > logicScreenResolution.Y)
			{
				throw new ArgumentException(
					"Object width and height must be less than logicScreenResolution");
			}

			int x = (int)obj.getX() / logicScreenResolution.X;
			int y = (int)obj.getX() / logicScreenResolution.Y;

			if (x < 0 || x > mapSize.X ||
			    y < 0 || y > mapSize.Y)
			{
				throw new ArgumentException("Object position exceed map size");
			}

			chunks[x, y].addGameObject(obj);
		}

		public List<Chunk> getChunks(Vector2 pos)
		{
			List<Chunk> result = new List<Chunk>(4);

			int x = (int)pos.X / logicScreenResolution.X;
			int y = (int)pos.Y / logicScreenResolution.Y;

			int[] xs = { x, x, x - 1, x - 1 };
			int[] ys = { y, y - 1, y, y - 1 };

			for (int i = 0; i < 4; ++i)
			{
				if (xs[i] >= 0 && xs[i] < mapSize.X
				    && ys[i] >= 0 && ys[i] < mapSize.Y)
				{
					result.Add(chunks[xs[i], ys[i]]);
				}
			}

			return result;
		}
	}
}

