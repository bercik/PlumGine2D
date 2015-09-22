using System;

using Microsoft.Xna.Framework;

namespace PlumGine2D
{
	public interface IGameObject
	{
		Vector2 getPosLeftTop();
		Vector2 getPosCenter();
		Vector2 getSize();
	}
}

