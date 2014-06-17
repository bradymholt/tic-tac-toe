using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Threading;

namespace BMH.TicTacToe
{
	/// <summary>
	/// Summary description for Game.
	/// </summary>
	public class Game
	{
		//constants
		private const string USER_WIN_TEXT = "You win!";														 
		private const string COMPUTER_WIN_TEXT = "You lose!";
		private const string CAT_WIN_TEXT = "Cat wins!";
		private const int MAX_PLAYS = 9;
		
		//gui points
		private Point[] m_PositionLocations = new Point[9]{new Point(40,40),  new Point(130,40),  new Point(220,40),
													     new Point(40,130), new Point(130,130), new Point(220,130),
														 new Point(40,220), new Point(130,220), new Point(220,220)};
		
		private Point[] m_LeftVerticalGridLine = new Point[2]{new Point(90,0), new Point(90,270)};
		private Point[] m_RightVerticalGridLine = new Point[2]{new Point(180,0), new Point(180,270)};
		private Point[] m_TopHorizontalGridLine = new Point[2]{new Point(0,90), new Point(270,90)};
		private Point[] m_BottomHorizontalGridLine = new Point[2]{new Point(0,180), new Point(270,180)};
		
		//gui visual settings
		Color m_GridBackGroundColor = Color.White;
		Color m_GridLineColor = Color.Black;
		Color m_PositionNumberColor = Color.Red;
		string m_GridPositionNumberFont = "Arial";
		float m_GridPositionNumberSize = 16;
		float m_GridLineSize = 2;
		
		//game objects
		private ArrayList m_WinPositionCombos;
		private Graphics m_graphics;
		private int playCount;
		private bool m_GameOver;
		private Play[] m_UserPlays;
		private Play[] m_ComputerPlays;
		
		public Game(ref Graphics graphicsObject)
		{
			//specify win combos
			m_WinPositionCombos = new ArrayList(9);
			m_WinPositionCombos.Add(new int[3]{0, 1, 2});  //top row
			m_WinPositionCombos.Add(new int[3]{3, 4, 5});  //middle row
			m_WinPositionCombos.Add(new int[3]{6, 7, 8});  //bottom row
			m_WinPositionCombos.Add(new int[3]{0, 3, 6});  //left column
			m_WinPositionCombos.Add(new int[3]{1, 4, 7});  //middle column
			m_WinPositionCombos.Add(new int[3]{2, 5, 8});  //right column
			m_WinPositionCombos.Add(new int[3]{0, 4, 8});  //back-slash
			m_WinPositionCombos.Add(new int[3]{2, 4, 6});  //front-slash
			
			m_graphics = graphicsObject;
			ResetGame();
		}
		
		public void ResetGame()
		{
			m_GameOver = false;
			
			//reset play collection
			m_UserPlays = new Play[9];
			m_ComputerPlays = new Play[9];
			
			playCount = 0;
			SetupBoard();
		}
		
		public void HandleMouseClickPlay(int x, int y)
		{
			string keyEqivelent = string.Empty;
			
			if (x <= m_LeftVerticalGridLine[0].X && y <= m_TopHorizontalGridLine[0].Y)
				keyEqivelent = "1";
			else if (x >= m_LeftVerticalGridLine[0].X && x <= m_RightVerticalGridLine[0].X && y <= m_TopHorizontalGridLine[0].Y)
				keyEqivelent = "2";
			else if (x >= m_RightVerticalGridLine[0].X && y <= m_TopHorizontalGridLine[0].Y)
				keyEqivelent = "3";
			else if (x <= m_LeftVerticalGridLine[0].X && y >= m_TopHorizontalGridLine[0].Y && y <= m_BottomHorizontalGridLine[0].Y)
				keyEqivelent = "4";
			else if (x >= m_LeftVerticalGridLine[0].X && x <= m_RightVerticalGridLine[0].X && y >= m_TopHorizontalGridLine[0].Y && y <= m_BottomHorizontalGridLine[0].Y)
				keyEqivelent = "5";
			else if (x >= m_RightVerticalGridLine[0].X && y >= m_TopHorizontalGridLine[0].Y && y <= m_BottomHorizontalGridLine[0].Y)
				keyEqivelent = "6";
			else if (x <= m_LeftVerticalGridLine[0].X && y >= m_BottomHorizontalGridLine[0].Y)
				keyEqivelent = "7";
			else if (x >= m_LeftVerticalGridLine[0].X && x <= m_RightVerticalGridLine[0].X && y >= m_BottomHorizontalGridLine[0].Y)
				keyEqivelent = "8";
			else if (x >= m_RightVerticalGridLine[0].X && y >= m_BottomHorizontalGridLine[0].Y)
				keyEqivelent = "9";
				
			if (keyEqivelent != string.Empty)
				MakeUserPlay(keyEqivelent);
		}
		
		public void MakeUserPlay(string keyPressed)
		{
			if (!m_GameOver)
			{
				int numberPressed;
				
				if (keyPressed.IndexOfAny(new char[9]{'1','2','3','4','5','6','7','8','9'}, 0, 1) == -1)
					throw new Exception("Enter a number between 1 and 9!");
				
				numberPressed = int.Parse(keyPressed);
				
				int arrayPosition = numberPressed - 1;
				if (m_UserPlays[arrayPosition] == null && m_ComputerPlays[arrayPosition] == null)
				{
					m_UserPlays[arrayPosition] = new Play(new XShape(), m_PositionLocations[arrayPosition]);
					m_UserPlays[arrayPosition].DrawPlay(ref m_graphics);
					playCount++;
					
					if (playCount == MAX_PLAYS)
						ShowWin(CAT_WIN_TEXT);
					else if (!CheckForWin(m_UserPlays))
						MakeComputerPlay();
					else
						ShowWin(USER_WIN_TEXT);
				}
				else
					throw new Exception("This position has already been played!");			
			}
		}
		
		public void MakeComputerPlay()
		{
			//wait to simulate computer "thinking"
			Thread.Sleep(1000);
			
			int positionToPlay = -1;
			
			//first, go for the win (if possible)
			foreach (int[] winCombo in m_WinPositionCombos)
			{
				if (m_ComputerPlays[winCombo[0]] != null && m_ComputerPlays[winCombo[1]] != null && m_ComputerPlays[winCombo[2]] == null && m_UserPlays[winCombo[2]] == null)
				{
					positionToPlay = winCombo[2];
					break;
				}
				else if (m_ComputerPlays[winCombo[0]] != null && m_ComputerPlays[winCombo[1]] == null && m_UserPlays[winCombo[1]] == null && m_ComputerPlays[winCombo[2]] != null)
				{
					positionToPlay = winCombo[1];
					break;
				}
				else if (m_ComputerPlays[winCombo[0]] == null && m_UserPlays[winCombo[0]] == null && m_ComputerPlays[winCombo[1]] != null && m_ComputerPlays[winCombo[2]] != null)
				{
					positionToPlay = winCombo[0];
					break;
				}
			}
			
			
			//next priority, block a user win
			if (positionToPlay == -1)
			{
				foreach (int[] winCombo in m_WinPositionCombos)
				{
					if (m_UserPlays[winCombo[0]] != null && m_UserPlays[winCombo[1]] != null && m_UserPlays[winCombo[2]] == null && m_ComputerPlays[winCombo[2]] == null)
					{
						positionToPlay = winCombo[2];
						break;
					}
					else if (m_UserPlays[winCombo[0]] != null && m_UserPlays[winCombo[1]] == null && m_ComputerPlays[winCombo[1]] == null && m_UserPlays[winCombo[2]] != null)
					{
						positionToPlay = winCombo[1];
						break;
					}
					else if (m_UserPlays[winCombo[0]] == null && m_ComputerPlays[winCombo[0]] == null && m_UserPlays[winCombo[1]] != null && m_UserPlays[winCombo[2]] != null)
					{
						positionToPlay = winCombo[0];
						break;
					}
				}
			}
			
			//if we still don't have a position to play, pick a random position
			if (positionToPlay == -1)
			{
				int randomPosition;
				Random randomPlay = new Random();
				
				while(positionToPlay == -1)
				{
					randomPosition = randomPlay.Next(0, 9);
					
					//make sure this position hasn't been played yet and if not, pick this position to play
					if (m_ComputerPlays[randomPosition] == null && m_UserPlays[randomPosition] == null)
					{
						positionToPlay = randomPosition;
						
					}
				}
			}
			
			m_ComputerPlays[positionToPlay] =  new Play(new OShape(m_GridBackGroundColor), m_PositionLocations[positionToPlay]);
			m_ComputerPlays[positionToPlay].DrawPlay(ref m_graphics);
			playCount++;
			
			if (playCount == MAX_PLAYS)
				ShowWin(CAT_WIN_TEXT);
			else if (CheckForWin(m_ComputerPlays))
				ShowWin(COMPUTER_WIN_TEXT);
		}
		
		private void ShowWin(string winText)
		{
			m_GameOver = true;
			MainForm.DisplayMessageBox(winText);
		}
		
		private bool CheckForWin(Play[] playCollection)
		{
			bool win = false;
			
			//loop through each win combo and check to see if any of them match the user or computer's plays
			foreach (int[] winCombo in m_WinPositionCombos)
			{
				if (playCollection[winCombo[0]] != null && playCollection[winCombo[1]] != null && playCollection[winCombo[2]] != null)
					win = true;
			}
			
			return win;
		}
		
		private void SetupBoard()
		{
			if (m_graphics != null)
			{
				//repaint background
				m_graphics.Clear(m_GridBackGroundColor);
				
				//draw grid lines
				Pen pn = new Pen(m_GridLineColor, m_GridLineSize);
				m_graphics.DrawLine(pn, m_LeftVerticalGridLine[0], m_LeftVerticalGridLine[1]);
				m_graphics.DrawLine(pn, m_RightVerticalGridLine[0], m_RightVerticalGridLine[1]);
				m_graphics.DrawLine(pn, m_TopHorizontalGridLine[0], m_TopHorizontalGridLine[1]);
				m_graphics.DrawLine(pn, m_BottomHorizontalGridLine[0], m_BottomHorizontalGridLine[1]);
				
				//draw position numbers
				Font fnt = new Font(m_GridPositionNumberFont, m_GridPositionNumberSize);
				for(int positionNumber = 1; positionNumber<=9; positionNumber++)
				{
					m_graphics.DrawString(positionNumber.ToString(), fnt, new SolidBrush(m_PositionNumberColor), m_PositionLocations[positionNumber - 1].X, m_PositionLocations[positionNumber - 1].Y);
				}
			}
		}
		
		public void ReDrawBoard()
		{
			SetupBoard();
			
			//draw user plays
			foreach(Play userPlay in m_UserPlays)
			{
				if (userPlay != null)
					userPlay.DrawPlay(ref m_graphics);
			}
		
			//draw computer plays
			foreach(Play computerPlay in m_ComputerPlays)
			{
				if (computerPlay != null)
					computerPlay.DrawPlay(ref m_graphics);
			}
		}
		
	}
}
