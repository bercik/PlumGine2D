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

		public List<T> getObjects<T>()
		{
			List<T> drawObjects = new List<T>();

			for (int i = 0; i < gameObjects.Count; ++i)
			{
				if (gameObjects[i] is T)
				{
					drawObjects.Add((T)gameObjects[i]);
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

