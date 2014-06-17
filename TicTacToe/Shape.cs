using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMH.TicTacToe
{
	/// <summary>
	/// Summary description for Shape.
	/// </summary>
	public abstract class Shape
	{
		protected System.Drawing.Color oColor;
		protected const float m_penSize = 18;
		
		public abstract void Draw(ref Graphics graphicsObject, Point startPoint);
		
	}
}
