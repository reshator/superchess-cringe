using System.Security.Cryptography.Xml;

namespace lichess
{
    public partial class Form1 : Form
    {
        private const string legal = "Legal";
        private Button[,] chessBoard;
        private List<Button> legalbuttons = new List<Button>();

        public Form1()
        {
            InitializeComponent();
        }

        public enum Pieces
        {
            Pawn,
            Knight,
            Bishop,
            Rook,
            Queen,
            King
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBoard();
        }

        private void CreateBoard()
        {
            int tileSize = 90;

            Color first = Color.White;
            Color second = Color.Gray;

            chessBoard = new Button[8, 8];

            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    var button = new Button();
                    button.Size = new Size(tileSize, tileSize);
                    button.Location = new Point(tileSize * i, tileSize * j);

                    Controls.Add(button);

                    if ((i + j) % 2 == 0)
                    {
                        button.BackColor = first;
                    }
                    else
                    {
                        button.BackColor = second;
                    }
                    button.Click += Spawn;
                    button.Tag = new Point(i, j);
                    chessBoard[i, j] = button;

                }

            }
        }

        // main button event
        private void Spawn(object sender, EventArgs e)
        {
            Button button = (Button)sender;
           
            button.Text = comboBox1.Text;
            Point location = (Point)button.Tag;
            int x = location.X;
            int y = location.Y;
            label1.Text = $"{x} {y}";
            ClearBoard(x, y);
            MarkLegalMove(x, y, (Pieces)comboBox1.SelectedIndex);


        }

        private void MarkLegalMove(int x, int y, Pieces piece)
        {
            switch (piece)
            {
                case Pieces.Pawn:
                    if (IsSafe(x, y - 1))
                        chessBoard[x, y - 1].Text = legal;
                    break;
                case Pieces.Knight:
                    if(IsSafe(x + 2, y + 1))
                        chessBoard[x + 2, y + 1].Text = legal;
                    if (IsSafe(x + 2, y - 1))
                        chessBoard[x + 2, y - 1].Text = legal;
                    if (IsSafe(x - 2, y + 1))
                        chessBoard[x - 2, y + 1].Text = legal;
                    if (IsSafe(x - 2, y - 1))
                        chessBoard[x - 2, y - 1].Text = legal;
                    if (IsSafe(x + 1, y + 2))
                        chessBoard[x + 1, y + 2].Text = legal;
                    if (IsSafe(x + 1, y - 2))
                        chessBoard[x + 1, y - 2].Text = legal;
                    if (IsSafe(x - 1, y + 2))
                        chessBoard[x - 1, y + 2].Text = legal;
                    if (IsSafe(x - 1, y - 2))
                        chessBoard[x - 1, y - 2].Text = legal;
                    break;
                case Pieces.Bishop:
                    Bishop(x, y);
                    break;
                case Pieces.Rook:
                    Rook(x, y);
                    break;
                case Pieces.Queen:
                    Bishop(x,y);
                    Rook(x,y);
                    break;
                case Pieces.King:
                    break;
                default:
                    break;

            }

        }

        private void Bishop(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (x == 0 && y == 7)
                {
                    if (IsSafe(x + i, 7 - i))
                        chessBoard[x + i, 7 - i].Text = legal;
                }
                else if (y == 0 && x == 7)
                {
                    chessBoard[x - i, y + i].Text = legal;
                }
                else
                {
                    if (IsSafe(x + i, y + i))
                        chessBoard[x + i, y + i].Text = legal;
                    if (IsSafe(x - i, y - i))
                        chessBoard[x - i, y - i].Text = legal;
                    if (IsSafe(x - i, y + i))
                        chessBoard[x - i, y + i].Text = legal;
                    if (IsSafe(x + i, y - i))
                        chessBoard[x + i, y - i].Text = legal;

                }
            }
        }


        private void Rook(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                chessBoard[x, i].Text = legal;
                chessBoard[i, y].Text = legal;
            }
        }
        
        // buttons mark as legal
        private void AddLegalButtons()
        {
            legalbuttons.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessBoard[i, j].Text == legal)
                    {
                        legalbuttons.Add(chessBoard[i, j]);
                    }
                }
            }
        }


        private void ClearBoard(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessBoard[x, y] == chessBoard[i, j])
                    {

                    }
                    else
                    {
                        chessBoard[i, j].Text = "";
                    }
                }
            }
        }
        private bool IsSafe(int x, int y)
        {
            if ((x >= 8 || y >= 8) || (x < 0 || y < 0))
            {
                return false;

            }
            else
            {
                return true;
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
            
           

        }

        private void button1_Click(object sender, EventArgs e)
        {

            AddLegalButtons();

            foreach (var button in legalbuttons)
            {

                var location = (Point) button.Tag;
                var str = $"{location.X},{location.Y}";
                
                if (textBox1.Text == str)
                {
                    label2.Text = "Можно";
                    break;
                }
                else
                {
                    label2.Text = "Нельзя";
                }
            }

        }
    }
}

