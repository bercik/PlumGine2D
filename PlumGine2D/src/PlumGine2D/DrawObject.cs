using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlumGine2D
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

		public Texture2D getTexture()
		{
			return texture;
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

