using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_sinh_vien
{

    public partial class frmQuanli : Form
    {
        QuanLySinhVien qlsv;
        public frmQuanli()
        {
            InitializeComponent();
        }
        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            List<string> cn = new List<string>();
            sv.MaSo = this.msbMaSo.Text;
            sv.HovaTen = this.txtHoTen.Text;
            if (rbNam.Checked)
                gt = false;
            sv.Phai = gt;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.Lop = this.cbbLop.Text;
            sv.SoDienThoai = this.msbSoDT.Text;
            sv.Email = this.txtEmail.Text;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.Hinh = this.txtHinh.Text;

            return sv;


        }
        private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = lvitem.SubItems[0].Text;
            sv.HovaTen = lvitem.SubItems[1].Text;
            sv.Phai = false;
            if (lvitem.SubItems[2].Text == "Nam")
                sv.Phai = true;
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[3].Text);
            sv.Lop = lvitem.SubItems[4].Text;
            sv.SoDienThoai = lvitem.SubItems[5].Text;
            sv.Email = lvitem.SubItems[6].Text;
            sv.DiaChi = lvitem.SubItems[7].Text;
            sv.Hinh = lvitem.SubItems[8].Text;
            return sv;

        }
        private void ThietLapThongTin(SinhVien sv)
        {
            this.msbSoDT.Text = sv.MaSo;
            this.txtHoTen.Text = sv.HovaTen;
            if (sv.Phai)
                this.rbNam.Checked = true;
            else
                this.rbNu.Checked = true;
            this.dtpNgaySinh.Value = sv.NgaySinh;
            this.cbbLop.Text = sv.Lop;
            this.msbSoDT.Text = sv.SoDienThoai;
            this.txtEmail.Text = sv.Email;
            this.txtDiaChi.Text = sv.DiaChi;
            this.txtHinh.Text = sv.Hinh;
            this.ptbHinh.ImageLocation = sv.Hinh;



        }
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MaSo);
            lvitem.SubItems.Add(sv.HovaTen);
            string gt = "Nam";
            if (sv.Phai)
                gt = "Nữ";
            lvitem.SubItems.Add(gt);
            lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SoDienThoai);
            lvitem.SubItems.Add(sv.Email);
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.Hinh);
            this.lvSinhVien.Items.Add(lvitem);
        }
        private void LoadListView()
        {
            this.lvSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.DanhSach)
            {
                ThemSV(sv);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvitem = this.lvSinhVien.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvitem);
                ThietLapThongTin(sv);
            }
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Picture";
            dlg.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
                            + "*.jpg;*.jpeg;*.gif;*.bmp;"
                            + "*.tif;*.tiff;*.png|"
                            + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
                            + "GIF files (*.gif)|*.gif|"
                            + "BMP files (*.bmp)|*.bmp|"
                            + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
                            + "PNG files (*.png)|*.png|"
                            + "All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var filename = dlg.FileName;
                txtHinh.Text = filename;
                ptbHinh.Load(filename);
            }
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {

            this.msbMaSo.Text = "";
            this.txtHoTen.Text = "";
            this.rbNam.Checked = true;
            this.dtpNgaySinh.Value = DateTime.Now;
            this.cbbLop.Text = this.cbbLop.Items[0].ToString();
            this.msbSoDT.Text = "";
            this.txtEmail.Text = "";
            this.txtDiaChi.Text = "";
            this.txtHinh.Text = "";
            this.ptbHinh.ImageLocation = "";


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn lưu lại danh sách không?", "Thông báo", MessageBoxButtons.YesNoCancel);
            if (dlr == DialogResult.Yes)
            {
                SinhVien sv = GetSinhVien();
                SinhVien kq = qlsv.Tim(sv.MaSo, delegate (object obj1, object obj2)
                {
                    return (obj2 as SinhVien).MaSo.CompareTo(obj1.ToString());
                });
                this.qlsv.Them(sv);
                this.LoadListView();
                MessageBox.Show("Danh sách đã được lưu");
            }
            else
            { Application.Exit(); }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            SinhVien sv = GetSinhVien();
            SinhVien kq = qlsv.Tim(sv.MaSo, delegate (object obj1, object obj2)
            {
                return (obj2 as SinhVien).MaSo.CompareTo(obj1.ToString());
            });
            if (kq != null)
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi thêm dữ liệu",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.qlsv.Them(sv);
                this.LoadListView();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();


        }


        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
        private int SoSanhTheoMa(object obj1, object obj2)
        {
            SinhVien sv = obj2 as SinhVien;
            return sv.MaSo.CompareTo(obj1);

        }
    }
}
