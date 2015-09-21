using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PlumGine2D
{
	public class Engine
	{
		private Point logicScreenResolution;
		private Point realScreenResolution;
		private Point mapSize;
		private Vector2 scale;

		private Vector2 pos;

		private ChunkManager chunkManager;

		// logicScreenResolution - logiczna rozdzielczość ekranu
		// realScreenResolution - realna rozdzielczość ekranu
		// przykładowo, jeżeli logicScreenResolution=(1600, 900)
		// 					   realScreenResolution=(1920, 1080)
		// to każdy obiekt podczas wyświetlania będzie przeskalowany o
		// (1920/1600, 1080/900)
		//
		// grę powinno tworzyć się pod konkretną logiczną rozdzielczość ekranu
		// włącznie z rozmiarami tekstur. Potem tylko w zależności od ekranu wystarczy
		// zmieniać realna rozdzielczość ekranu
		//
		// mapSize - rozmiar mapy jako wielokrotność logicScreenSize tzn. jeżeli
		// mapSize=(10, 10), a logicScreenResolution=(1600, 900) to rozmiar mapy
		// w pikselach wyniesie (16000, 9000)
		public Engine(Point logicScreenResolution, Point realScreenResolution,
			Point mapSize,
			bool fullscreen, GraphicsDeviceManager graphics)
		{
			this.logicScreenResolution = logicScreenResolution;
			this.realScreenResolution = realScreenResolution;
			this.mapSize = mapSize;

			graphics.IsFullScreen = fullscreen;
			graphics.PreferredBackBufferWidth = realScreenResolution.X;
			graphics.PreferredBackBufferHeight = realScreenResolution.Y;

			scale = new Vector2((float)realScreenResolution.X / (float)logicScreenResolution.X,
				(float)realScreenResolution.Y / (float)logicScreenResolution.Y);

			chunkManager = new ChunkManager(logicScreenResolution, mapSize);
		}

		public void addGameObject(IGameObject obj)
		{
			chunkManager.addGameObject(obj);
		}

		public void setView(float x, float y)
		{
			pos = new Vector2(x, y);
		}

		public void draw(SpriteBatch spriteBatch)
		{
			List<Chunk> chunks = chunkManager.getChunks(pos);

			spriteBatch.Begin();

			for (int i = 0; i < chunks.Count; ++i)
			{
				List<IDrawObject> drawObjects = chunks[i].getDrawObjects();
				for (int j = 0; j < drawObjects.Count; ++j)
				{
					spriteBatch.Draw(drawObjects[j].getTexture(), drawObjects[j].getPos());
				}
			}

			spriteBatch.End();
		}
	}
}

