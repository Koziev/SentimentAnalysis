using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoringCorpusEditor
{
    public partial class ScoringCorpusEditorMainForm : Form
    {
        public static string corpus_path = "";
        public static string current_line_index = "";
        public ScoringCorpusEditorMainForm()
        {
            InitializeComponent();
        }


        private void cb_select_path_Click(object sender, EventArgs e)
        {
            var dlg = new System.Windows.Forms.OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                corpus_path = dlg.FileName;
                tb_path.Text = corpus_path;
            }
        }

        private string GetConfigFilePath()
        {
            const string CONFIG_FILENAME = "ScoringCorpusEditor.cfg";
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CONFIG_FILENAME);
        }

        private void LoadConfig()
        {
            string config_path = GetConfigFilePath();

            if (System.IO.File.Exists(config_path))
            {
                using (System.IO.StreamReader rdr = new System.IO.StreamReader(config_path))
                {
                    corpus_path = rdr.ReadLine().Trim();
                    current_line_index = rdr.ReadLine().Trim();
                }
            }

            return;
        }

        private void StoreConfig()
        {
            string config_path = GetConfigFilePath();

            using (System.IO.StreamWriter wrt = new System.IO.StreamWriter(config_path))
            {
                wrt.WriteLine("{0}", corpus_path);
                wrt.WriteLine("{0}", current_line_index);
            }

            return;
        }



        private void cb_load_corpus_Click(object sender, EventArgs e)
        {
            LoadCorpus();
            StoreConfig();
            return;
        }

        private void ScoringCorpusEditorMainForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
            tb_path.Text = corpus_path;
            LoadCorpus();
            SelectCurrentLine();
        }

        private void SelectCurrentLine()
        {
            for (int i = 0; i < lv_lines.SelectedIndices.Count; ++i)
            {
                lv_lines.Items[lv_lines.SelectedIndices[i]].Selected = false;
            }


            if (!string.IsNullOrEmpty(current_line_index))
            {
                int ii = int.Parse(current_line_index);
                if (ii < lv_lines.Items.Count)
                {
                    lv_lines.Items[ii].Selected = true;
                    lv_lines.EnsureVisible(ii);
                }
            }

            return;
        }


        private List<long> file_pos_of_line = new List<long>();

        private static System.Data.SQLite.SQLiteConnection GetConnection()
        {
            System.Data.SQLite.SQLiteConnectionStringBuilder cnx_build = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            cnx_build.DataSource = corpus_path;

            System.Data.SQLite.SQLiteConnection cnx = new System.Data.SQLite.SQLiteConnection(cnx_build.ConnectionString);
            cnx.Open();
            return cnx;
        }

        private void LoadCorpus()
        {
            if (!string.IsNullOrEmpty(corpus_path))
            {
                try
                {
                    System.Data.SQLite.SQLiteConnection cnx = GetConnection();
                    System.Data.SQLite.SQLiteTransaction tx = cnx.BeginTransaction(IsolationLevel.ReadCommitted);

                    // подготовим экранный список.

                    lv_lines.Items.Clear();
                    file_pos_of_line.Clear();
                    tb_curent.Text = "";
                    System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT COUNT(*) FROM Sent_DATASET", cnx);
                    int nline = int.Parse(cmd.ExecuteScalar().ToString());

                    cnx.Close();

                    lv_lines.VirtualListSize = nline;

                    cb_save_corpus.Enabled = true;

                    if (corpus_path.Contains("polarity"))
                    {
                        rb_polarity.Checked = true;
                    }
                    else if (corpus_path.Contains("modifier"))
                    {
                        rb_modifier.Checked = true;
                    }
                    else if (corpus_path.Contains("aspect"))
                    {
                        rb_aspect.Checked = true;
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

            return;
        }

        HashSet<string> aspect_names = new HashSet<string>();
        HashSet<string> modifier_names = new HashSet<string>();

        private void lv_lines_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem lvi = new ListViewItem();

            System.Data.SQLite.SQLiteConnection cnx = null;
            try
            {
                cnx = GetConnection();
                System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT str, labels FROM Sent_DATASET WHERE id=?", cnx);
                cmd.Parameters.AddWithValue("id", e.ItemIndex);
                using (System.Data.SQLite.SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    string sent = rdr["str"].ToString();
                    string labels = rdr["labels"].ToString();

                    lvi.Text = e.ItemIndex.ToString();

                    ListViewItem.ListViewSubItem lvsi1 = new ListViewItem.ListViewSubItem();
                    lvsi1.Text = sent;
                    lvi.SubItems.Add(lvsi1);

                    ListViewItem.ListViewSubItem lvsi2 = new ListViewItem.ListViewSubItem();
                    lvsi2.Text = labels;
                    lvi.SubItems.Add(lvsi2);

                    if (rb_polarity.Checked)
                    {
                        bool plus = labels.Contains("posit=1");
                        bool minus = labels.Contains("negat=1");

                        if (plus && minus)
                        {
                            lvi.BackColor = Color.Yellow;
                        }
                        else if (minus)
                        {
                            lvi.BackColor = Color.LightPink;
                        }
                        else if (plus)
                        {
                            lvi.BackColor = Color.LightGreen;
                        }
                    }
                    else if (rb_aspect.Checked)
                    {
                        if (aspect_names.Count == 0)
                        {
                            foreach (string s in cbx_aspects.Items)
                            {
                                aspect_names.Add(s);
                            }
                        }

                        foreach (string label in labels.Split(' '))
                        {
                            if (aspect_names.Contains(label.Split('=')[0]))
                                lvi.BackColor = Color.LightBlue;
                        }
                    }
                    else if (rb_modifier.Checked)
                    {
                        if (modifier_names.Count == 0)
                        {
                            foreach (string s in cbx_modifiers.Items)
                            {
                                modifier_names.Add(s);
                            }
                        }

                        foreach (string label in labels.Split(' '))
                        {
                            if (modifier_names.Contains(label.Split('=')[0]))
                                lvi.BackColor = Color.LawnGreen;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                cnx.Close();
            }

            e.Item = lvi;

            return;
        }

        private void lv_lines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_lines.SelectedIndices.Count == 1)
            {
                current_line_index = lv_lines.SelectedIndices[0].ToString();

                System.Data.SQLite.SQLiteConnection cnx = GetConnection();
                System.Data.SQLite.SQLiteTransaction tx = cnx.BeginTransaction(IsolationLevel.ReadCommitted);

                int cur_index = lv_lines.SelectedIndices[0];

                System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT str, labels FROM Sent_DATASET WHERE id=?", cnx, tx);
                cmd.Parameters.AddWithValue("id", cur_index);

                using (System.Data.SQLite.SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    string sent = rdr["str"].ToString();
                    string labels = rdr["labels"].ToString();
                    tb_curent.Text = sent;
                    tb_labels.Text = labels;
                }

                tx.Commit();
                cnx.Close();
            }
            else
            {
                current_line_index = "";
            }

            StoreConfig();
        }

        private void StoreLabels(params string[] labels)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                System.Data.SQLite.SQLiteConnection cnx = GetConnection();

                int cur_index = lv_lines.SelectedIndices[0];

                System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT labels FROM Sent_DATASET WHERE id=?", cnx);
                cmd.Parameters.AddWithValue("id", cur_index);
                string old_labels = cmd.ExecuteScalar().ToString();

                string[] lx = old_labels.Split(' ');

                List<string> remove = new List<string>();

                foreach (string l in labels)
                {
                    string label_name = l.Split('=')[0];
                    string label_value = l.Split('=')[1];
                    if (label_name == "negat" || label_name == "posit" || label_name == "nosign")
                    {
                        remove.Add("negat");
                        remove.Add("nosign");
                        remove.Add("posit");
                    }
                    else
                    {
                        remove.Add(label_name);
                    }
                }


                string new_labels = string.Join(" ", lx.Where(z => !remove.Contains(z.Split('=')[0])).Concat(labels).ToArray());
                new_labels = new_labels.Trim();

                cmd = new System.Data.SQLite.SQLiteCommand("UPDATE Sent_DATASET SET labels=? WHERE id=?", cnx);
                cmd.Parameters.AddWithValue("labels", new_labels);
                cmd.Parameters.AddWithValue("id", cur_index);
                cmd.ExecuteNonQuery();

                cnx.Close();


                lv_lines.Focus();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            Cursor.Current = Cursors.Default;

            return;
        }

        private void cb_plus_Click(object sender, EventArgs e)
        {
            StoreLabels("posit=1");
            lv_lines.Focus();
        }

        private void cb_0_Click(object sender, EventArgs e)
        {
            StoreLabels("nosign=1");
            lv_lines.Focus();
        }

        private void cb_minus_Click(object sender, EventArgs e)
        {
            StoreLabels("negat=1");
            lv_lines.Focus();
        }

        private void cb_save_corpus_Click(object sender, EventArgs e)
        {
            SaveCorpus();
        }

        private static void SaveCorpus()
        {
            Cursor.Current = Cursors.WaitCursor;

            System.Data.SQLite.SQLiteConnection cnx = GetConnection();
            System.Data.SQLite.SQLiteTransaction tx = cnx.BeginTransaction(IsolationLevel.ReadCommitted);

            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT id, str, labels FROM Sent_DATASET ORDER BY id", cnx, tx);

            string export_path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(corpus_path), System.IO.Path.GetFileNameWithoutExtension(corpus_path) + ".dat");

            using (System.IO.StreamWriter wrt = new System.IO.StreamWriter(export_path))
            {
                using (System.Data.SQLite.SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string sent = rdr["str"].ToString();
                        string label = rdr["labels"].ToString();

                        wrt.WriteLine("{0}\t{1}", sent, label);
                    }
                }
            }

            cnx.Close();
            Cursor.Current = Cursors.Default;

            return;
        }

        private void cb_aspect_0_Click(object sender, EventArgs e)
        {
            StoreLabels(cbx_aspects.Text + "=0");
        }

        private void cb_aspect_1_Click(object sender, EventArgs e)
        {
            StoreLabels(cbx_aspects.Text + "=1");
        }

        private void cb_modifier_0_Click(object sender, EventArgs e)
        {
            StoreLabels(cbx_modifiers.Text + "=0");
        }

        private void cb_modifier_1_Click(object sender, EventArgs e)
        {
            StoreLabels(cbx_modifiers.Text + "=1");
        }

        private void cb_plus_minus_Click(object sender, EventArgs e)
        {
            StoreLabels("negat=1", "posit=1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(tb_id.Text, out id))
            {
                lv_lines.SelectedIndices.Clear();
                lv_lines.SelectedIndices.Add(id);
                lv_lines.EnsureVisible(id);
                lv_lines.Focus();
            }
        }
    }
}