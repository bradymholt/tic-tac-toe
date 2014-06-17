using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMH.TicTacToe
{
	/// <summary>
	/// Summary description for Position.
	/// </summary>
	public class Play
	{
		private Point m_Position;
		private Shape m_PlayShape;
		
		public Play(Shape shape, Point position)
		{
			m_PlayShape = shape;
			m_Position = position;
		}
		
		public void DrawPlay(ref Graphics gph)
		{
			m_PlayShape.Draw(ref gph, m_Position);
		}
		
		
		
	}
}
