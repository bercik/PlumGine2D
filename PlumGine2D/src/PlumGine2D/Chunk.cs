using System;
using System.Collections.Generic;

namespace PlumGine2D
{
	public class Chunk
	{
		private List<IGameObject> gameObjects = new List<IGameObject>();

		public Chunk()
		{
		}

		public List<IDrawObject> getDrawObjects()
		{
			List<IDrawObject> drawObjects = new List<IDrawObject>();

			for (int i = 0; i < gameObjects.Count; ++i)
			{
				if (gameObjects[i] is IDrawObject)
				{
					drawObjects.Add((IDrawObject)gameObjects[i]);
				}
			}

			return drawObjects;
		}

		public void addGameObject(IGameObject obj)
		{
			gameObjects.Add(obj);
		}
	}
}

