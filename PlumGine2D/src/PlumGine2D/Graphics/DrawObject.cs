using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlumGine2D.Graphics
{
	public class DrawObject : IDrawObject
	{
		private Vector2 centerPos;
		private Vector2 size;
		private Texture2D texture;

		public DrawObject(Texture2D texture, float x, float y)
		{
			this.texture = texture;
			this.centerPos = new Vector2(x, y);
			this.size = new Vector2(texture.Width, texture.Height);
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 pos, Vector2 screenSize, 
		                 Vector2 scale)
		{
			Vector2 halfScreenSize = screenSize * 0.5f;
			Vector2 posToDraw = getPosLeftTop() - pos;
			posToDraw *= scale;
			posToDraw += halfScreenSize;
			spriteBatch.Draw(texture, posToDraw, null,
				Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
		}

		public Vector2 getPosLeftTop()
		{
			return (centerPos - (size * 0.5f));
		}

		public Vector2 getPosCenter()
		{
			return centerPos;
		}

		public Vector2 getSize()
		{
			return size;
		}
	}
}

