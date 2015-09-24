using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D.Graphics
{
	public class DrawEngine : EngineExt
	{
		public Vector2 logicScreenResolution { get; private set; }

		public Vector2 realScreenResolution { get; private set; }

		public Vector2 resScale { get; private set; }

		private GraphicsDeviceManager graphics;

		private List<DrawEngineExt> extensions = new List<DrawEngineExt>();

		private List<Viewport> viewports = new List<Viewport>();

		public DrawEngine(Engine engine, 
		                  Vector2 logicScreenResolution, Vector2 realScreenResolution,
		                  bool fullscreen, GraphicsDeviceManager graphics) : base(engine)
		{
			this.graphics = graphics;
			graphics.IsFullScreen = fullscreen;
			graphics.PreferredBackBufferWidth = (int)realScreenResolution.X;
			graphics.PreferredBackBufferHeight = (int)realScreenResolution.Y;

			this.logicScreenResolution = logicScreenResolution;
			this.realScreenResolution = realScreenResolution;

			this.resScale = new Vector2(realScreenResolution.X / logicScreenResolution.X,
				realScreenResolution.Y / logicScreenResolution.Y);
		}

		public void AddViewport(Viewport v)
		{
			viewports.Add(v);
		}

		public void AddExtension(DrawEngineExt ext)
		{
			extensions.Add(ext);
		}

		public virtual void LoadContent(ContentManager content)
		{
			foreach (DrawEngineExt dee in extensions)
			{
				dee.LoadContent(content);
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			foreach (Viewport v in viewports)
			{
				// count scale, pos and realScreenResolution based on viewport
				Vector2 screenSize = new Vector2(v.rectOnScreen.Width, 
					                     v.rectOnScreen.Height) * resScale;

				Vector2 mapSize = new Vector2(v.rectOnMap.Width, v.rectOnMap.Height);
				Vector2 scale = screenSize / mapSize;
				if (scale.X < scale.Y)
				{
					scale = new Vector2(scale.X, scale.X);
				}
				else
				{
					scale = new Vector2(scale.Y, scale.Y);
				}

				// draw all extensions
				foreach (DrawEngineExt dee in extensions)
				{
					dee.Draw(spriteBatch, scale, v.centerMapPos,
						screenSize);
				}
			}

			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (DrawEngineExt dee in extensions)
			{
				dee.Update(gameTime);
			}
		}
	}
}

