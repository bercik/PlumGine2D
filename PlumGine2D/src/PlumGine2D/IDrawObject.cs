using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlumGine2D
{
	public interface IDrawObject : IGameObject
	{
		Texture2D getTexture();
	}
}

