using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMH.TicTacToe
{
	/// <summary>
	/// Summary description for OShape.
	/// </summary>
	public class OShape : Shape
	{
		private const int RADIUS = 30;
		private const float INNER_CIRCLE_PEN_SIZE = 25;
		private const int X_START_OFFSET = -RADIUS + 5;
		private const int Y_START_OFFSET = -RADIUS + 5;
		private const int CIRCLE_WIDTH = RADIUS * 2;
		private const int CIRCLE_HEIGHT = RADIUS * 2;
		
		Color backGroundColor;
		
		public OShape(Color backColor)
		{
			oColor = Color.Blue;
			backGroundColor = backColor;
		}
		
		public override void Draw(ref Graphics graphicsObject, Point startPoint)
		{
			Pen circlePen = new Pen(oColor, m_penSize);
			Pen innerPen = new Pen(backGroundColor, INNER_CIRCLE_PEN_SIZE);
			graphicsObject.DrawEllipse(innerPen, startPoint.X + X_START_OFFSET / 2, startPoint.Y + Y_START_OFFSET / 2, CIRCLE_WIDTH / 2, CIRCLE_HEIGHT / 2);
			graphicsObject.DrawEllipse(circlePen, startPoint.X + X_START_OFFSET, startPoint.Y + Y_START_OFFSET, CIRCLE_WIDTH, CIRCLE_HEIGHT);
		}
	}
}
