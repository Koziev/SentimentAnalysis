using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace SplitCorpus
{
    class Program
    {
        static void ConvertPolarity()
        {
            List<Tuple<string, string>> sents = new List<Tuple<string, string>>();
            HashSet<string> sent_set = new HashSet<string>();

            using (System.IO.StreamReader rdr = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\sentiment.export.dat"))
            {
                for (int i = 0; i < 40000; ++i)
                {
                    string line = rdr.ReadLine();
                    if (line == null) break;

                    string[] parts = line.Split('\t');

                    string label = "";
                    if (parts[1].Contains("sign=1"))
                    {
                        label = "posit=1";
                    }
                    else if (parts[1].Contains("sign=-1"))
                    {
                        label = "negat=1";
                    }
                    else
                    {
                        label = "nosign=1";
                    }

                    sent_set.Add(parts[0]);
                    sents.Add(new Tuple<string, string>(parts[0], label));
                }

                using (System.IO.StreamReader rdr2 = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\unscored_lines.dat"))
                {
                    for (int i = 0; i < 40000; ++i)
                    {
                        rdr2.ReadLine();
                    }

                    while (!rdr.EndOfStream && !rdr2.EndOfStream)
                    {
                        string line = rdr2.ReadLine();
                        if (line == null) break;
                        string sent = line.Split('\t')[0];
                        if (!sent_set.Contains(sent))
                        {
                            sent_set.Add(sent);
                            sents.Add(new Tuple<string, string>(sent, "nosign=1"));
                        }

                        // -----------------------------------------------------
                        line = rdr.ReadLine();
                        if (line == null) break;
                        string[] parts = line.Split('\t');

                        if (!sent_set.Contains(parts[0]))
                        {
                            string label = "";
                            if (parts[1].Contains("sign=1"))
                            {
                                label = "posit=1";
                            }
                            else if (parts[1].Contains("sign=-1"))
                            {
                                label = "negat=1";
                            }
                            else
                            {
                                label = "nosign=1";
                            }

                            sent_set.Add(parts[0]);
                            sents.Add(new Tuple<string, string>(parts[0], label));
                        }
                    }
                }
            }





            System.Data.SQLite.SQLiteConnectionStringBuilder cnx_build = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            cnx_build.DataSource = @"e:\MVoice\Слова\rus\polarity_corpus.sqlite";

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


        static void ConvertModifiers()
        {
            List<Tuple<string, string>> sents = new List<Tuple<string, string>>();
            HashSet<string> sent_set = new HashSet<string>();

            HashSet<string> modifiers = new HashSet<string>();
            modifiers.Add("ассортимент");
            modifiers.Add("качество");
            modifiers.Add("количество");
            modifiers.Add("вкусность");
            modifiers.Add("дешевизна");
            modifiers.Add("близость");


            using (System.IO.StreamReader rdr = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\sentiment.export.dat"))
            {
                for (int i = 0; i < 2000; ++i)
                {
                    string line = rdr.ReadLine();
                    if (line == null) break;

                    string[] parts = line.Split('\t');

                    System.Text.StringBuilder buf = new System.Text.StringBuilder();
                    foreach (string l in parts[1].Split(' '))
                    {
                        if (modifiers.Contains(l.Split('=')[0]))
                        {
                            buf.AppendFormat(" {0}", l);
                        }
                    }

                    sent_set.Add(parts[0]);
                    sents.Add(new Tuple<string, string>(parts[0], buf.ToString().Trim()));
                }

                using (System.IO.StreamReader rdr2 = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\scored_lines.dat"))
                {
                    while (!rdr.EndOfStream && !rdr2.EndOfStream)
                    {
                        string line = rdr2.ReadLine();
                        if (line == null) break;
                        string sent = line.Split('\t')[0];
                        if (!sent_set.Contains(sent))
                        {
                            sent_set.Add(sent);
                            sents.Add(new Tuple<string, string>(sent, ""));
                        }

                        // -----------------------------------------------------
                        line = rdr.ReadLine();
                        if (line == null) break;
                        string[] parts = line.Split('\t');

                        if (!sent_set.Contains(parts[0]))
                        {
                            string label = "";

                            sent_set.Add(parts[0]);
                            sents.Add(new Tuple<string, string>(parts[0], label));
                        }
                    }
                }
            }





            System.Data.SQLite.SQLiteConnectionStringBuilder cnx_build = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            cnx_build.DataSource = @"e:\MVoice\Слова\rus\modifiers_corpus.sqlite";

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

        static void ConvertAspects()
        {
            List<Tuple<string, string>> sents = new List<Tuple<string, string>>();
            HashSet<string> sent_set = new HashSet<string>();

            using (System.IO.StreamReader rdr1 = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\aspects.dat"))
            using (System.IO.StreamReader rdr2 = new System.IO.StreamReader(@"e:\MVoice\Слова\rus\unscored_lines.dat"))
            {
                while (!rdr1.EndOfStream && !rdr2.EndOfStream)
                {
                    string line = rdr1.ReadLine();
                    if (line == null) break;

                    string[] parts = line.Split('\t');

                    string label = parts[1];

                    sent_set.Add(parts[0]);
                    sents.Add(new Tuple<string, string>(parts[0], label));

                    line = rdr2.ReadLine();
                    if (line == null) break;
                    parts = line.Split('\t');

                    if (!sent_set.Contains(parts[0]))
                    {
                        sent_set.Add(parts[0]);
                        sents.Add(new Tuple<string, string>(parts[0], ""));
                    }
                }
            }



            System.Data.SQLite.SQLiteConnectionStringBuilder cnx_build = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            cnx_build.DataSource = @"e:\MVoice\Слова\rus\aspects_corpus.sqlite";

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

        static void Main(string[] args)
        {
            //ConvertModifiers();
            ConvertAspects();

            return;
        }
    }
}
