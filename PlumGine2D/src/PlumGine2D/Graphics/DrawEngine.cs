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
			List<Texture2D> textures = new List<Texture2D>();
			List<Vector2> positions = new List<Vector2>();

			foreach (Viewport v in viewports)
			{
				// count scale, pos and realScreenResolution based on viewport
				Vector2 screenSize = new Vector2(v.rectOnScreen.Width, 
					                     v.rectOnScreen.Height) * resScale;

				v.AddRenderTarget(graphics.GraphicsDevice, 
					new Point((int)screenSize.X, (int)screenSize.Y));
				graphics.GraphicsDevice.SetRenderTarget(v.GetTarget());

				spriteBatch.Begin();

				// draw all extensions
				foreach (DrawEngineExt dee in extensions)
				{
					dee.Draw(spriteBatch, v.drawScale * v.scale, v.centerMapPos,
						screenSize, v.rectOnMap);
				}

				spriteBatch.End();

				textures.Add(v.GetTarget());

				Vector2 position = new Vector2(v.rectOnScreen.X, v.rectOnScreen.Y)
				                   * resScale;
				positions.Add(position);
			}

			graphics.GraphicsDevice.SetRenderTarget(null);
			graphics.GraphicsDevice.Clear(Color.Pink);
			spriteBatch.Begin();

			for (int i = 0; i < textures.Count; ++i)
			{
				spriteBatch.Draw(textures[i], positions[i]);
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

