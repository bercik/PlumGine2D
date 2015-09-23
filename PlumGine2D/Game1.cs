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
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;

		Engine engine;
		DrawEngine drawEngine;
		FrameCounter frameCounter;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";	            

			engine = new Engine(new Point(1600, 900), new Point(10, 10));
			drawEngine = new DrawEngine(engine, new Vector2(1600.0f, 900.0f), 
				new Vector2(1280.0f, 720.0f), false, graphics);
			drawEngine.AddExtension(new BasicDrawEngine(drawEngine));
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

			drawEngine.SetView(ballObject.getPosCenter());

			Texture2D carTexture = Content.Load<Texture2D>("car");
			IGameObject carObject = new DrawObject(carTexture, 700.0f, 1400.0f);
			engine.addGameObject(carObject);
			carObject = new DrawObject(carTexture, 1400.0f, 2200.0f);
			engine.addGameObject(carObject);

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
			KeyboardState state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Left))
			{
				drawEngine.SetView(drawEngine.pos + new Vector2(-40.0f, 0.0f));
			}
			if (state.IsKeyDown(Keys.Right))
			{
				drawEngine.SetView(drawEngine.pos + new Vector2(40.0f, 0.0f));
			}
			if (state.IsKeyDown(Keys.Up))
			{
				drawEngine.SetView(drawEngine.pos + new Vector2(0.0f, -40.0f));
			}
			if (state.IsKeyDown(Keys.Down))
			{
				drawEngine.SetView(drawEngine.pos + new Vector2(0.0f, 40.0f));
			}
			if (state.IsKeyDown(Keys.OemPlus))
			{
				if (drawEngine.scale < 3.0f)
				{
					drawEngine.SetScale(drawEngine.scale * 1.01f);
				}
			}
			if (state.IsKeyDown(Keys.OemMinus))
			{
				if (drawEngine.scale > 0.3f)
				{
					drawEngine.SetScale(drawEngine.scale * 0.99f);
				}
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
			string x = string.Format("x: {0}", drawEngine.pos.X);
			spriteBatch.DrawString(font, x, new Vector2(10.0f, 30.0f), Color.White);
			string y = string.Format("y: {0}", drawEngine.pos.Y);
			spriteBatch.DrawString(font, y, new Vector2(10.0f, 50.0f), Color.White);
			string scale = string.Format("scale: {0}", drawEngine.scale);
			spriteBatch.DrawString(font, scale, new Vector2(10.0f, 70.0f), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}

