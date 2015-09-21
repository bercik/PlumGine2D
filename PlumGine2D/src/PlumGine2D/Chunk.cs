using System;
using System.Collections.Generic;

namespace PlumGine2D
{
	public class Chunk
	{
		private List<IDrawObject> drawObjects = new List<IDrawObject>();

		public Chunk()
		{
		}

		public List<IDrawObject> getDrawObjects()
		{
			return drawObjects;
		}

		public void addGameObject(IGameObject obj)
		{
			if (obj is IDrawObject)
			{
				drawObjects.Add((IDrawObject)obj);
			}
		}
	}
}

