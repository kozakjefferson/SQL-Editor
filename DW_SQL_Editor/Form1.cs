using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DW_SQL_Editor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DW SQL Editor is a free tool to edit your SQL files.", "About DW SQL Editor", MessageBoxButtons.OK);
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for using this simple app.", "DW SQL Editor", MessageBoxButtons.OK);
            Application.Exit();
        }

        private void tsmSaveFile_Click(object sender, EventArgs e)
        {

        }

        private void tsmOpenFile_Click(object sender, EventArgs e)
        {
            dlgOpenFile.InitialDirectory = @"C:\";
            dlgOpenFile.Title = "Browse for the SQL File";
            dlgOpenFile.CheckFileExists = true;
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.DefaultExt = "sql";
            dlgOpenFile.Filter = "SQL Files|*.sql|Text Files|*.txt";
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                boxCode.Text = File.ReadAllText(dlgOpenFile.FileName);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            dlgOpenFile.InitialDirectory = @"C:\";
            dlgOpenFile.Title = "Browse for the SQL File";
            dlgOpenFile.CheckFileExists = true;
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.DefaultExt = "sql";
            dlgOpenFile.Filter = "SQL Files|*.sql|Text Files|*.txt";
            if (dlgOpenFile.ShowDialog()==DialogResult.OK)
            {
                boxCode.Text = File.ReadAllText(dlgOpenFile.FileName);
            }
        }

        //Memory Stream for saving file 
        MemoryStream fstream = new MemoryStream();

        //Saving content from the TextBox
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            RichTextBox boxcode = new RichTextBox();
            boxcode.SaveFile(fstream, RichTextBoxStreamType.UnicodePlainText);
            fstream.WriteByte(13);
            SaveFileDialog dlgSaveFile = new SaveFileDialog();
            dlgSaveFile.CreatePrompt = true;
            dlgSaveFile.OverwritePrompt = true;
            dlgSaveFile.FileName = boxcode.Text;
            dlgSaveFile.Filter = "SQL files (*.sql)|*.sql";
            dlgSaveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DialogResult result = dlgSaveFile.ShowDialog();
            Stream fileStream;

            if (result == DialogResult.OK)
            {
                // Open the file and copy it's content to the stream
                // Starts at position 0 of the stream
                // Then it closed and saved
                fileStream = dlgSaveFile.OpenFile();
                fstream.Position = 0;
                fstream.WriteTo(fileStream);
                fileStream.Close();
            }
        }

    }
}
