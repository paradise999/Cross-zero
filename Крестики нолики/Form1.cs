using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;


namespace креститки_нолики
{
    public partial class Form1 : Form
    {
        public bool qwe = false;      //можно ли ходить
        public bool pervhod = true;      //true говорит о том, что у компьютера нынче первый ход. первым ходом компьютер должен ходить в
                                         //центр, дальше по ситуации. значение false переправляет на путь "дальше по ситуации"
        public bool flag = true;      // проверка новая ли игра                        
        public bool first = true;      //меняется на false сразу после первого хода юзера(играет крестиками), нужен для проверки, был ли первый ход в центр или нет
        public int ph = 0;      // счётчик ходов. Пользователь с компьютером, крестики с человеком.
        public int ch = 0;      // ходы компьютера.
        public int ph2 = 0;      // ходы ноликов.
        public int cng = 0;      // для передачи хода между игроками.       
        public int korn = 0;      // для игы с человеком. Если 0 - крестики, 1 - нолики.
        public int cherta = 0;      // черта для вычеркивания выигрышной комбинации
        public int xfir = -1;      // первый ход пользователя
        public int yfir = -1;      // когда он играет крестиками
        public int xlast = -1;      // последний
        public int ylast = -1;      // ход пользователя
        public int win = 0;       // если 1 - выиграл компьютер, 2 - пользователь, 3 - ничья. В случае игры с человеком, если 1 - выиграли крестики, 2 - выиграли нолики, 3 - ничья.
        public bool hdpc = false;      // true - сейчас ход компьютера, false - ход пользователя
        public int pc = 1;      // кто ходит первым, 1 - ходит комп, 2 первый ходит юзер
        public int[,] a = new int[3, 3];      // если равен 0 = пустая клетка, равен 1 - ход компьютера, 2 - пользователь. В случае игры с человеком, -1 - крестики, 2 - нолики.
        public string[] text = new string[24];      // для записи данных для загрузки.
        List<string> file = new List<string>();      // для форматирования данных для сохранения.
        public string line;      // для считывания данных для загрузки.
        RegistryKey XorN;      //Ключ для чтения и записи реестра.
        private string[] password = { "123456", "abcdef", "ABCDEF", "7890" };    //пароли активации.





        public Form1()
        {
            for (int i = 0; i < 3; i++) // заполнение массива ходов нулями.
            {
                for (int j = 0; j < 0; j++)
                {
                    a[i, j] = 0;
                }
            }
            InitializeComponent();
        }





        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics gPanel = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 1);
            Pen p1 = new Pen(Color.Blue, 2);
            gPanel.DrawLine(p, new Point(0, 0), new Point(300, 0));
            gPanel.DrawLine(p, new Point(300, 0), new Point(300, 300));
            gPanel.DrawLine(p, new Point(0, 0), new Point(0, 300));
            gPanel.DrawLine(p, new Point(0, 300), new Point(300, 300));
            gPanel.DrawLine(p1, new Point(100, 0), new Point(100, 300));
            gPanel.DrawLine(p1, new Point(200, 0), new Point(200, 300));
            gPanel.DrawLine(p1, new Point(0, 100), new Point(300, 100));
            gPanel.DrawLine(p1, new Point(0, 200), new Point(300, 200));
        }      // рисование поля для игры.

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (qwe)
            {
                if (pc == 0)  //если пользователь сразу нажимает на доску, не выбрав кто будет ходить первым.
                {
                    MessageBox.Show("Выберите режим игры!");
                }
                if (pc == 3)
                {
                    if (korn == 0)   //крестики
                    {

                        if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 0 && e.Location.Y < 100)  //1 ячейка
                        {

                            if (a[0, 0] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2, 2);
                                label1.Text = "Ходят нолики!";
                                a[0, 0] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }

                        if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 0 && e.Location.Y < 100)//2 ячейка
                        {
                            if (a[1, 0] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 102, 2);
                                label1.Text = "Ходят нолики!";
                                a[1, 0] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 0 && e.Location.Y < 100)//3 ячейка
                        {
                            if (a[2, 0] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 202, 2);
                                label1.Text = "Ходят нолики!";
                                a[2, 0] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 100 && e.Location.Y < 200)//4 ячейка
                        {
                            if (a[0, 1] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2, 102);
                                label1.Text = "Ходят нолики!";
                                a[0, 1] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 100 && e.Location.Y < 200)//5 ячейка
                        {
                            if (a[1, 1] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 102, 102);
                                label1.Text = "Ходят нолики!";
                                a[1, 1] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 100 && e.Location.Y < 200)//6 ячейка
                        {
                            if (a[2, 1] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 202, 102);
                                label1.Text = "Ходят нолики!";
                                a[2, 1] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 200 && e.Location.Y < 300)//7 ячейка
                        {
                            if (a[0, 2] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2, 202);
                                label1.Text = "Ходят нолики!";
                                a[0, 2] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                        {
                            if (a[1, 2] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 102, 202);
                                label1.Text = "Ходят нолики!";
                                a[1, 2] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 200 && e.Location.Y < 300)//9 ячейка
                        {
                            if (a[2, 2] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../крестик.png"), 202, 202);
                                label1.Text = "Ходят нолики!";
                                a[2, 2] = -1;
                                cng = 1;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                    }
                    if (korn == 1)   // нолики
                    {
                        if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 0 && e.Location.Y < 100)  //1 ячейка
                        {

                            if (a[0, 0] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 0, 2);
                                label1.Text = "Ходят крестики!";
                                a[0, 0] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }

                        if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 0 && e.Location.Y < 100)//2 ячейка
                        {
                            if (a[1, 0] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 100, 2);
                                label1.Text = "Ходят крестики!";
                                a[1, 0] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 0 && e.Location.Y < 100)//3 ячейка
                        {
                            if (a[2, 0] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 200, 2);
                                label1.Text = "Ходят крестики!";
                                a[2, 0] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 100 && e.Location.Y < 200)//4 ячейка
                        {
                            if (a[0, 1] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 0, 102);
                                label1.Text = "Ходят крестики!";
                                a[0, 1] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 100 && e.Location.Y < 200)//5 ячейка
                        {
                            if (a[1, 1] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 100, 102);
                                label1.Text = "Ходят крестики!";
                                a[1, 1] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 100 && e.Location.Y < 200)//6 ячейка
                        {
                            if (a[2, 1] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 200, 102);
                                label1.Text = "Ходят крестики!";
                                a[2, 1] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 200 && e.Location.Y < 300)//7 ячейка
                        {
                            if (a[0, 2] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 0, 202);
                                label1.Text = "Ходят крестики!";
                                a[0, 2] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                        {
                            if (a[1, 2] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 100, 202);
                                label1.Text = "Ходят крестики!";
                                a[1, 2] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                        if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 200 && e.Location.Y < 300)//9 ячейка
                        {
                            if (a[2, 2] == 0)
                            {
                                Graphics gPanel = panel1.CreateGraphics();
                                gPanel.DrawImage(Image.FromFile("../../нолик.png"), 200, 202);
                                label1.Text = "Ходят крестики!";
                                a[2, 2] = 2;
                                cng = 0;
                                if (proverka() == 1)
                                {
                                    winner();
                                }
                                else
                                {
                                    nichia();
                                    winner();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ячейка уже занята");
                            }
                        }
                    }
                    if (cng == 0) korn = 0;
                    if (cng == 1) korn = 1;
                }
                else
                {
                    if (hdpc == false)   //ход пользователя.
                    {
                        if (pc == 2)    //первый ход пользователя, значит он играет крестиками.
                        {
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 0 && e.Location.Y < 100)  //1 ячейка
                            {

                                if (a[0, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2, 2);
                                    a[0, 0] = 2;
                                    xlast = 0;
                                    ylast = 0;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }

                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 0 && e.Location.Y < 100)//2 ячейка
                            {
                                if (a[1, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 102, 2);
                                    a[1, 0] = 2;
                                    xlast = 1;
                                    ylast = 0;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 0 && e.Location.Y < 100)//3 ячейка
                            {
                                if (a[2, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 202, 2);
                                    a[2, 0] = 2;
                                    xlast = 2;
                                    ylast = 0;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 100 && e.Location.Y < 200)//4 ячейка
                            {
                                if (a[0, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2, 102);
                                    a[0, 1] = 2;
                                    xlast = 0;
                                    ylast = 1;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 100 && e.Location.Y < 200)//5 ячейка
                            {
                                if (a[1, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 102, 102);
                                    a[1, 1] = 2;
                                    xlast = 1;
                                    ylast = 1;
                                    hdpc = true;
                                    if (first)
                                    {
                                        xfir = 1;
                                        yfir = 1;
                                        first = false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 100 && e.Location.Y < 200)//6 ячейка
                            {
                                if (a[2, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 202, 102);
                                    a[2, 1] = 2;
                                    xlast = 2;
                                    ylast = 1;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 200 && e.Location.Y < 300)//7 ячейка
                            {
                                if (a[0, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2, 202);
                                    a[0, 2] = 2;
                                    xlast = 0;
                                    ylast = 2;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                            {
                                if (a[1, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 102, 202);
                                    a[1, 2] = 2;
                                    xlast = 1;
                                    ylast = 2;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 200 && e.Location.Y < 300)//9 ячейка
                            {
                                if (a[2, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../крестик.png"), 202, 202);
                                    a[2, 2] = 2;
                                    xlast = 2;
                                    ylast = 2;
                                    first = false;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                        }
                        else   // пользователь играет ноликами.
                        {
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 0 && e.Location.Y < 100)  //1 ячейка
                            {

                                if (a[0, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 0, 2);
                                    a[0, 0] = 2;
                                    xlast = 0;
                                    ylast = 0;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }

                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 0 && e.Location.Y < 100)//2 ячейка
                            {
                                if (a[1, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 100, 2);
                                    a[1, 0] = 2;
                                    xlast = 1;
                                    ylast = 0;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 0 && e.Location.Y < 100)//3 ячейка
                            {
                                if (a[2, 0] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 200, 2);
                                    a[2, 0] = 2;
                                    xlast = 2;
                                    ylast = 0;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 100 && e.Location.Y < 200)//4 ячейка
                            {
                                if (a[0, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 0, 102);
                                    a[0, 1] = 2;
                                    xlast = 0;
                                    ylast = 1;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 100 && e.Location.Y < 200)//5 ячейка
                            {
                                if (a[1, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 100, 102);
                                    a[1, 1] = 2;
                                    xlast = 1;
                                    ylast = 1;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 100 && e.Location.Y < 200)//6 ячейка
                            {
                                if (a[2, 1] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 200, 102);
                                    a[2, 1] = 2;
                                    xlast = 2;
                                    ylast = 1;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 0 && e.Location.X < 100 && e.Location.Y > 200 && e.Location.Y < 300)//7 ячейка
                            {
                                if (a[0, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 0, 202);
                                    a[0, 2] = 2;
                                    xlast = 0;
                                    ylast = 2;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 100 && e.Location.X < 200 && e.Location.Y > 200 && e.Location.Y < 300)//8 ячейка
                            {
                                if (a[1, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 100, 202);
                                    a[1, 2] = 2;
                                    xlast = 1;
                                    ylast = 2;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                            if (e.Location.X > 200 && e.Location.X < 300 && e.Location.Y > 200 && e.Location.Y < 300)//9 ячейка
                            {
                                if (a[2, 2] == 0)
                                {
                                    Graphics gPanel = panel1.CreateGraphics();
                                    gPanel.DrawImage(Image.FromFile("../../нолик.png"), 200, 202);
                                    a[2, 2] = 2;
                                    xlast = 2;
                                    ylast = 2;
                                    hdpc = true;
                                }
                                else
                                {
                                    MessageBox.Show("Ячейка уже занята");
                                }
                            }
                        }
                        if (proverka() == 0)
                        {
                            if (hdpc)
                                hod();
                        }
                        else
                        {
                            winner();
                        }

                    }
                }
            }
        }      // ход пользователя

        private int proverka()
        {

            if ((a[0, 0] + a[0, 1] + a[0, 2]) == 6)
            {
                win = 2;
                cherta = 4;
                return 1;
            }

            else
            {
                if ((a[1, 0] + a[1, 1] + a[1, 2]) == 6)
                {
                    win = 2;
                    cherta = 5;
                    return 1;
                }
                else
                {
                    if ((a[2, 0] + a[2, 1] + a[2, 2]) == 6)
                    {
                        win = 2;
                        cherta = 6;
                        return 1;
                    }
                    else
                    {
                        if ((a[0, 0] + a[1, 0] + a[2, 0]) == 6)
                        {
                            win = 2;
                            cherta = 1;
                            return 1;
                        }
                        else
                        {
                            if ((a[0, 1] + a[1, 1] + a[2, 1]) == 6)
                            {
                                win = 2;
                                cherta = 2;
                                return 1;
                            }
                            else
                            {
                                if ((a[0, 2] + a[1, 2] + a[2, 2]) == 6)
                                {
                                    win = 2;
                                    cherta = 3;
                                    return 1;
                                }
                                else
                                {
                                    if ((a[0, 0] + a[1, 1] + a[2, 2]) == 6)
                                    {
                                        win = 2;
                                        cherta = 7;
                                        return 1;
                                    }
                                    else
                                    {
                                        if ((a[0, 2] + a[1, 1] + a[2, 0]) == 6)
                                        {
                                            win = 2;
                                            cherta = 8;
                                            return 1;
                                        }
                                        else
                                        {
                                            if (pc == 3)
                                            {
                                                if ((a[0, 0] + a[0, 1] + a[0, 2]) == -3)
                                                {
                                                    win = 1;
                                                    cherta = 4;
                                                    return 1;
                                                }

                                                else
                                                {
                                                    if ((a[1, 0] + a[1, 1] + a[1, 2]) == -3)
                                                    {
                                                        win = 1;
                                                        cherta = 5;
                                                        return 1;
                                                    }
                                                    else
                                                    {
                                                        if ((a[2, 0] + a[2, 1] + a[2, 2]) == -3)
                                                        {
                                                            win = 1;
                                                            cherta = 6;
                                                            return 1;
                                                        }
                                                        else
                                                        {
                                                            if ((a[0, 0] + a[1, 0] + a[2, 0]) == -3)
                                                            {
                                                                win = 1;
                                                                cherta = 1;
                                                                return 1;
                                                            }
                                                            else
                                                            {
                                                                if ((a[0, 1] + a[1, 1] + a[2, 1]) == -3)
                                                                {
                                                                    win = 1;
                                                                    cherta = 2;
                                                                    return 1;
                                                                }
                                                                else
                                                                {
                                                                    if ((a[0, 2] + a[1, 2] + a[2, 2]) == -3)
                                                                    {
                                                                        win = 1;
                                                                        cherta = 3;
                                                                        return 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        if ((a[0, 0] + a[1, 1] + a[2, 2]) == -3)
                                                                        {
                                                                            win = 1;
                                                                            cherta = 7;
                                                                            return 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            if ((a[0, 2] + a[1, 1] + a[2, 0]) == -3)
                                                                            {
                                                                                win = 1;
                                                                                cherta = 8;
                                                                                return 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                return 0;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                return 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }       // проверка - выиграш

        private void chet()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[i, j] == 1)
                    {
                        ch++;
                    }
                    if (a[i, j] == -1)
                    {
                        ph++;
                    }
                    if (a[i, j] == 2)
                    {
                        ph2++;
                    }
                }
            }
        }      // подсчёт ходов

        private void hod1()
        {
            if (a[1, 1] == 0)
            {
                a[1, 1] = 1;   //компьютер первым ходом всегда ходит в центр(если есть возможность), независимо от того какими он играет
            }
            else
            {
                random();
            }
            paint();
            hdpc = false;
            pervhod = false;
        }       // первый ход компьютера
        private void hod()
        {
            if ((pc == 1) || (pc == 2))
            {
                nichia();     //есть ли свободное поле
                if (qwe)
                {
                    pobeda();    //1 правило
                    if (win == 0)
                    {
                        zachita();  //2 правило
                        if (hdpc == true)   //если 1,2 правила невыполнены, то есть до сих пор ход компьютера, он должен сделать либо противоположный
                        {                              // либо любой ход
                            if (pc == 1)    //если первый сходил компьтер, значит по тактике нужно ходить точно противоположно ходу пользователя
                            {
                                krestiki();//ход противоположный
                            }
                            else  //компьютер ходит вторым
                            {
                                if (xfir == 1 && yfir == 1)   //ходят в центр - ходим в углы
                                {
                                    ugol();
                                }
                                else   //если ходят не в центр
                                {
                                    if (pervhod)
                                    {
                                        hod1();   //ходим в центр
                                        pervhod = false;
                                    }
                                    else
                                    {
                                        ugol();
                                    }
                                }
                            }
                        }    //конец противоположного хода
                        nichia();
                    }
                    else
                    {
                        winner();
                    }
                }
            }
        }      // ход компьютера, когда он ходит первый
        private void random()
        {
            bool rand = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (rand == false)
                    {
                        if (a[i, j] == 0)
                        {
                            a[i, j] = 1;
                            rand = true;
                            hdpc = false;
                            paint();
                        }
                    }
                }
            }
        }      // заполняем любую пустую клетку, для компьютера. случайность даёт шанс выиграть у компьютера.
        private void paint()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pc == 1)   //если компьютер начинал, то 1 - это крестики
                    {
                        if (a[i, j] == 1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2 + i * 100, 2 + j * 100);
                        }
                    }
                    else   //компьютер ходил вторым, 1 - нолики
                    {
                        if (a[i, j] == 1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../нолик.png"), i * 100, j * 100);
                        }
                    }
                }
            }
        }      // рисование хода компьютера.
        private void paintload()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pc == 1)   //если компьютер начинал, то 1 - это крестики
                    {
                        if (a[i, j] == 1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2 + i * 100, 2 + j * 100);
                        }
                        if (a[i, j] == 2)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../нолик.png"), i * 100, j * 100);
                        }
                    }
                    if (pc == 2)   //компьютер ходил вторым, 1 - нолики
                    {
                        if (a[i, j] == 1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../нолик.png"), i * 100, j * 100);
                        }
                        if (a[i, j] == 2)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../крестик.png"), 2 + i * 100, 2 + j * 100);
                        }
                    }
                    if (pc == 3)
                    {
                        if (a[i, j] == 2)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../нолик.png"), i * 100, j * 100);
                        }
                        if (a[i, j] == -1)
                        {
                            Graphics gPanel = panel1.CreateGraphics();
                            gPanel.DrawImage(Image.FromFile("../../крестик.png"), i * 100, j * 100);
                        }
                    }
                }
            }
        }      // рисование при загрузке.
        private void winner()
        {
            Graphics gPanel = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 6);
            switch (cherta)
            {
                case 1: gPanel.DrawLine(p, 2, 50, 298, 50); break;
                case 2: gPanel.DrawLine(p, 2, 150, 298, 150); break;
                case 3: gPanel.DrawLine(p, 2, 250, 298, 250); break;
                case 4: gPanel.DrawLine(p, 50, 2, 50, 298); break;
                case 5: gPanel.DrawLine(p, 150, 2, 150, 298); break;
                case 6: gPanel.DrawLine(p, 250, 2, 250, 298); break;
                case 7: gPanel.DrawLine(p, 2, 2, 298, 298); break;
                case 8: gPanel.DrawLine(p, 298, 2, 2, 298); break;
            }
            if ((pc == 1) || (pc == 2))
            {
                if (win == 1)
                {
                    chet();
                    if (ch > 4)
                        MessageBox.Show("Компьютер выиграл за " + ch + " шагов!");
                    else
                        MessageBox.Show("Компьютер выиграл за " + ch + " шага!");
                    newgame();
                }
                if (win == 2)
                {
                    chet();
                    if (ph > 4)
                        MessageBox.Show("Пользователь выигралз а " + ph + " шагов!");
                    else
                        MessageBox.Show("Пользователь выиграл! За " + ph + " шага!");
                    newgame();
                }
                if (win == 3)
                {
                    chet();
                    if ((ph + ch) > 4)
                        MessageBox.Show("Ничья за " + (ph + ch) + " шагов!");
                    else
                        MessageBox.Show("Ничья за " + (ph + ch) + " шага!");
                    newgame();
                }
            }
            if (pc == 3)
            {
                if (win == 1)
                {
                    chet();
                    if (ph > 4)
                        MessageBox.Show("Крестик выиграл за " + ph + " шагов!");
                    else
                        MessageBox.Show("Крестик выиграл за " + ph + " шага!");
                    newgame();
                }
                if (win == 2)
                {
                    chet();
                    if (ph2 > 4)
                        MessageBox.Show("Нолик выиграл за " + ph2 + " шагов!");
                    else
                        MessageBox.Show("Нолик выиграл за " + ph2 + " шага!");
                    newgame();
                }
                if (win == 3)
                {
                    chet();
                    if ((ph + ph2) > 4)
                        MessageBox.Show("Ничья за " + (ph + ph2) + " шагов!");
                    else
                        MessageBox.Show("Ничья за " + (ph + ph2) + " шага!");
                    newgame();
                }
            }
        }      // вывод победы.
        private void pobeda()
        {
            //НАЧИНАЕТСЯ НАПАДЕНИЕ(ПОПЫТКА ВЫИГРАТЬ, ЕСЛИ ЕСТЬ ВОЗМОЖНОСТЬ)
            if (((a[0, 0] + a[0, 1] + a[0, 2]) == 2) && (a[0, 0] == 1 || a[0, 1] == 1 || a[0, 2] == 1))  //1-4-7  - нападение
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[0, j] == 0)
                    {
                        a[0, j] = 1;
                    }
                }
                win = 1;
                paint();
                cherta = 4;
            }
            else
            {
                if (((a[1, 0] + a[1, 1] + a[1, 2]) == 2) && (a[1, 0] == 1 || a[1, 1] == 1 || a[1, 2] == 1))   //2-5-8  - нападение
                {

                    for (int j = 0; j < 3; j++)
                    {
                        if (a[1, j] == 0)
                        {
                            a[1, j] = 1;
                        }
                    }
                    win = 1;
                    paint();
                    cherta = 5;
                }
                else
                {
                    if (((a[2, 0] + a[2, 1] + a[2, 2]) == 2) && (a[2, 0] == 1 || a[2, 1] == 1 || a[2, 2] == 1))   //3-6-9  - нападение
                    {

                        for (int j = 0; j < 3; j++)
                        {
                            if (a[2, j] == 0)
                            {
                                a[2, j] = 1;
                            }
                        }
                        win = 1;
                        paint();
                        cherta = 6;
                    }
                    else
                    {
                        if (((a[0, 0] + a[1, 0] + a[2, 0]) == 2) && (a[0, 0] == 1 || a[1, 0] == 1 || a[2, 0] == 1))   //1-2-3  -нападение
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (a[i, 0] == 0)
                                {
                                    a[i, 0] = 1;
                                }
                            }
                            win = 1;
                            paint();
                            cherta = 1;
                        }
                        else
                        {
                            if (((a[0, 1] + a[1, 1] + a[2, 1]) == 2) && (a[0, 1] == 1 || a[1, 1] == 1 || a[2, 1] == 1))  //4-5-6  - нападение
                            {

                                for (int i = 0; i < 3; i++)
                                {
                                    if (a[i, 1] == 0)
                                    {
                                        a[i, 1] = 1;
                                    }
                                }
                                win = 1;
                                paint();
                                cherta = 2;
                            }
                            else
                            {
                                if (((a[0, 2] + a[1, 2] + a[2, 2]) == 2) && (a[0, 2] == 1 || a[1, 2] == 1 || a[2, 2] == 1))   //7-8-9  - нападение
                                {

                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (a[i, 2] == 0)
                                        {
                                            a[i, 2] = 1;
                                        }
                                    }
                                    win = 1;
                                    paint();
                                    cherta = 3;
                                }
                                else
                                {
                                    if (((a[0, 0] + a[1, 1] + a[2, 2]) == 2) && (a[0, 0] == 1 || a[1, 1] == 1 || a[2, 2] == 1))   //1-5-9  - нападение
                                    {
                                        if (a[0, 0] == 0)
                                            a[0, 0] = 1;
                                        if (a[1, 1] == 0)
                                            a[1, 1] = 1;
                                        if (a[2, 2] == 0)
                                            a[2, 2] = 1;
                                        win = 1;
                                        paint();
                                        cherta = 7;

                                    }
                                    else
                                    {
                                        if (((a[2, 0] + a[1, 1] + a[0, 2]) == 2) && (a[2, 0] == 1 || a[1, 1] == 1 || a[0, 2] == 1))   //3-5-7  - нападение
                                        {
                                            if (a[2, 0] == 0)
                                                a[2, 0] = 1;
                                            if (a[1, 1] == 0)
                                                a[1, 1] = 1;
                                            if (a[0, 2] == 0)
                                                a[0, 2] = 1;
                                            win = 1;
                                            paint();
                                            cherta = 8;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }      // попытка победить.
        private void zachita()
        {
            if ((a[0, 0] + a[0, 1] + a[0, 2]) == 4 && a[0, 0] != 1 && a[0, 1] != 1 && a[0, 2] != 1)   //1-4-7  - защита
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[0, j] == 0)
                    {
                        a[0, j] = 1;
                        hdpc = false;
                        paint();
                    }
                }
            }
            else
            {
                if ((a[1, 0] + a[1, 1] + a[1, 2]) == 4 && a[1, 0] != 1 && a[1, 1] != 1 && a[1, 2] != 1)   //2-5-8  - защита
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (a[1, j] == 0)
                        {
                            a[1, j] = 1;
                            hdpc = false;
                            paint();
                        }
                    }
                }
                else
                {
                    if ((a[2, 0] + a[2, 1] + a[2, 2]) == 4 && a[2, 0] != 1 && a[2, 1] != 1 && a[2, 2] != 1)   //3-6-9  - защита
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (a[2, j] == 0)
                            {
                                a[2, j] = 1;
                                hdpc = false;
                                paint();

                            }
                        }
                    }
                    else
                    {
                        if ((a[0, 0] + a[1, 0] + a[2, 0]) == 4 && a[0, 0] != 1 && a[1, 0] != 1 && a[2, 0] != 1)   //1-2-3  - защита
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (a[i, 0] == 0)
                                {
                                    a[i, 0] = 1;
                                    hdpc = false;
                                    paint();
                                }
                            }
                        }
                        else
                        {
                            if ((a[0, 1] + a[1, 1] + a[2, 1]) == 4 && a[0, 1] != 1 && a[1, 1] != 1 && a[2, 1] != 1)   //4-5-6  - защита
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (a[i, 1] == 0)
                                    {
                                        a[i, 1] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                }
                            }
                            else
                            {
                                if ((a[0, 2] + a[1, 2] + a[2, 2]) == 4 && a[0, 2] != 1 && a[1, 2] != 1 && a[2, 2] != 1)   //7-8-9  - защита
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (a[i, 2] == 0)
                                        {
                                            a[i, 2] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                    }
                                }
                                else
                                {
                                    if ((a[0, 0] + a[1, 1] + a[2, 2]) == 4 && a[0, 0] != 1 && a[1, 1] != 1 && a[2, 2] != 1)   //1-5-9  - защита
                                    {
                                        if (a[0, 0] == 0)
                                            a[0, 0] = 1;
                                        if (a[1, 1] == 0)
                                            a[1, 1] = 1;
                                        if (a[2, 2] == 0)
                                            a[2, 2] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if ((a[2, 0] + a[1, 1] + a[0, 2]) == 4 && a[2, 0] != 1 && a[1, 1] != 1 && a[0, 2] != 1)   //3-5-7  - защита
                                        {
                                            if (a[2, 0] == 0)
                                                a[2, 0] = 1;
                                            if (a[1, 1] == 0)
                                                a[1, 1] = 1;
                                            if (a[0, 2] == 0)
                                                a[0, 2] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }       //конец защиты по 2 правилу
        }      // защита.

        private void krestiki()
        {
            if (xlast == 0 && ylast == 0)  //если 0,0
            {
                if (a[2, 2] == 0)
                {
                    a[2, 2] = 1;
                    hdpc = false;
                    paint();
                }
                else
                {
                    random();
                }
            }
            else
            {
                if (xlast == 2 && ylast == 0)   //2.0
                {
                    if (a[0, 2] == 0)
                    {
                        a[0, 2] = 1;
                        hdpc = false;
                        paint();
                    }
                    else
                    {
                        random();
                    }
                }
                else
                {
                    if (xlast == 0 && ylast == 2)   //0.2
                    {
                        if (a[2, 0] == 0)
                        {
                            a[2, 0] = 1;
                            hdpc = false;
                            paint();
                        }
                        else
                        {
                            random();
                        }
                    }
                    else
                    {
                        if (xlast == 2 && ylast == 2)   //2.2
                        {
                            if (a[0, 0] == 0)
                            {
                                a[0, 0] = 1;
                                hdpc = false;
                                paint();
                            }
                            else
                            {
                                random();
                            }
                        }
                        else
                        {
                            if (xlast == 0 && ylast == 1)   //0.1
                            {
                                if (a[2, 0] == 0)
                                {
                                    a[2, 0] = 1;
                                    hdpc = false;
                                    paint();
                                }
                                else
                                {
                                    if (a[2, 2] == 0)
                                    {
                                        a[2, 2] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        random();
                                    }
                                }
                            }
                            else
                            {
                                if (xlast == 1 && ylast == 0)   //1.0
                                {
                                    if (a[0, 2] == 0)
                                    {
                                        a[0, 2] = 1;
                                        hdpc = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if (a[2, 2] == 0)
                                        {
                                            a[2, 2] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                        else
                                        {
                                            random();
                                        }
                                    }
                                }
                                else
                                {
                                    if (xlast == 2 && ylast == 1)   //2.1
                                    {
                                        if (a[0, 0] == 0)
                                        {
                                            a[0, 0] = 1;
                                            hdpc = false;
                                            paint();
                                        }
                                        else
                                        {
                                            if (a[0, 2] == 0)
                                            {
                                                a[0, 2] = 1;
                                                hdpc = false;
                                                paint();
                                            }
                                            else
                                            {
                                                random();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (xlast == 1 && ylast == 2)   //1.2
                                        {
                                            if (a[0, 0] == 0)
                                            {
                                                a[0, 0] = 1;
                                                hdpc = false;
                                                paint();
                                            }
                                            else
                                            {
                                                if (a[2, 0] == 0)
                                                {
                                                    a[2, 0] = 1;
                                                    hdpc = false;
                                                    paint();
                                                }
                                                else
                                                {
                                                    random();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }      // противоположный ход.
        private void nichia()
        {
            bool nich = true;   //если true - то ничья
            for (int i = 0; i < 3; i++)   //если находим 0, то получим false, то есть не ничья, можно еще ходить
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nich)
                    {
                        if (a[i, j] == 0)
                        {
                            nich = false;
                        }
                    }
                }
            }
            if (nich)
            {
                win = 3;
                qwe = false;
                winner();
            }
        }      // проверка - ничья.
        private void newgame()
        {
            qwe = false;
            flag = true;
            label1.Text = "";
            ph = 0;
            ch = 0;
            ph2 = 0;
            Graphics gPanel = panel1.CreateGraphics();
            panel1.Controls.Clear();
            panel1.Invalidate();
            Pen p = new Pen(Color.Blue, 1);
            Pen p1 = new Pen(Color.Blue, 2);
            gPanel.DrawLine(p, new Point(0, 0), new Point(300, 0));
            gPanel.DrawLine(p, new Point(300, 0), new Point(300, 300));
            gPanel.DrawLine(p, new Point(0, 0), new Point(0, 300));
            gPanel.DrawLine(p, new Point(0, 300), new Point(300, 300));
            gPanel.DrawLine(p1, new Point(100, 0), new Point(100, 300));
            gPanel.DrawLine(p1, new Point(200, 0), new Point(200, 300));
            gPanel.DrawLine(p1, new Point(0, 100), new Point(300, 100));
            gPanel.DrawLine(p1, new Point(0, 200), new Point(300, 200));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    a[i, j] = 0;
                }
            }
            first = true;
            cherta = 0;   // черта для вычеркивания выигрышной комбинации
            xfir = -1; // первый ход юзера
            yfir = -1; // когда он играет крестиками
            xlast = -1; // последний
            ylast = -1; // ход пользователя
            win = 0; // если 1- выиграл комп, 2-пользователь, 3- ничья
            hdpc = false; // true - сейчас ход компьютера, false - ход пользователя
            pc = 1;     // кто ходит первым, 1 - ходит компьютер, 2 первый ходит пользователь
        }      // новая игра.
        private void ugol()
        {
            if (a[0, 0] == 0)
            {
                a[0, 0] = 1;
                hdpc = false;
                paint();
            }
            else
            {
                if (a[2, 0] == 0)
                {
                    a[2, 0] = 1;
                    hdpc = false;
                    paint();
                }
                else
                {
                    if (a[0, 2] == 0)
                    {
                        a[0, 2] = 1;
                        hdpc = false;
                        paint();
                    }
                    else
                    {
                        if (a[2, 2] == 0)
                        {
                            a[2, 2] = 1;
                            hdpc = false;
                            paint();
                        }
                        else
                        {
                            napad();
                        }
                    }
                }
            }
        }      // ход в угол.

        private void napad()
        {
            if ((a[0, 0] + a[0, 1] + a[0, 2]) == 1)  //1-4-7  - нападение
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[0, j] == 0)
                    {
                        a[0, j] = 1;
                    }
                }
                paint();
            }
            else
            {
                if ((a[1, 0] + a[1, 1] + a[1, 2]) == 1)   //2-5-8  - нападение
                {

                    for (int j = 0; j < 3; j++)
                    {
                        if (a[1, j] == 0)
                        {
                            a[1, j] = 1;
                        }
                    }
                    paint();
                }
                else
                {
                    if ((a[2, 0] + a[2, 1] + a[2, 2]) == 1)   //3-6-9  - нападение
                    {

                        for (int j = 0; j < 3; j++)
                        {
                            if (a[2, j] == 0)
                            {
                                a[2, j] = 1;
                            }
                        }
                        paint();
                    }
                    else
                    {
                        if ((a[0, 0] + a[1, 0] + a[2, 0]) == 1)   //1-2-3  -нападение
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                if (a[i, 0] == 0)
                                {
                                    a[i, 0] = 1;
                                }
                            }
                            paint();
                        }
                        else
                        {
                            if ((a[0, 1] + a[1, 1] + a[2, 1]) == 1)  //4-5-6  - нападение
                            {

                                for (int i = 0; i < 3; i++)
                                {
                                    if (a[i, 1] == 0)
                                    {
                                        a[i, 1] = 1;
                                    }
                                }
                                paint();
                            }
                            else
                            {
                                if ((a[0, 2] + a[1, 2] + a[2, 2]) == 1)   //7-8-9  - нападение
                                {

                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (a[i, 2] == 0)
                                        {
                                            a[i, 2] = 1;
                                        }
                                    }
                                    paint();
                                }
                                else
                                {
                                    if ((a[0, 0] + a[1, 1] + a[2, 2]) == 1)   //1-5-9  - нападение
                                    {
                                        if (a[0, 0] == 0)
                                        {
                                            a[0, 0] = 1;
                                        }
                                        else
                                        {
                                            if (a[1, 1] == 0)
                                            {
                                                a[1, 1] = 1;
                                            }
                                            else
                                            {
                                                if (a[2, 2] == 0)
                                                    a[2, 2] = 1;
                                            }
                                        }
                                        paint();
                                    }
                                    else
                                    {
                                        if ((a[2, 0] + a[1, 1] + a[0, 2]) == 1)   //3-5-7  - нападение
                                        {
                                            if (a[2, 0] == 0)
                                            {
                                                a[2, 0] = 1;
                                            }
                                            else
                                            {
                                                if (a[1, 1] == 0)
                                                {
                                                    a[1, 1] = 1;
                                                }
                                                else
                                                {
                                                    if (a[0, 2] == 0)
                                                    {
                                                        a[0, 2] = 1;
                                                    }
                                                }
                                            }
                                            paint();
                                        }
                                        else
                                        {
                                            random();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }      // нападение.

        private void Form1_Load(object sender, EventArgs e)
        { // проверяем активирован ли продукт.
            XorN = Registry.CurrentUser.CreateSubKey("Software\\Mihno\\Крестики нолики");
            if ((XorN.GetValue("Activate") == null) || (XorN.GetValue("HowMuchRun") == null))
            {
                XorN.SetValue("Activate", 0);
                XorN.SetValue("HowMuchRun", 0);
            }
            string load = (string)XorN.GetValue("Activate").ToString();
            if ((load == null) || (Int32.Parse(load) < 0)) XorN.SetValue("Activate", 0);
            if (Int32.Parse(load) == 0)
            {
                load = (string)XorN.GetValue("HowMuchRun").ToString();
                if ((load == null) || (Int32.Parse(load) < 0)) XorN.SetValue("HowMuchRun", 6);
                load = (string)XorN.GetValue("HowMuchRun").ToString();
                int n = Int32.Parse(load);
                if (n < 5)
                {
                    n++;
                    XorN.SetValue("HowMuchRun", n);
                }
                else
                {
                    человекToolStripMenuItem.Visible = false;
                    MessageBox.Show("Триал истёк. Игра с Человеком заблокирована. Для разблокировки активируйте игру");
                }
            }
            if (Int32.Parse(load) == 1) активацияToolStripMenuItem.Visible = false;            
            XorN.Close();
        }    

        private void игратьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                if (pc == 0) // pc равен 0 тогда, когда пользователь еще не выбрал кто будет ходить первым
                {
                   MessageBox.Show("Выберите режим игры!");
                }
                else
                {
                    if (pc == 1)  // первый ход компьютера
                    {
                        qwe = true;
                        hdpc = true;
                        hod1();
                    }
                    if (pc == 2)     // первый ход пользователя
                    {
                        qwe = true;
                        hdpc = false;
                        MessageBox.Show("Ваш ход!");
                    }
                    if (pc == 3)
                    {
                        qwe = true;
                        label1.Text = "Ходят крестики!";
                    }
                }
                flag = false;
            }
            else MessageBox.Show("Начните новую игру");
        }       // работа кнопки меню "Игра -> Играть".
        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newgame();
        }     // работа кнопки меню "Игра -> Новая игра".

        private void пКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            первыйХодToolStripMenuItem.Visible = true;
        }     // работа кнопки меню "С кем играть -> ПК".

        private void человекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc = 3; // pc равен 3 тогда, когда выбрана игра с человеком.
            первыйХодToolStripMenuItem.Visible = false;
        }     // работа кнопки меню "С кем играть -> Человек".

        private void пКToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pc = 1; // pc равен 1 тогда, когда пользователь выбрал, что первым ходит компьютер.
        }     // работа кнопки меню "первый ход -> ПК".

        private void пользовательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc = 2; // pc равен 2 тогда, когда пользователь выбрал, что первым ходит он.
        }     // работа кнопки меню "первый ход -> Пользователь".

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }     // работа кнопки меню "Игра -> Выход".

        private void инфоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработчик: Михов А.А. 331б");
        }     // работа кнопки меню "Справка -> Инфо".

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int k = 0;
            System.IO.StreamReader read = new System.IO.StreamReader(Application.StartupPath.ToString() + "\\Save.ini");  // чтение из файла Save.ini.
            if (read.ReadLine() == null)  // если файл пуст.
            {
                MessageBox.Show("Сохранений нет");
                read.Close();
            }
            else  // если не пуст.
            {
                read.Close();
                System.IO.StreamReader load = new System.IO.StreamReader(Application.StartupPath.ToString() + "\\Save.ini");
                while ((line = load.ReadLine()) != null) // считывание из файла построчно.
                {
                    text[k] = line;
                    k++;

                }                
                load.Close();
                k = 0;
                for (int i = 0; i < 3; i++)   // заполнение массива с данными о том куда сходили.
                {
                    for (int j = 0; j < 3; j++)
                    {
                        a[i, j] = Int32.Parse(text[k]);
                        k++;
                    }
                }
                pc = Int32.Parse(text[9]);  // заполнение остальных важных переменных.
                hdpc = Convert.ToBoolean(text[10]);
                first = Convert.ToBoolean(text[11]);
                flag = Convert.ToBoolean(text[12]);
                xfir = Int32.Parse(text[13]);
                yfir = Int32.Parse(text[14]);
                xlast = Int32.Parse(text[15]);
                ylast = Int32.Parse(text[16]);
                win = Int32.Parse(text[17]);
                qwe = Convert.ToBoolean(text[18]);
                cng = Int32.Parse(text[19]);
                korn = Int32.Parse(text[20]);
                label1.Text = text[21];
                paintload();  // отрисовка панели в зависимости от полученных данных.
            }
        }      // работа кнопки меню "Игра -> Загрузка".

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        { // сохранение данных игры.
            for (int i = 0; i < 3; i++) // сохранение массива с ходами.
            {
                for (int j = 0; j < 3; j++)
                {
                    file.Add(a[i, j].ToString());
                }
            }
            file.Add(pc.ToString());  // сохранение данных игры.
            file.Add(hdpc.ToString());
            file.Add(first.ToString());
            file.Add(flag.ToString());
            file.Add(xfir.ToString());
            file.Add(yfir.ToString());
            file.Add(xlast.ToString());
            file.Add(ylast.ToString());
            file.Add(win.ToString());
            file.Add(qwe.ToString());
            file.Add(cng.ToString());
            file.Add(korn.ToString());
            file.Add(label1.Text);
            System.IO.File.WriteAllLines(Application.StartupPath.ToString() + "\\Save.ini", file);  // запись данных игры в файл.
        }     // работа при закрытии формы.

        private void активацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activate.Visible = true;
            textBox1.Visible = true;
            label1.Text = "Введите код";
        }     // работа при меню "Справк -> Активация".

        private void activate_Click(object sender, EventArgs e)
        {
            int l = 0;  // счётчик для массива.
            int act = 0;  // флаг активации.
            while (l < password.Length) // проход массива паролей.
            {
                if (textBox1.Text == password[l]) // если пароль подошёл.
                {
                    человекToolStripMenuItem.Visible = true;  
                    act = 1;
                    XorN = Registry.CurrentUser.OpenSubKey("Software\\Mihno\\Крестики нолики", true);
                    XorN.SetValue("Activate", 1); // отмечаем в реестре что продукт активирован.
                    XorN.Close();
                    break;
                }
                else l++;
            }
            if (act == 1)
            {
                MessageBox.Show("Активация прошла успешно");
                activate.Visible = false;
                textBox1.Visible = false;
                label1.Text = "";
                активацияToolStripMenuItem.Visible = false;
            }
            else MessageBox.Show("Активация провалилась");
        }      // работа кнопки активации

    }
    
}
