using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace PlumGine2D
{
	public class Engine
	{
		public Point logicScreenResolution { get; private set; }
		public Point realScreenResolution { get; private set; }
		public Point mapSize { get; private set; }

		public Vector2 pos { get; private set; }
		public float scale { get; private set; }

		public ChunkManager chunkManager { get; private set; }

		// extensions engines
		DrawEngine drawEngine;

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

			this.scale = 1.0f;

			graphics.IsFullScreen = fullscreen;
			graphics.PreferredBackBufferWidth = realScreenResolution.X;
			graphics.PreferredBackBufferHeight = realScreenResolution.Y;

			chunkManager = new ChunkManager(this);

			drawEngine = new DrawEngine(this);

			setView(new Vector2(0.0f, 0.0f));
		}

		public void addGameObject(IGameObject obj)
		{
			chunkManager.addGameObject(obj);
		}

		public void setView(Vector2 pos)
		{
			this.pos = pos;
		}

		public void setScale(float scale)
		{
			this.scale = scale;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			drawEngine.Draw(spriteBatch);
		}
	}
}

