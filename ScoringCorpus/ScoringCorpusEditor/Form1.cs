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

            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Показываем текст случившейся ошибки и позволяем работать дальше в расчете на то,
        /// что пользователь устранит причину ошибки, например - даст разрешение на запись в
        /// файл конфигурации и т.д.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке</param>
        private static void ShowErrorAndContinue(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Показываем текст случившейся ошибки и прекращаем работу, так как дальше
        /// может возкнуть опасность повреждения БД.
        /// </summary>
        /// <param name="message"></param>
        private static void ShowErrorAndAbort(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, "Фатальная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            System.Windows.Forms.Application.Exit();
        }

        private static void ShowInfo(string message)
        {
            System.Windows.Forms.MessageBox.Show(message);
        }

        private static log4net.ILog GetLog()
        {
            return log4net.LogManager.GetLogger(typeof(ScoringCorpusEditorMainForm));
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
            var log = GetLog();
            log.InfoFormat("Reading config from {0}", config_path);

            if (System.IO.File.Exists(config_path))
            {
                try
                {
                    using (System.IO.StreamReader rdr = new System.IO.StreamReader(config_path))
                    {
                        string cfg_json = rdr.ReadToEnd();
                        EditorConfig cfg = Newtonsoft.Json.JsonConvert.DeserializeObject<EditorConfig>(cfg_json);
                        corpus_path = cfg.corpus_path;
                        current_line_index = cfg.current_line_index;
                    }
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Error when reading from config file {0}: {1}", config_path, ex.Message);
                    ShowErrorAndContinue($"При чтении из файла конфигурации {config_path} возникла ошибка: {ex.Message}");
                }
            }

            return;
        }

        private void StoreConfig()
        {
            string config_path = GetConfigFilePath();
            var log = GetLog();
            log.InfoFormat("Writing config to {0}", config_path);

            try
            {
                using (System.IO.StreamWriter wrt = new System.IO.StreamWriter(config_path))
                {
                    EditorConfig cfg = new EditorConfig { corpus_path = corpus_path, current_line_index = current_line_index };
                    string cfg_json = Newtonsoft.Json.JsonConvert.SerializeObject(cfg);
                    wrt.Write(cfg_json);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error when writing to config file {0}: {1}", config_path, ex.Message);
                ShowErrorAndContinue($"При записи в файл конфигурации {config_path} возникла ошибка: {ex.Message}");
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

            string connection_string = cnx_build.ConnectionString;

            var log = GetLog();
            log.InfoFormat("Opening connection {0}", connection_string);

            try
            {
                System.Data.SQLite.SQLiteConnection cnx = new System.Data.SQLite.SQLiteConnection(connection_string);
                cnx.Open();
                return cnx;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error occured when opening connection {0}: {1}", connection_string, ex.Message);
                ShowErrorAndAbort($"При открытии соединения с БД {connection_string} возникла ошибка: {ex.Message}");
                throw;
            }
        }

        private void LoadCorpus()
        {
            if (!string.IsNullOrEmpty(corpus_path))
            {
                var log = GetLog();
                try
                {
                    log.InfoFormat("LoadCorpus corpus_path={0}", corpus_path);
                    int nline = 0;
                    using (System.Data.SQLite.SQLiteConnection cnx = GetConnection())
                    {
                        System.Data.SQLite.SQLiteTransaction tx = cnx.BeginTransaction(IsolationLevel.ReadCommitted);

                        // подготовим экранный список.

                        lv_lines.Items.Clear();
                        file_pos_of_line.Clear();
                        tb_curent.Text = "";
                        System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT COUNT(*) FROM Sent_DATASET", cnx);
                        nline = int.Parse(cmd.ExecuteScalar().ToString());
                        log.InfoFormat("nline={0}", nline);
                    } // cnx.Close();

                    lv_lines.VirtualListSize = nline;

                    cb_save_corpus.Enabled = true;

                    // В зависимости от имени файла базы выбираем режим разметки и
                    // включаем доступность визуальных элементов на форме.
                    if (corpus_path.ContainsCI("polarity"))
                    {
                        // Корпус с полярностью высказываний.
                        rb_polarity.Checked = true;
                    }
                    else if (corpus_path.ContainsCI("modifier"))
                    {
                        // Корпус с усиливающими/ослабляющими модификаторами
                        rb_modifier.Checked = true;
                    }
                    else if (corpus_path.ContainsCI("aspect"))
                    {
                        // Корпус с упоминанием аспектов
                        rb_aspect.Checked = true;
                    }
                    else
                    {
                        throw new ApplicationException($"Не могу определить вид корпуса из имени файла {corpus_path}");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("LoadCorpus error", ex);
                    ShowErrorAndContinue(ex.Message);
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
                GetLog().ErrorFormat("Error in {0}: {1}", nameof(lv_lines_RetrieveVirtualItem), ex.Message);
                ShowErrorAndContinue(ex.Message);
                // !!! гасим исключение, пробуем работать дальше
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
            try
            {
                if (lv_lines.SelectedIndices.Count == 1)
                {
                    current_line_index = lv_lines.SelectedIndices[0].ToString();

                    using (System.Data.SQLite.SQLiteConnection cnx = GetConnection())
                    {
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
                    } //cnx.Close();
                }
                else
                {
                    current_line_index = "";
                }
            }
            catch (Exception ex)
            {
                GetLog().ErrorFormat("Error in {0}: {1}", nameof(lv_lines_SelectedIndexChanged), ex.Message);
                ShowErrorAndContinue(ex.Message);
                // !!! гасим исключение, пробуем работать дальше
            }

            StoreConfig();
        }

        private void StoreLabels(params string[] labels)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (System.Data.SQLite.SQLiteConnection cnx = GetConnection())
                {
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

                } // cnx.Close();

                lv_lines.Focus();
            }
            catch (Exception ex)
            {
                GetLog().ErrorFormat("Error in {0}: {1}", nameof(StoreLabels), ex.Message);
                ShowErrorAndAbort(ex.Message);
                throw; // так как произошла ошибка при сохранении изменений в БД, то прекратим работу программы
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

        /// <summary>
        /// Экспорт содержимого БД в CSV файл
        /// </summary>
        private static void SaveCorpus()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (System.Data.SQLite.SQLiteConnection cnx = GetConnection())
                {
                    System.Data.SQLite.SQLiteTransaction tx = cnx.BeginTransaction(IsolationLevel.ReadCommitted);

                    System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("SELECT id, str, labels FROM Sent_DATASET ORDER BY id", cnx, tx);

                    string export_path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(corpus_path), System.IO.Path.GetFileNameWithoutExtension(corpus_path) + ".dat");
                    var log = GetLog();
                    log.InfoFormat("SaveCorpus export_path={0}", export_path);
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

                    log.InfoFormat("Database exported to {0}", export_path);
                    ShowInfo($"Данные сохранены в файл {export_path}");
                } //cnx.Close();
            }
            catch( Exception ex)
            {
                GetLog().ErrorFormat("Error in {0}: {1}", nameof(SaveCorpus), ex.Message);
                ShowErrorAndContinue(ex.Message);
                // Попробуем дать возможность работать далее
            }

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