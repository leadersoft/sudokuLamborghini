﻿using Gat.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SudokuDogan
{
    public partial class Form1 : Form
    {
        //All these variables are needed to create the form board
        const int CellWidth = 32;
        const int cellHeight = 32;

        const int xOffset = -20;
        const int yOffset = 25;

        private Color DEFAULT_BACKCOLOR = Color.White;

        private Color FIXED_FORECOLOR = Color.Blue;
        private Color FIXED_BACKCOLOR = Color.LightSteelBlue;

        private Color USER_FORECOLOR = Color.Black;
        private Color USER_BACKCOLOR = Color.LightYellow;


        private int SelectedNumber;

        //using the stack for LIFO priciple- Last-in, First-out. Purpose is to make undo moves.
        private Stack<string> Moves;
        private Stack<string> RedoMoves;


        private string saveFileName = string.Empty;

        private int[,] actual = new int[10, 10];

        private int seconds = 0;

        private bool GameStarted = false;


        public Form1()
        {
            InitializeComponent();
        }

        public void DrawBoard()
        {
            toolStripButton1.Checked = true;
            SelectedNumber = 1;

            Point location = new Point();

            for (int row = 1; row <= 9; row++)
            {
                for (int col = 1; col <= 9; col++)
                {
                    location.X = col * (CellWidth + 1) + xOffset;
                    location.Y = row * (cellHeight + 1) + yOffset;

                    Label lbl = new Label();

                    var _with1 = lbl;
                    _with1.Name = col.ToString() + row.ToString();
                    _with1.BorderStyle = BorderStyle.Fixed3D;
                    _with1.Location = location;
                    _with1.Width = CellWidth;
                    _with1.Height = cellHeight;
                    _with1.TextAlign = ContentAlignment.MiddleCenter;
                    _with1.BackColor = DEFAULT_BACKCOLOR;
                    _with1.Font = new Font(_with1.Font, _with1.Font.Style | FontStyle.Bold);
                    _with1.Tag = "1";
                    lbl.Click += Cell_Click;
                    this.Controls.Add(lbl);
                }
            }
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Empty;
            toolStripStatusLabel2.Text = string.Empty;
            DrawBoard();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //we have to show 9 mini squeares- horizontal lines
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            x1 = 1 * (CellWidth + 1) + xOffset - 1;
            x2 = 9 * (CellWidth + 1) + xOffset + CellWidth;
            for (int r = 1; r <= 10; r += 3)
            {
                y1 = r * (cellHeight + 1) + yOffset - 1;
                y2 = y1;
                e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

            //vertical lines
            y1 = 1 * (cellHeight + 1) + yOffset - 1;
            y2 = 9 * (cellHeight + 1) + yOffset + cellHeight;
            for (int c = 1; c <= 10; c += 3)
            {
                x1 = c * (CellWidth + 1) + xOffset - 1;
                x2 = x1;
                e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (GameStarted)
            //{
            //    DialogResult response = MessageBox.Show("Do you want to save current game?", "Save current game", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //    if (response == DialogResult.Yes)
            //    {
            //        SaveGameToDisk(false);
            //    }
            //    else if (response == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            StartNewGame();
        }

        private void StartNewGame()
        {
            saveFileName = string.Empty;
            txtActivities.Text = string.Empty;
            seconds = 0;
            ClearBoard();
            GameStarted = true;
            timer1.Enabled = true;
            toolStripStatusLabel1.Text = "New game started";
        }

        private void ClearBoard()
        {
            Moves = new Stack<string>();
            RedoMoves = new Stack<string>();

            for (int row = 1; row <= 9; row++)
            {
                for (int col = 1; col <= 9; col++)
                {
                    SetCell(col, row, 0, 1);
                }
            }
        }

        private void SetCell(int col, int row, int p1, int p2)
        {
            //Unncessary function right now. The idea with setcell is to assign possible values to a cell.
        //    Control[] lbl = this.Controls.Find(col.ToString() + row.ToString(), true);
        //    Label cellLabel = (Label)lbl(0);
        //    actual[col, row] = value;
        //    if (value == 0)
        //    {
        //        for (int r = 1; r <= 9; r++)
        //        {
        //            for (int c = 1; c <= 9; c++)
        //            {
        //                if (actual[c, r] == 0)
        //                    possible(c, r) = string.Empty;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        possible[col, row] = value.ToString();
        //    }

        //    if (value == 0)
        //    {
        //        cellLabel.Text = string.Empty;
        //        cellLabel.Tag = erasable;
        //        cellLabel.BackColor = DEFAULT_BACKCOLOR;
        //    }
        //    else
        //    {
        //        if (erasable == 0)
        //        {
        //            cellLabel.BackColor = FIXED_BACKCOLOR;
        //            cellLabel.ForeColor = FIXED_FORECOLOR;
        //        }
        //        else
        //        {
        //            cellLabel.BackColor = USER_BACKCOLOR;
        //            cellLabel.ForeColor = USER_FORECOLOR;
        //        }
        //        cellLabel.Text = value;
        //        cellLabel.Tag = erasable;
        //    }

        }

        private void SaveGameToDisk(bool p)
        {
            throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Elapsed time: " + seconds + " second(s)";
            seconds += 1;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SelectedNumber = 1;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 1";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SelectedNumber = 2;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 2";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SelectedNumber = 3;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 3";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SelectedNumber = 4;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 4";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SelectedNumber = 5;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 5";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SelectedNumber = 6;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 6";
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SelectedNumber = 7;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 7";
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            SelectedNumber = 8;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 8";
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            SelectedNumber = 9;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number 9";
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            SelectedNumber = 0;
            txtActivities.Text = string.Empty;
            txtActivities.Text = "Selected number has been erased!";
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            //if (!GameStarted)
            //{
            //    DisplayActivity("Click File->New to start a new" + " game or File->Open to load an existing game", true);
            //    return;
            //}
            //Label cellLabel = (Label)sender;
            //if (cellLabel.Tag.ToString() == "0")
            //{
            //    DisplayActivity("Selected cell is not empty", false);
            //    return;
            //}

            int col = cellLabel.Name.Substring(0, 1);
            int row = cellLabel.Name.ToString().Substring(1, 1);

            if (SelectedNumber == 0)
            {
                if (actual[col, row] == 0)
                    return;
                SetCell(col, row, SelectedNumber, 1);
                DisplayActivity("Number erased at (" + col + "," + row + ")", false);
            }
            else if (cellLabel.Text == string.Empty)
            {
                if (!IsMoveValid(col, row, SelectedNumber))
                {
                    DisplayActivity("Invalid move at (" + col + "," + row + ")", false);
                    return;
                }

                SetCell(col, row, SelectedNumber, 1);
                DisplayActivity("Number placed at (" + col + "," + row + ")", false);
                Moves.Push(cellLabel.Name.ToString() + SelectedNumber);
                if (IsPuzzleSolved())
                {
                    timer1.Enabled = false;
                    Interaction.Beep();
                    toolStripStatusLabel1.Text = "*****Puzzle Solved*****";
                }
            }
        }

    }
}
