using System;
using System.Windows.Forms;

namespace ASK_P5
{
    public partial class Form1 : Form
    {
        private string AX, BX, CX, DX;

        public Form1()
        {
            InitializeComponent();
            InitializeRejestr();
        }

        private void InitializeRejestr()
        {
            AX = "0000000000000000";
            BX = "0000000000000000";
            CX = "0000000000000000";
            DX = "0000000000000000";

            rejestrA.Text = AX;
            rejestrB.Text = BX;
            rejestrC.Text = CX;
            rejestrD.Text = DX;
        }
        private string Konwertujna16bit(int value)
        {
            return Convert.ToString(value & 0xFFFF, 2).PadLeft(16, '0');
        }
        private void Operacja()
        {
            string operacja = textBoxOperacja.Text.ToUpper();
            if (operacja != "ADD" && operacja != "MOV" && operacja != "SUB")
            {
                MessageBox.Show("NIEPOPRAWNA OPERACJA");
            }
        }
        private void RejDoc()
        {
            string rejdoc = rejDocelowy.Text.ToUpper();
            if (rejdoc != "AX" && rejdoc != "BX" && rejdoc != "CX" && rejdoc != "DX")
            {
                MessageBox.Show("NIEPRAWIDŁOWY REJESTR DOCELOWY");
            }
        }
        private void Wynik(object sender, EventArgs e)
        {
            Operacja();
            RejDoc();

            if (textBoxOperacja.Text.ToUpper() == "ADD")
            {
                DodajLiczbe();
            }
            else if (textBoxOperacja.Text.ToUpper() == "SUB")
            {
                OdejmijLiczbe();
            }
            else if(textBoxOperacja.Text.ToUpper() == "MOV")
            {
                PrzeniesWartosc();
            }
            else
            {
                MessageBox.Show("BŁĄD OPERACJI");
            }

            TrybKrokowy();
        }

        private void Docelowy(string docelowy)
        {
            if (docelowy != "AX" && docelowy != "BX" && docelowy != "CX" && docelowy != "DX")
            {
                MessageBox.Show("NIEPRAWIDŁOWY REJESTR DOCELOWY");
            }

        }

        private void KtoraOp(string op)
        {

            if (op != "ADD" && op != "SUB" && op != "MOV")
            {
                MessageBox.Show("BŁĘDNA OPERACJA");
            }


        }
        private void DodajLiczbe()
        {
            string rejestrDocelowy = rejDocelowy.Text.ToUpper();

            if (int.TryParse(wartosc.Text, out int liczbaD))
            {
                switch (rejestrDocelowy)
                {
                    case "AX":
                        AX = Konwertujna16bit(Convert.ToUInt16(AX, 2) + liczbaD);
                        rejestrA.Text = AX;
                        break;
                    case "BX":
                        BX = Konwertujna16bit(Convert.ToUInt16(BX, 2) + liczbaD);
                        rejestrB.Text = BX;
                        break;
                    case "CX":
                        CX = Konwertujna16bit(Convert.ToUInt16(CX, 2) + liczbaD);
                        rejestrC.Text = CX;
                        break;
                    case "DX":
                        DX = Konwertujna16bit(Convert.ToUInt16(DX, 2) + liczbaD);
                        rejestrD.Text = DX;
                        break;
                    default:
                        MessageBox.Show("BŁĘDNY REJESTR");
                        break;
                }
            }
            else
            {
                MessageBox.Show("BŁĘDNA LICZBA");
            }
        }
        private void OdejmijLiczbe()
        {
            string rejestrDoc = rejDocelowy.Text.ToUpper();

            if (int.TryParse(wartosc.Text, out int liczbaD))
            {
                switch (rejestrDoc)
                {
                    case "AX":
                        int wynikAx = Math.Max(0, Convert.ToInt32(AX, 2) - liczbaD);
                        AX = Konwertujna16bit(wynikAx);
                        rejestrA.Text = AX;
                        break;
                    case "BX":
                        int wynikBx = Math.Max(0, Convert.ToInt32(BX, 2) - liczbaD);
                        BX = Konwertujna16bit(wynikBx);
                        rejestrB.Text = BX;
                        break;
                    case "CX":
                        int wynikCx = Math.Max(0, Convert.ToInt32(CX, 2) - liczbaD);
                        CX = Konwertujna16bit(wynikCx);
                        rejestrC.Text = CX;
                        break;
                    case "DX":
                        int wynikDx = Math.Max(0, Convert.ToInt32(DX, 2) - liczbaD);
                        DX = Konwertujna16bit(wynikDx);
                        rejestrD.Text = DX;
                        break;
                    default:
                        MessageBox.Show("BŁĘDNY REJESTR DOCELOWY");
                        break;
                }
            }
            else
            {
                MessageBox.Show("BŁĘDNA LICZBA");
            }
        }
        private void PrzeniesWartosc()
        {
            string rejestrDocelowy = rejDocelowy.Text.ToUpper().Trim();
            string rejestrZrodlowy = rejZrodlowy.Text.ToUpper().Trim();

            TextBox textboxZ = null;

            switch (rejestrZrodlowy)
            {
                case "AX":
                    textboxZ = rejestrA;
                    break;
                case "BX":
                    textboxZ = rejestrB;
                    break;
                case "CX":
                    textboxZ = rejestrC;
                    break;
                case "DX":
                    textboxZ = rejestrD;
                    break;
                default:
                    MessageBox.Show("BŁĘDNY REJESTR ŹRÓDŁOWY");
                    return;
            }

            TextBox textboxDo = null;

            switch (rejestrDocelowy)
            {
                case "AX":
                    textboxDo = rejestrA;
                    AX = textboxZ.Text;
                    break;
                case "BX":
                    textboxDo = rejestrB;
                    BX = textboxZ.Text;
                    break;
                case "CX":
                    textboxDo = rejestrC;
                    CX = textboxZ.Text;
                    break;
                case "DX":
                    textboxDo = rejestrD;
                    DX = textboxZ.Text;
                    break;
                default:
                    MessageBox.Show("BŁĘDNY REJESTR DOCELOWY");
                    return;
            }

            textboxDo.Text = textboxZ.Text;
        }

        private void TrybKrokowy()
        {
            string[] texts = { textBoxOperacja.Text.ToUpper(), rejDocelowy.Text.ToUpper(), rejZrodlowy.Text.ToUpper(), wartosc.Text };
            string result = string.Join(" ", texts);
            textBoxKrokowy.Text += result + Environment.NewLine;
            textBoxOperacja.Clear();
            rejDocelowy.Clear();
            rejZrodlowy.Clear();
            wartosc.Clear();
        }
        private void Dodaj(string docelowy, string dane)
        {
            string rejestrDocelowy = docelowy;

            if (int.TryParse(dane, out int liczbaD))
            {
                switch (rejestrDocelowy)
                {
                    case "AX":
                        AX = Konwertujna16bit(Convert.ToUInt16(AX, 2) + liczbaD);
                        rejestrA.Text = AX;
                        break;
                    case "BX":
                        BX = Konwertujna16bit(Convert.ToUInt16(BX, 2) + liczbaD);
                        rejestrB.Text = BX;
                        break;
                    case "CX":
                        CX = Konwertujna16bit(Convert.ToUInt16(CX, 2) + liczbaD);
                        rejestrC.Text = CX;
                        break;
                    case "DX":
                        DX = Konwertujna16bit(Convert.ToUInt16(DX, 2) + liczbaD);
                        rejestrD.Text = DX;
                        break;
                    default:
                        MessageBox.Show("BŁĘDNY REJESTR DOCELOWY");
                        break;
                }
            }
            else
            {
                MessageBox.Show("BŁĘDNA LICZBA");
            }
        }
        private void Odejmij(string docelowy, string dane)
        {
            string rejestrDocelowy = docelowy;

            if (int.TryParse(dane, out int liczbaD))
            {
                switch (rejestrDocelowy)
                {
                    case "AX":
                        int wynikAx = Math.Max(0, Convert.ToInt32(AX, 2) - liczbaD);
                        AX = Konwertujna16bit(wynikAx);
                        rejestrA.Text = AX;
                        break;
                    case "BX":
                        int wynikBx = Math.Max(0, Convert.ToInt32(BX, 2) - liczbaD);
                        BX = Konwertujna16bit(wynikBx);
                        rejestrB.Text = BX;
                        break;
                    case "CX":
                        int wynikCx = Math.Max(0, Convert.ToInt32(CX, 2) - liczbaD);
                        CX = Konwertujna16bit(wynikCx);
                        rejestrC.Text = CX;
                        break;
                    case "DX":
                        int wynikDx = Math.Max(0, Convert.ToInt32(DX, 2) - liczbaD);
                        DX = Konwertujna16bit(wynikDx);
                        rejestrD.Text = DX;
                        break;
                    default:
                        MessageBox.Show("BŁĘDNY REJESTR DOCELOWY");
                        break;
                }
            }
            else
            {
                MessageBox.Show("BŁĘDNA LICZBA");
            }
        }

        private void Przenies(string zrodlowy, string docelowy)
        {
            TextBox textboxZrodlowy = null;

            switch (zrodlowy)
            {
                case "AX":
                    textboxZrodlowy = rejestrA;
                    break;
                case "BX":
                    textboxZrodlowy = rejestrB;
                    break;
                case "CX":
                    textboxZrodlowy = rejestrC;
                    break;
                case "DX":
                    textboxZrodlowy = rejestrD;
                    break;
                default:
                    MessageBox.Show("BŁĘDNY REJESTR ŹRÓDŁOWY");
                    return;
            }

            TextBox textboxDocelowy = null;

            switch (docelowy)
            {
                case "AX":
                    textboxDocelowy = rejestrA;
                    AX = textboxZrodlowy.Text;
                    break;
                case "BX":
                    textboxDocelowy = rejestrB;
                    BX = textboxZrodlowy.Text;
                    break;
                case "CX":
                    textboxDocelowy = rejestrC;
                    CX = textboxZrodlowy.Text;
                    break;
                case "DX":
                    textboxDocelowy = rejestrD;
                    DX = textboxZrodlowy.Text;
                    break;
                default:
                    MessageBox.Show("BŁĘDNY REJESTR DOCELOWY");
                    return;
            }

            textboxDocelowy.Text = textboxZrodlowy.Text;
        }
        private void Wykonaj(object sender, EventArgs e)
        {
            string[] linieRozkazow = textBoxLista.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string liniaRozkazow in linieRozkazow)
            {
                string[] rozkazyArray = liniaRozkazow.Split(' ');

                if (rozkazyArray.Length == 4)
                {
                    string operacja = rozkazyArray[0];
                    string docelowy = rozkazyArray[1];
                    string zrodlowy = rozkazyArray[2];
                    string dane = rozkazyArray[3];

                    KtoraOp(operacja);
                    Docelowy(docelowy);

                    if (operacja == "ADD")
                    {
                        Dodaj(docelowy, dane);
                    }
                    else if (operacja == "SUB")
                    {
                        Odejmij(docelowy, dane);
                    }
                    else if (operacja == "MOV")
                    {
                        Przenies(zrodlowy, docelowy);
                    }
                    else
                    {
                        MessageBox.Show("BŁĘDNA OPERACJA");
                    }
                }
                else
                {
                    MessageBox.Show("BŁĘDNY ROZKAZ");
                }
            }

        }
        private void Przekaz(object sender, EventArgs e)
        {
            string[] texts = { textBoxOperacja.Text.ToUpper(), rejDocelowy.Text.ToUpper(), rejZrodlowy.Text.ToUpper(), wartosc.Text };
            string result = string.Join(" ", texts);
            textBoxLista.Text += result + Environment.NewLine;
            textBoxOperacja.Clear();
            rejDocelowy.Clear();
            rejZrodlowy.Clear();
            wartosc.Clear();

        }
        private void Clear(object sender, EventArgs e)
        {
            textBoxOperacja.Clear();
            rejDocelowy.Clear();
            rejZrodlowy.Clear();
            wartosc.Clear();
            textBoxKrokowy.Clear();
            textBoxLista.Clear();
            InitializeRejestr();
        }

        private void textBoxLista_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxOperacja_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
