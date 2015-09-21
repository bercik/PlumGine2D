using System;

using Microsoft.Xna.Framework;

namespace PlumGine2D
{
	public interface IGameObject
	{
		float getX();
		float getY();
		float getWidth();
		float getHeight();

		Vector2 getPos();
		Vector2 getSize();
	}
}

