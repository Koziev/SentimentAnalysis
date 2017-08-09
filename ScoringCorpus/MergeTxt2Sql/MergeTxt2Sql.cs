using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static string AddLabel(string str, string label)
    {
        if (string.IsNullOrEmpty(str))
        {
            return label;
        }
        else
        {
            string[] tags = str.Split(' ');

            string prefix = label.Split('=')[0] + "=";

            return string.Join(" ", tags.Where(z => !z.StartsWith(prefix)).ToArray()) + " " + label;
        }
    }

    static void Main(string[] args)
    {
        List<Tuple<string, string>> sents = new List<Tuple<string, string>>();
        Dictionary<string, int> sent2id = new Dictionary<string, int>();

        using (System.IO.StreamReader rdr = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\unscored_lines.dat"))
        {
            for (int i = 0; i < 40000; ++i)
            {
                string line = rdr.ReadLine();
                string[] toks = line.Split('\t');
                string sent = toks[0];
                string label = "sign=" + toks[1];

                int id_sent = -1;
                if (!sent2id.TryGetValue(sent, out id_sent))
                {
                    id_sent = sents.Count;
                    sents.Add(new Tuple<string, string>(sent, label));
                    sent2id.Add(sent, id_sent);
                }
            }
        }

        // ===================================================================

        using (System.IO.StreamReader rdr = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\scored_lines.dat"))
        {
            while (!rdr.EndOfStream)
            {
                string line = rdr.ReadLine();
                if (line == null) break;

                string[] toks = line.Split('\t');
                string sent = toks[0];
                string label = "sign=" + (toks[1] == "+" ? "1" : "-1");

                int id_sent = -1;
                if (!sent2id.TryGetValue(sent, out id_sent))
                {
                    id_sent = sents.Count;
                    sents.Add(new Tuple<string, string>(sent, label));
                    sent2id.Add(sent, id_sent);
                }
            }
        }

        // ===================================================================

        using (System.IO.StreamReader rdr = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\catering_1.dat"))
        {
            while (!rdr.EndOfStream)
            {
                string line = rdr.ReadLine();
                if (line == null) break;

                string[] toks = line.Split('\t');
                string sent = toks[0];
                string label = "catering=1";

                int id_sent = -1;
                if (!sent2id.TryGetValue(sent, out id_sent))
                {
                    id_sent = sents.Count;
                    sents.Add(new Tuple<string, string>(sent, label));
                    sent2id.Add(sent, id_sent);
                }
                else
                {
                    sents[id_sent] = new Tuple<string, string>(sent, AddLabel(sents[id_sent].Item2, label));
                }
            }
        }

        // ==================================================================

        // catering_0.dat


        // ===================================================================

        System.Data.SQLite.SQLiteConnectionStringBuilder cnx_build = new System.Data.SQLite.SQLiteConnectionStringBuilder();
        cnx_build.DataSource = @"e:\MVoice\Слова\rus\scoring_corpus.sqlite";

        if (System.IO.File.Exists(cnx_build.DataSource))
        {
            System.IO.File.Delete(cnx_build.DataSource);
        }

        System.Data.SQLite.SQLiteConnection cnx = new System.Data.SQLite.SQLiteConnection(cnx_build.ConnectionString);
        cnx.Open();

        System.Data.SQLite.SQLiteCommand cmd1 = new System.Data.SQLite.SQLiteCommand("CREATE TABLE Sent_DATASET( id INT, str VARCHAR(1000), labels VARCHAR(10) )", cnx);
        cmd1.ExecuteNonQuery();

        cmd1 = new System.Data.SQLite.SQLiteCommand("CREATE UNIQUE INDEX Sent_DATASET_IDX1 ON Sent_DATASET(ID)", cnx);
        cmd1.ExecuteNonQuery();

        System.Data.SQLite.SQLiteTransaction tx = cnx.BeginTransaction(IsolationLevel.ReadCommitted);

        for (int i = 0; i < sents.Count; ++i)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand("INSERT INTO Sent_DATASET( id, str, labels ) VALUES ( ?, ?, ? )", cnx, tx);
            cmd.Parameters.AddWithValue("id", i);
            cmd.Parameters.AddWithValue("str", sents[i].Item1);
            cmd.Parameters.AddWithValue("labels", sents[i].Item2);
            cmd.ExecuteNonQuery();

            if ((i % 10000) == 0)
            {
                tx.Commit();
                tx = cnx.BeginTransaction();
            }
        }

        tx.Commit();
        cnx.Close();

        return;
    }
}
