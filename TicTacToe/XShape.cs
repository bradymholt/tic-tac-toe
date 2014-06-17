using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMH.TicTacToe
{
	public class XShape : Shape
	{
		private const int X_OFFSET_LEFT = -25;
		private const int X_OFFSET_RIGHT = 35;
		private const int Y_OFFSET_TOP = -25;
		private const int X_OFFSET_BOTTOM = 35;
		
		public XShape()
		{
			oColor = System.Drawing.Color.Red;
		}
		
		public override void Draw(ref Graphics graphicsObject, Point startPoint)
		{
			Pen pn = new Pen(oColor, m_penSize);
			Point blackSlashTop = new Point(startPoint.X + X_OFFSET_LEFT, startPoint.Y + Y_OFFSET_TOP);
			Point blackSlashBottom = new Point(startPoint.X + X_OFFSET_RIGHT, startPoint.Y + X_OFFSET_BOTTOM);
			Point frontSlashTop = new Point(startPoint.X + X_OFFSET_RIGHT, startPoint.Y + Y_OFFSET_TOP);
			Point frontSlashBottom = new Point(startPoint.X + X_OFFSET_LEFT, startPoint.Y + X_OFFSET_BOTTOM);
			graphicsObject.DrawLine(pn, blackSlashTop, blackSlashBottom);
			graphicsObject.DrawLine(pn, frontSlashTop, frontSlashBottom);
		}
	}
}
