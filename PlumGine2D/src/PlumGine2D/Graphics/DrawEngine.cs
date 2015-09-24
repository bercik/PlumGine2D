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

		public float scale { get; private set; }

		public Vector2 resScale { get; private set; }

		public Vector2 pos { get; private set; }

		private GraphicsDeviceManager graphics;

		private List<DrawEngineExt> extensions = new List<DrawEngineExt>();

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
			if (this.resScale.X > this.resScale.Y)
			{
				this.resScale = new Vector2(this.resScale.Y, this.resScale.Y);
			}
			else
			{
				this.resScale = new Vector2(this.resScale.X, this.resScale.X);
			}
			this.scale = 1.0f;
		}

		public void AddExtension(DrawEngineExt ext)
		{
			extensions.Add(ext);
		}

		public void SetView(Vector2 newPos)
		{
			this.pos = newPos;
		}

		public void SetScale(float newScale)
		{
			this.scale = newScale;
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

			foreach (DrawEngineExt dee in extensions)
			{
				dee.Draw(spriteBatch, resScale * scale, pos, realScreenResolution);
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

