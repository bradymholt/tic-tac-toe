using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace BMH.TicTacToe
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
	    private Graphics mainFormGraphics;
	    private Game m_TicTacToeGame;
	    private bool paintedOnce = false;
	    
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}
		
		#region Events
		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Brady's TicTacToe (C) 2006\nDeveloped by Brady Holt (bholt@alumni.utexas.net)", "About");
		}
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			StartGame();
		}

		private void MainForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			MakeUserPlay(e.KeyChar.ToString());
		}

		private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MakeUserPlay(e.X, e.Y);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (!paintedOnce)
			{
				mainFormGraphics = this.CreateGraphics();
				m_TicTacToeGame = new Game(ref mainFormGraphics);
			}
			
			m_TicTacToeGame.ReDrawBoard();
				
			paintedOnce = true;
		}
		
		#endregion

		
		#region Methods
		private void StartGame()
		{
			m_TicTacToeGame.ResetGame();
			
			//randomly decide whether the computer makes the first move
			Random rand = new Random();
			int computerFirst = rand.Next(0,2);
			if (computerFirst == 1)
			{
				Cursor = Cursors.WaitCursor;
				m_TicTacToeGame.MakeComputerPlay();
				Cursor = Cursors.Default;
			}
		}
		
		private void MakeUserPlay(string keyPressed)
		{
			Cursor = Cursors.WaitCursor;
			
			try
			{
				m_TicTacToeGame.MakeUserPlay(keyPressed);
			}
			catch (Exception ex)
			{
				DisplayMessageBox(ex.Message);
			}
			
			Cursor = Cursors.Default;
		}

		private void MakeUserPlay(int mouseX, int mouseY)
		{
			Cursor = Cursors.WaitCursor;
	
			try
			{
				m_TicTacToeGame.HandleMouseClickPlay(mouseX, mouseY);
			}
			catch (Exception ex)
			{
				DisplayMessageBox(ex.Message);
			}
	
			Cursor = Cursors.Default;
		}
		
		public static void DisplayMessageBox(string message)
		{
			MessageBox.Show(message, "Brady's TicTacToe");
		}
		#endregion

	
	#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem1});
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "New Game";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "About";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(269, 267);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(275, 320);
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Brady\'s TicTacToe";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);

		}
		#endregion
	}
}
