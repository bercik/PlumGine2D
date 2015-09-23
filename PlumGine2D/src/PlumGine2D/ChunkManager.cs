using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PlumGine2D
{
	public class ChunkManager
	{
		private Chunk[,] chunks;

		private Point chunkSize;
		private Point numberOfChanks;

		public ChunkManager(Engine engine, Point chunkSize, Point numberOfChanks)
		{
			this.chunkSize = chunkSize;
			this.numberOfChanks = numberOfChanks;

			this.chunks = new Chunk[numberOfChanks.X, numberOfChanks.Y];
			for (int x = 0; x < numberOfChanks.X; ++x)
			{
				for (int y = 0; y < numberOfChanks.Y; ++y)
				{
					chunks[x, y] = new Chunk();
				}
			}
		}

		public void addGameObject(IGameObject obj)
		{
			if (obj.getSize().X > chunkSize.X
				|| obj.getSize().Y > chunkSize.Y)
			{
				throw new ArgumentException(
					"Object width and height must be less than logicScreenResolution");
			}

			int x = (int)obj.getPosLeftTop().X / chunkSize.X;
			int y = (int)obj.getPosLeftTop().Y / chunkSize.Y;

			if (x < 0 || x > numberOfChanks.X ||
				y < 0 || y > numberOfChanks.Y)
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

			int xMin = rect.Left / chunkSize.X - 1;
			int xMax = rect.Right / chunkSize.X + 1;
			int[] xs = Range(xMin, xMax);

			int yMin = rect.Top / chunkSize.Y - 1;
			int yMax = rect.Bottom / chunkSize.Y + 1;
			int[] ys = Range(yMin, yMax);

			for (int i = 0; i < xs.Length; ++i)
			{
				for (int j = 0; j < ys.Length; ++j)
				{
					int x = xs[i];
					int y = ys[j];

					if (x >= 0 && x < numberOfChanks.X
						&& y >= 0 && y < numberOfChanks.Y)
					{
						result.Add(chunks[x, y]);
					}
				}
			}

			return result;
		}
	}
}

