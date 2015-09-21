using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlumGine2D
{
	public class DrawObject : IDrawObject
	{
		private float x;
		private float y;
		private float width;
		private float height;
		private Texture2D texture;

		public DrawObject(Texture2D texture, float x, float y)
		{
			this.texture = texture;
			this.x = x;
			this.y = y;
			this.width = texture.Width;
			this.height = texture.Height;
		}

		public Texture2D getTexture()
		{
			return texture;
		}

		public float getX()
		{
			return x;
		}

		public float getY()
		{
			return y;
		}

		public float getWidth()
		{
			return width;
		}

		public float getHeight()
		{
			return height;
		}

		public Vector2 getPos()
		{
			return new Vector2(x, y);
		}

		public Vector2 getSize()
		{
			return new Vector2(width, height);
		}
	}
}

