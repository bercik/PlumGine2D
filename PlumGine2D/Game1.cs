#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using PlumGine2D.Graphics;
using PlumGine2D.Utils;

#endregion

/* HOW TO ADD DYNAMIC LIGHT
// turning on the light effect
lightEffect.CurrentTechnique = lightEffect.Techniques["Light"];
lightEffect.CurrentTechnique.Passes[0].Apply();

spriteBatch.Draw(scene, new Vector2(0, 0), Color.White);

spriteBatch.End();
 * */

namespace PlumGine2D
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		private bool v1 = true;
		PlumGine2D.Graphics.Viewport v;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;

		Engine engine;
		DrawEngine drawEngine;
		PlumGine2D.Graphics.Viewport viewport1;
		PlumGine2D.Graphics.Viewport viewport2;
		FrameCounter frameCounter;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";	            

			engine = new Engine(new Point(1600, 900), new Point(10, 10));
			drawEngine = new DrawEngine(engine, new Vector2(1600.0f, 900.0f), 
				new Vector2(1600.0f, 200.0f), false, graphics);
			drawEngine.AddExtension(new BasicDrawEngine(drawEngine));
			viewport1 = new PlumGine2D.Graphics.Viewport(
				new Rectangle(0, 0, 800, 900), new Point(1600, 900));
			viewport2 = new PlumGine2D.Graphics.Viewport(
				new Rectangle(800, 0, 800, 900), new Point(800, 900));

			drawEngine.AddViewport(viewport1);
			drawEngine.AddViewport(viewport2);

			engine.AddEngineExt(drawEngine);
			frameCounter = new FrameCounter();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			base.Initialize();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: use this.Content to load your game content here 
			Texture2D ballTexture = Content.Load<Texture2D>("ball");
			IGameObject ballObject = new DrawObject(ballTexture, 1601.0f, 901.0f);
			engine.addGameObject(ballObject);

			viewport1.centerMapPos = ballObject.getPosCenter();

			Texture2D carTexture = Content.Load<Texture2D>("car");
			IGameObject carObject = new DrawObject(carTexture, 700.0f, 1400.0f);
			engine.addGameObject(carObject);
			carObject = new DrawObject(carTexture, 1400.0f, 2200.0f);
			engine.addGameObject(carObject);

			viewport2.centerMapPos = carObject.getPosCenter();

			// DOESN'T WORK!
			font = Content.Load<SpriteFont>("font");
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}
			#endif
			// TODO: Add your update logic here
			if (v1)
			{
				v = viewport1;
			}
			else
			{
				v = viewport2;
			}

			KeyboardState state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Left))
			{
				v.centerMapPos = v.centerMapPos +
				new Vector2(-20.0f, 0.0f);
			}
			if (state.IsKeyDown(Keys.Right))
			{
				v.centerMapPos = v.centerMapPos +
				new Vector2(20.0f, 0.0f);
			}
			if (state.IsKeyDown(Keys.Up))
			{
				v.centerMapPos = v.centerMapPos +
				new Vector2(0.0f, -20.0f);
			}
			if (state.IsKeyDown(Keys.Down))
			{
				v.centerMapPos = v.centerMapPos +
				new Vector2(0.0f, 20.0f);
			}
			if (state.IsKeyDown(Keys.OemPlus))
			{
				if (v.scale < 3.0f)
				{
					v.scale = v.scale * 1.01f;
				}
			}
			if (state.IsKeyDown(Keys.OemMinus))
			{
				if (v.scale > 0.3f)
				{
					v.scale = v.scale * 0.99f;
				}
			}

			if (state.IsKeyDown(Keys.Space))
			{
				v1 = !v1;
			}

			frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
			//TODO: Add your drawing code here
			engine.Draw(spriteBatch);
            
			// draw some information
			spriteBatch.Begin();
			string fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);
			spriteBatch.DrawString(font, fps, new Vector2(10.0f, 10.0f), Color.White);
			string x = string.Format("x: {0}", v.centerMapPos.X);
			spriteBatch.DrawString(font, x, new Vector2(10.0f, 30.0f), Color.White);
			string y = string.Format("y: {0}", v.centerMapPos.Y);
			spriteBatch.DrawString(font, y, new Vector2(10.0f, 50.0f), Color.White);
			string scale = string.Format("scale: {0}", v.scale);
			spriteBatch.DrawString(font, scale, new Vector2(10.0f, 70.0f), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}

