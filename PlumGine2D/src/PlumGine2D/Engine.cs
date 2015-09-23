using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PlumGine2D
{
	public class Engine
	{
		public ChunkManager chunkManager { get; private set; }

		// list of extensions engines
		List<EngineExt> engineExtensions = new List<EngineExt>();

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
		public Engine(Point chunkSize, Point numberOfChunks)
		{
			chunkManager = new ChunkManager(this, chunkSize, numberOfChunks);
		}

		public void AddEngineExt(EngineExt engineExt)
		{
			engineExtensions.Add(engineExt);
		}

		public void addGameObject(IGameObject obj)
		{
			chunkManager.addGameObject(obj);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (EngineExt ee in engineExtensions)
			{
				ee.Draw(spriteBatch);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (EngineExt ee in engineExtensions)
			{
				ee.Update(gameTime);
			}
		}
	}
}

