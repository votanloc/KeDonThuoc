namespace KeDonThuoc
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnKeDonThuoc_Click(object sender, EventArgs e)
        {
            KeDon keDon = new KeDon();
            keDon.Show();
        }
    }
}
