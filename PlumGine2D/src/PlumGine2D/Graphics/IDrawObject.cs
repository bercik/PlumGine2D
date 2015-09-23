using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D.Graphics
{
	public interface IDrawObject : IGameObject
	{
		void Draw(SpriteBatch spriteBatch, Vector2 pos, Point screenSize, Vector2 scale);
	}
}

